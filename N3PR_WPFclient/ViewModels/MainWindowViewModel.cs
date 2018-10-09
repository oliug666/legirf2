using System;
using System.Threading;
using N3PR_WPFclient.Connectors;
using N3PR_WPFclient.Helpers;
using N3PR_WPFclient.Core;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace N3PR_WPFclient.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ModbusConnector MbConnector;
        private DatabaseConnector DbConnector;
        private Thread MbConnectorTh, DbConnectorTh;        

        private string _modbusConnStatus, _sqlConnStatus, _statusMessage;
        private string _mbRxDataCounter, _sqlTxDataCounter;
        private string _queueDataCounter;

        public string ModbusConnectionStatus { get { return _modbusConnStatus; } set { _modbusConnStatus = value; OnPropertyChanged(() => ModbusConnectionStatus); } }
        public string SqlConnectionStatus { get { return _sqlConnStatus; } set { _sqlConnStatus = value; OnPropertyChanged(() => SqlConnectionStatus); } }
        public string StatusMessage { get { return _statusMessage; } set { _statusMessage = value; OnPropertyChanged(() => StatusMessage); } }
        public string MbRxDataCounter { get { return _mbRxDataCounter; } set { _mbRxDataCounter = value; OnPropertyChanged(() => MbRxDataCounter); } }
        public string SqlTxDataCounter { get { return _sqlTxDataCounter; } set { _sqlTxDataCounter = value; OnPropertyChanged(() => SqlTxDataCounter); } }
        public string QueueDataCounter { get { return _queueDataCounter; } set { _queueDataCounter = value; OnPropertyChanged(() => QueueDataCounter); } }

        public MainWindowViewModel()
        {
            // Read eventually unsent data
            StartupAndReadUnsentData();

            XDocument doc = new XDocument();
            try
            {
                doc = XDocument.Load("mb_config.xml");
            }
            catch
            {
                GlobalCommands.ShowError.Execute(new Exception("Impossible to load XML configuration file."));
            }

            var mb = doc.Root.Descendants("Modbus");
            var sq = doc.Root.Descendants("MySql");

            string mbaddr = ParseXmlElement(mb.Elements("addr").Nodes());
            string mbport = ParseXmlElement(mb.Elements("port").Nodes());
            string mbfreq = ParseXmlElement(mb.Elements("data_frequency_sec").Nodes());

            string sqladdr = ParseXmlElement(sq.Elements("addr").Nodes());
            string sqlname = ParseXmlElement(sq.Elements("db_name").Nodes());
            string sqltablename = ParseXmlElement(sq.Elements("table_name").Nodes());
            string sqlusername = ParseXmlElement(sq.Elements("username").Nodes());
            string sqlpass = ParseXmlElement(sq.Elements("pwd").Nodes());

            MbConnector = new ModbusConnector(new ModbusCfg()
            {
                Address = mbaddr,
                Port = Convert.ToInt16(mbport),
                Freq = Convert.ToInt16(mbfreq)
            });

            DbConnector = new DatabaseConnector(new SqlCfg()
            {
                Address = sqladdr,                
                DBname = sqlname,
                TableName = sqltablename,
                Username = sqlusername,
                Password = sqlpass
            });

            MbConnector.OnDataReceivedEvent += new EventHandler(MbDataReceived);
            MbConnector.ConnectionChangedEvent += new EventHandler(MbConnectionStatusChanged);
            MbConnector.ErrorEvent += new EventHandler(MbErrorHandler);
            DbConnector.OnDataTransmittedEvent += new EventHandler(SqlDataTransmitted);
            DbConnector.ConnectionChangedEvent += new EventHandler(SqlConnectionStatusChanged);
            DbConnector.ErrorEvent += new EventHandler(SqlErrorHandler);

            MbConnectorTh = new Thread(MbConnector.ModbusConnectorThread);
            MbConnectorTh.IsBackground = true;
            MbConnectorTh.Start();
            DbConnectorTh = new Thread(DbConnector.DatabaseConnectorThread);
            DbConnectorTh.IsBackground = true;
            DbConnectorTh.Start();

            // Refresh queue counter
            MbRxDataCounter = 0.ToString();
            SqlTxDataCounter = 0.ToString();

            Thread RefreshQueueCounterTh = new Thread(RefreshQueueCounter);
            RefreshQueueCounterTh.IsBackground = true;
            RefreshQueueCounterTh.Start();
        }

        private string ParseXmlElement(IEnumerable<XNode> nodes)
        {
            List<string> myS = new List<string>();
            foreach (XNode xn in nodes)
                myS.Add(xn.ToString());

            if (myS.Count > 0)
                return myS[0];
            else
                return "";
        }

        private void MbConnectionStatusChanged(object sender, System.EventArgs e)
        {
            ModbusConnectionStatus = MbConnector.IsConnected ? "Connected" : "Disconnected";
            StatusMessage += DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]") + " Modbus status change: " + ModbusConnectionStatus+".\n";
        }

        private void SqlConnectionStatusChanged(object sender, System.EventArgs e)
        {
            SqlConnectionStatus = DbConnector.IsConnected ? "Connected" : "Disconnected";
            StatusMessage += DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]") + " MySQL status change: " + SqlConnectionStatus + ".\n";
        }

        private void SqlDataTransmitted(object sender, System.EventArgs e)
        {
            SqlTxDataCounter = (Convert.ToInt32(SqlTxDataCounter) + 1).ToString();
            if (DataContainer.Data.DataQueue.Count == 0)
            {
                StatusMessage += DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]") + " MySQL queue Empty!.\n";
            }
        }

        private void MbDataReceived(object sender, System.EventArgs e)
        {
            MbRxDataCounter = (Convert.ToInt32(MbRxDataCounter) + 1).ToString();            
        }

        private void MbErrorHandler(object sender, System.EventArgs e)
        {            
            StatusMessage += DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss] ") + MbConnector.Status;
        }

        private void SqlErrorHandler(object sender, System.EventArgs e)
        {
            StatusMessage += DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss] ") + DbConnector.Status;
        }

        private void RefreshQueueCounter()
        {
            while(true)
            {
                QueueDataCounter = DataContainer.Data.DataQueue.Count.ToString();
                Thread.Sleep(300);
            }
        }

        public void TerminateExecutionAndSaveUnsentData()
        {
            DbConnectorTh.Abort();
            while (DbConnectorTh.ThreadState != ThreadState.Aborted) ;
            if (DbConnector.IsConnected)
                DbConnector.Dispose();

            MbConnectorTh.Abort();
            while (MbConnectorTh.ThreadState != ThreadState.Aborted) ;
            if (MbConnector.IsConnected)
                MbConnector.Dispose();

            StringBuilder sb = new StringBuilder();
            MeasurePoint _dataPoint;
            while (DataContainer.Data.DataQueue.Count != 0)
            {
                DataContainer.Data.DataQueue.TryTake(out _dataPoint, -1);
                sb.Append(string.Format("('{0}','{1}','{2}','{3}','{4}')\n", _dataPoint.Date.ToString(), _dataPoint.Reg_Name,
                    Convert.ToInt16(_dataPoint.b_val), _dataPoint.i_val, _dataPoint.ui_val));
            }

            try
            {
                File.AppendAllText("logFile.txt", sb.ToString());
            }
            catch (Exception ex)
            {
                StatusMessage += DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss] ") + ex.Message + ".\n";
            }
        }

        public void StartupAndReadUnsentData()
        {
            try
            {
                if (File.Exists("logFile.txt"))
                {
                    var myUnsentData = File.ReadAllLines("logFile.txt");
                    StatusMessage += DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss] ") + myUnsentData.Length.ToString() + " unsent data found in logFile.txt\n";

                    foreach (string data in myUnsentData)
                    {
                        string[] substrings = data.Replace("(", "").Replace(")", "").Replace("'", "").Split(',');
                        DataContainer.Data.DataQueue.Add(new MeasurePoint
                        {
                            Date = DateTime.ParseExact(substrings[0], "dd/MM/yyyy HH:mm:ss",
                            System.Globalization.CultureInfo.InvariantCulture),
                            Reg_Name = substrings[1],
                            b_val = Convert.ToBoolean(Convert.ToInt32(substrings[2])),
                            i_val = Convert.ToInt32(substrings[3]),
                            ui_val = Convert.ToUInt32(substrings[4])
                        });
                    }
                    File.Delete("logFile.txt");
                }
            }
            catch (Exception ex)
            {
                StatusMessage += DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss]") + " Error reading logFile.txt " + ex.Message + "\n";
            }
        }
    }
}

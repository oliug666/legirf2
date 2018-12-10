using System;
using System.Linq;
using MySql.Data.MySqlClient;
using N3PR_WPFclient.Helpers;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Threading;

namespace N3PR_WPFclient.Connectors
{
    public enum ServerState
    {
        CONNECT_TO_SQL_SERVER,
        CONNECTION_ESTABLISHED,
        CONNECTION_ERROR
    }

    public class DatabaseConnector
    {
        private ServerState _myServerState;
        private MySqlConnection _mySqlConnection;
        private string _databaseName, _serverName, _userName, _passWord, _tableName;
        private bool _isConnected;
        private MeasurePoint _dataPoint;

        public string Status;
        public EventHandler ErrorEvent;
        public EventHandler OnDataTransmittedEvent;
        public EventHandler ConnectionChangedEvent;
        public bool IsConnected { get { return _isConnected; } set { _isConnected = value; ConnectionChangedEvent?.Invoke(this, null); } }

        public DatabaseConnector(SqlCfg cfg)
        {
            _myServerState = ServerState.CONNECT_TO_SQL_SERVER;

            _serverName = cfg.Address;
            _databaseName = cfg.DBname;
            _tableName = cfg.TableName;
            _userName = cfg.Username;
            _passWord = cfg.Password;

            IsConnected = false;
        }

        public void DatabaseConnectorThread()
        {           
            while (true)
            {
                switch (_myServerState)
                {
                    case ServerState.CONNECT_TO_SQL_SERVER:
                        IsConnected = false;

                        if (Connect())
                        {
                            _myServerState = ServerState.CONNECTION_ESTABLISHED;
                            IsConnected = true;
                        }
                        else
                            _myServerState = ServerState.CONNECTION_ERROR;

                        break;

                    case ServerState.CONNECTION_ESTABLISHED:                        
                        // Wait for event from Modbus_thread
                        while (DataContainer.Data.DataQueue.TryTake(out _dataPoint, -1))
                        {
                            /*
                            string insertquery = string.Format("INSERT INTO " + _tableName + "(" + N3PR.DATE + "," + N3PR.REG_NAME + "," + N3PR.BVAL +
                                "," + N3PR.IVAL + "," + N3PR.UIVAL + ") VALUES ('{0}','{1}','{2}','{3}','{4}')",
                                _dataPoint.Date.ToString(N3PR.DATA_FORMAT), _dataPoint.Reg_Name, Convert.ToInt16(_dataPoint.b_val),
                                _dataPoint.i_val, _dataPoint.ui_val);
                            */
                            string insertquery = string.Format("INSERT INTO " + _tableName + "(" + N3PR.DATE + "," + N3PR.REG_NAME + "," + 
                                N3PR.IVAL + ") VALUES ('{0}','{1}','{2}','{3}','{4}')", _dataPoint.Date.ToString(N3PR.DATA_FORMAT), _dataPoint.Reg_Name, _dataPoint.i_val);

                            var cmd = new MySqlCommand(insertquery, _mySqlConnection);
                            try
                            {
                                int ret = cmd.ExecuteNonQuery();
                                if (ret < 0)
                                {
                                    Status = "MySQL Error inserting " + _dataPoint.Reg_Name + "register into database.\n";
                                    ErrorEvent?.Invoke(this, null);
                                    _myServerState = ServerState.CONNECTION_ERROR;
                                    // Put data back in queue
                                    lock (DataContainer.Data.DataQueue)
                                    {
                                        DataContainer.Data.DataQueue.Add(_dataPoint);
                                    }
                                    break;
                                }                                
                            }
                            catch (Exception e)
                            {
                                Status = "MySQL Error: " + e.Message + ".\n";
                                ErrorEvent?.Invoke(this, null);
                                _myServerState = ServerState.CONNECTION_ERROR;
                                // Put data back in queue
                                lock (DataContainer.Data.DataQueue)
                                {
                                    DataContainer.Data.DataQueue.Add(_dataPoint);
                                }
                                break;
                            }
                            // Signal event for visualization
                            OnDataTransmittedEvent?.Invoke(this, null);
                        }                                                                                                

                        break;

                    case ServerState.CONNECTION_ERROR:
                        _mySqlConnection.Dispose();
                        _mySqlConnection = null;
                        Thread.Sleep(2 * 1000);
                        _myServerState = ServerState.CONNECT_TO_SQL_SERVER;
                        break;
                }
            }
        }

        private bool Connect()
        {
            if (_mySqlConnection == null)
            {
                if (String.IsNullOrEmpty(_databaseName))
                    return false;
                string connstring = string.Format("Server={0};Database={1};UID={2};PWD={3}",
                    _serverName, _databaseName, _userName, _passWord);
                _mySqlConnection = new MySqlConnection(connstring);
                try
                {
                    _mySqlConnection.Open();
                }
                catch (Exception e)
                {
                    Status = "MySQL Error: " + e.Message + ".\n";
                    ErrorEvent?.Invoke(this, null);
                    return false;
                }
            }

            if (_mySqlConnection.State == System.Data.ConnectionState.Open)
                return true;
            else
                return false;
        }

        public void Dispose()
        {
            try
            {
                _mySqlConnection.Close();
                _mySqlConnection.Dispose();
            }
            catch (Exception e)
            {
                Status = "MySQL Error: " + e.Message + ".\n";
                ErrorEvent?.Invoke(this, null);
            }
        }
    }
}

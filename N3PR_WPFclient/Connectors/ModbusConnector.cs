using Modbus.Device;
using N3PR_WPFclient.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace N3PR_WPFclient.Connectors
{
    public enum MbState
    {
        CREATING_MB_SOCKET,
        CONNECTION_MB_ESTABLISHED
    }

    public class ModbusConnector
    {
        public string IPAddress;
        public int Port;
        public int FreqReadSec;
        private Thread _dataRetrievingThread, _alarmRetrievingThread;
        private List<MeasurePoint> _alarmList, _alarmOldList;

        public bool IsConnected { get { return _isConnected; } set { _isConnected = value; ConnectionChangedEvent?.Invoke(this, null); } }
        public ThreadState DataRetrievingThreadState { get {
                if (_dataRetrievingThread != null) return _dataRetrievingThread.ThreadState;
                else return ThreadState.Stopped;
            } }

        public EventHandler OnDataReceivedEvent;
        public EventHandler OnAlarmReceivedEvent;
        public EventHandler ConnectionChangedEvent;
        public EventHandler ErrorEvent;

        public string Status;

        private ModbusIpMaster _master;
        private MbState _myMbState;
        private TcpClient _client;
        private bool _isConnected;

        public ModbusConnector(ModbusCfg mb)
        {
            Port = mb.Port;
            IPAddress = mb.Address;
            FreqReadSec = mb.Freq;
            _myMbState = MbState.CREATING_MB_SOCKET;

            _alarmList = new List<MeasurePoint>();
            _alarmOldList = new List<MeasurePoint>();
            InitializeAlarmOldList(); // Populate buffer list with alarms 0 (not active)
        }

        private bool Connect()
        {
#if DEMO
            return true;
#else
            try
            {
                _client = new TcpClient(IPAddress, Port);
                _client.LingerState = new LingerOption(true, 1);
                _master = ModbusIpMaster.CreateIp(_client);
                return (_master != null) ? true : false;
            }
            catch (Exception e)
            {
                var aa = e.InnerException;
                return false;
            }            
#endif
        }

        public void Dispose()
        {
            try
            {
                if (_master != null) _master.Dispose();
                if (_client != null) _client.Close();
            }
            catch (Exception e)
            {
                Status = "Error disposing Modbus objects " + e.Message + " .\n";
                ErrorEvent?.Invoke(this, null);
            }            
        }

        public void ModbusConnectorThread()
        {
            while (true)
            {
                switch (_myMbState)
                {
                    case MbState.CREATING_MB_SOCKET:
                        if (Connect())
                        {
#if !DEMO
                            IsConnected = true;
                            _myMbState = MbState.CONNECTION_MB_ESTABLISHED;
                            // Start threads
                            _dataRetrievingThread = new Thread(DataRetrievingThread);
                            _dataRetrievingThread.IsBackground = true;
                            _alarmRetrievingThread = new Thread(AlarmRetrievingThread);
                            _alarmRetrievingThread.IsBackground = true;
                            _dataRetrievingThread.Start();
                            _alarmRetrievingThread.Start();
#else
                            IsConnected = true;
                            _myMbState = MbState.CONNECTION_MB_ESTABLISHED;
                            _dataRetrievingThread = new Thread(DataRetrievingThread);
                            _dataRetrievingThread.IsBackground = true;
                            _dataRetrievingThread.Start();
#endif
                        }
                        else
                        {
                            IsConnected = false;
                            if (_master != null) _master.Dispose();
                            if (_client != null) _client.Close();
                            System.Threading.Thread.Sleep(5000); // Re-try to connect after 5 seconds
                        }
                        break;

                    case MbState.CONNECTION_MB_ESTABLISHED:
#if !DEMO
                        if (_dataRetrievingThread.ThreadState == ThreadState.Stopped ||
                            _alarmRetrievingThread.ThreadState == ThreadState.Stopped)
                        {
                            _dataRetrievingThread.Abort();
                            _dataRetrievingThread = null;

                            _alarmRetrievingThread.Abort();
                            _alarmRetrievingThread = null;

                            IsConnected = false;                            
                            ErrorEvent?.Invoke(this, null);
                            if (_master != null) _master.Dispose();
                            if (_client != null) _client.Close();
                            _myMbState = MbState.CREATING_MB_SOCKET;
                        }               
#else
                        if (_dataRetrievingThread.ThreadState == ThreadState.Stopped)
                        {
                            _dataRetrievingThread.Abort();
                            _dataRetrievingThread = null;

                            IsConnected = false;
                            ErrorEvent?.Invoke(this, null);
                            if (_master != null) _master.Dispose();
                            if (_client != null) _client.Close();
                            _myMbState = MbState.CREATING_MB_SOCKET;
                        }
#endif
                        break;
                }
                System.Threading.Thread.Sleep(100);
            }           
        }            

        private void DataRetrievingThread()
        {
#if DEMO
            Random rnd = new Random();
#endif
            while (true)
            {
                // Get time
                DateTime now = DateTime.Now;
                // Read			
                lock (DataContainer.Data.DataQueue)
                {
                    // Register                    
                    for (int i = 0; i < N3PR.REG_ADDRESS.Count; i++)
                    {
                        try
                        {
                            ushort[] regs = new ushort[2];
#if !DEMO
                            if (N3PR.REG_FUNCTION_CODES[i] == "0x04")
                            {
                                regs = _master.ReadInputRegisters(N3PR.REG_ADDRESS[i], 1);
                            }
                            else if (N3PR.REG_FUNCTION_CODES[i] == "0x02")
                            {
                                bool[] _hh = _master.ReadInputs(N3PR.REG_ADDRESS[i], 1);
                                for (int j = 0; j < _hh.Count(); j++)
                                    regs[j] = Convert.ToUInt16(_hh[j]);
                            }
#endif
                            if (regs != null && regs.Count() > 0)
                            {
#if DEMO
                                var mp = new MeasurePoint
                                {
                                    Date = now,
                                    Reg_Name = N3PR.REG_NAMES[i],
                                    b_val = Convert.ToBoolean(rnd.Next(0, 2)),
                                    i_val = Convert.ToInt32((uint)rnd.Next(0, 32766)),
                                    ui_val = (uint)rnd.Next(0, 65535)
                                };
                                DataContainer.Data.DataQueue.Add(mp);
#else
                                if (regs[0] < 32766)
                                {
                                    var mp = new MeasurePoint
                                    {
                                        Date = now,
                                        Reg_Name = N3PR.REG_NAMES[i],
                                        b_val = Convert.ToBoolean(regs[0]),
                                        i_val = Convert.ToInt32(regs[0]),
                                        ui_val = (uint)regs[0]
                                    };
                                    DataContainer.Data.DataQueue.Add(mp);
                                }
                                else
                                {
                                    var mp = new MeasurePoint
                                    {
                                        Date = now,
                                        Reg_Name = N3PR.REG_NAMES[i],
                                        b_val = Convert.ToBoolean(regs[0]),
                                        i_val = Convert.ToInt32(regs[0] - 65532),
                                        ui_val = (uint)regs[0]
                                    };
                                    DataContainer.Data.DataQueue.Add(mp);
                                }
#endif                                
                                OnDataReceivedEvent?.Invoke(this, null);
                            }
                        }
                        catch
                        {
                            Status = "Error reading Data ModBus register.\n";
                            return;                            
                        }                        
                    }
                }
                Thread.Sleep(1000 * FreqReadSec);
            }
        }

        private void AlarmRetrievingThread()
        {
#if DEMO
            Random rnd = new Random();
#endif
            while (true)
            {
                // Get time
                DateTime now = DateTime.Now;
                // Clear the list of the alarms (will be populated in the loop)
                _alarmList.Clear();
                // Alarms
                for (int i = 0; i < N3PR.ALARM_ADDRESS.Count; i++)
                {
                    try
                    {
                        ushort[] regs = new ushort[2];
                        if (N3PR.ALARM_FUNCTION_CODES[i] == "0x04")
                        {
                            regs = _master.ReadInputRegisters(N3PR.ALARM_ADDRESS[i], 1);
                        }
                        else if (N3PR.ALARM_FUNCTION_CODES[i] == "0x02")
                        {
                            bool[] _hh = _master.ReadInputs(N3PR.ALARM_ADDRESS[i], 1);
                            for (int j = 0; j < _hh.Count(); j++)
                                regs[j] = Convert.ToUInt16(_hh[j]);
                        }
                        if (regs != null && regs.Count() > 0)
                        {
#if DEMO
                            var mp = new MeasurePoint
                            {
                                Date = now,
                                Reg_Name = N3PR.ALARM_NAMES[i],
                                b_val = Convert.ToBoolean(rnd.Next(0, 2)),
                                i_val = (int)rnd.Next(-32767, 32767),
                                ui_val = (uint)rnd.Next(0, 65535)
                            };
#else
                            var mp = new MeasurePoint
                            {
                                Date = now,
                                Reg_Name = N3PR.ALARM_NAMES[i],
                                b_val = Convert.ToBoolean(regs[0]),
                                i_val = (int)regs[0],
                                ui_val = (uint)regs[0]
                            };
#endif
                            _alarmList.Add(mp);
                        }
                    }
                    catch
                    {
                        _alarmList.Clear();
                        Status = "Error reading Alarm ModBus register.\n";
                        return;
                    }
                }
                // Check if the alarm status has changed
                // If so, send to database
                if (_alarmOldList.Count() == _alarmList.Count())
                {
                    for (int j = 0; j < _alarmOldList.Count(); j++)
                    {
                        if (_alarmList[j].b_val != _alarmOldList[j].b_val)
                        {
                            // Memorize values
                            _alarmOldList[j] = _alarmList[j];
                            lock (DataContainer.Data.DataQueue)
                            {
                                DataContainer.Data.DataQueue.Add(_alarmList[j]);
                                OnDataReceivedEvent?.Invoke(this, null);
                            }
                            //Trigger also a message to display the alarm
                            OnAlarmReceivedEvent?.Invoke(this, null);
                        }
                    }
                }
                Thread.Sleep(1000);
            }
        }
        
        private void InitializeAlarmOldList()
        {
            _alarmOldList.Clear();
            // Initialize the buffer alarm list with 0 (no alarms)
            for (int i=0;i<N3PR.ALARM_ADDRESS.Count();i++)
            {
                var mp = new MeasurePoint
                {
                    Date = DateTime.Now,
                    Reg_Name = N3PR.ALARM_NAMES[i],
                    b_val = Convert.ToBoolean(0),
                    i_val = 0,
                    ui_val = 0
                };
                _alarmOldList.Add(mp);
            }
        }
    }
}

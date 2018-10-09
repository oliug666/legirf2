using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N3PR_WPFclient.Helpers
{
    public interface IData
    {
        BlockingCollection<MeasurePoint> DataQueue { get; set; }
    }
}

/*
public interface IGeccoDriver
{
    bool IsConnected { get; }
    event EventHandler OnDataRetrievalCompleted;
    event EventHandler OnLatestDataRetrievalCompleted;

    IList<MeasurePoint> MbData { get; }
    IList<MeasurePoint> LatestData { get; }

    void Connect(string ipa, int port, string dbname, string username, string password);

    void Disconnect();

    void GetDataFromLastXDays(string tableName, int lastDays);

    void GetLatestData(string tableName);
}
*/
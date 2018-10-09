using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N3PR_WPFclient.Helpers
{
    public class Data : IData
    {       
        public BlockingCollection<MeasurePoint> DataQueue { get; set; }

        public Data()
        {
            DataQueue = new BlockingCollection<MeasurePoint>();
        }
    }
}

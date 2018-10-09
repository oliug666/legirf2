using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N3PR_WPFclient.Helpers
{
    public static class DataContainer
    {
        private static IData _data;
        public static IData Data
        {
            get { return _data ?? (_data = new Data()); }
            set { _data = value; }
        }
    }
}
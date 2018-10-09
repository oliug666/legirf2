using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N3PR_WPFclient.Helpers
{
    public class SqlCfg
    {
        public string Address, DBname, Username, Password, TableName;
    }

    public class ModbusCfg
    {
        public string Address;
        public int Port, Freq;        
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N3PR_WPFclient.Helpers
{
    public class MeasurePoint
    {
        public DateTime Date { get; set; }
        public string Reg_Name { get; set; }
        public bool b_val { get; set; }
        public double i_val { get; set; }
        public double ui_val { get; set; }
    }
}
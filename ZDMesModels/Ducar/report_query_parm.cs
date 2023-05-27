using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.Ducar
{
    public class report_query_parm
    {
        public DateTime ksrq { get; set; }
        public DateTime jsrq { get; set; }
        public string scx { get; set; }
        public string engine_no { get; set; }
        public string gwh { get; set; }
        public int pageindex { get; set; }
        public int pagesize { get; set; }
        public int resultcount { get; set; }
    }
}

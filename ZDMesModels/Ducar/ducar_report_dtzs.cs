using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.Ducar
{
    public class ducar_report_dtzs
    {
        public string scx { get; set; }
        public string engine_no { get; set; }
        public string jx_no { get; set; }
        public string status_no { get; set; }
        public string gwh { get; set; }
        public string gwmc { get; set; }
        public string jcjg { get; set; }
        public string jcry { get; set; }
        public string jcsj { get; set; }
        public string jjh { get; set; }
        public List<zxjc_data_detail_mx> mxlist { get; set; }
    }
}

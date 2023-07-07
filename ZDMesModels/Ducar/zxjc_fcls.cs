using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.Ducar
{
    public class zxjc_fcls
    {
        public int id { get; set; }
        public string scx { get; set; }
        public string gwh { get; set; }
        public string gwmc { get; set; }
        public string engine_no { get; set; }
        public string gwbh { get; set; }
        public string xgbh { get; set; }
        public string lrr { get; set; }
        public DateTime lrsj { get; set; }
        public DateTime gwghsj { get; set; }
        public double gwbzsj { get; set; }
        public double gwsysj { get; set; }
        public DateTime xgghsj { get; set; }
        public double xgbzsj { get; set; }
        public double xgsysj { get; set; }

        public List<sys_options_list> gwhs { get; set; }
    }
}

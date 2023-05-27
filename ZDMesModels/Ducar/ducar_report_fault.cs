using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.Ducar
{
    public class ducar_report_fault
    {
        public string jx_no { get; set; }
        public string status_no { get; set; }
        public int jcsl { get; set; }
        /// <summary>
        /// 一次合格数量
        /// </summary>
        public int firsthgsl { get; set; }
        public int hgsl { get; set; }
        public double hgl { get; set; }
        public List<ducar_fxmx_item> fxmxlist { get; set; }
    }

    public class ducar_report_item
    {
        public string engine_no { get; set; }
        public string jx_no { get; set; }
        public string status_no { get; set; }
    }

    public class ducar_fxmx_item
    {
        public string fault_no { get; set; }
        public string fault_name { get; set; }
        public int gzsl { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.Ducar
{
    public class zxjc_order_jy
    {
        public string order_no { get; set; }
        public string qdjy { get; set; }
        public string gdbomjy { get; set; }
        public string gylxjy { get; set; }
        public string status { get; set; }
        public string sjbz { get; set; }
        public DateTime lrsj { get; set; } = DateTime.Now;
        public string scx { get; set; }
    }
}

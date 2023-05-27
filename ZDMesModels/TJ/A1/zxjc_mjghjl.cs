using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.TJ.A1
{
    /// <summary>
    /// 毛巾更换记录
    /// </summary>
    public class zxjc_mjghjl
    {
        public string gcdm { get; set; }
        public string scx { get; set; }
        public string gwh { get; set; }
        public string engine_no { get; set; }
        public string jx_no { get; set; }
        public string status_no { get; set; }
        public string order_no { get; set; }
        /// <summary>
        /// 装配计划号
        /// </summary>
        public string zpjhh { get; set; }
        /// <summary>
        /// 毛巾跟换时间
        /// </summary>
        public DateTime mjghsj { get; set; }
        public string lrr { get; set; }
        public DateTime lrsj { get; set; }
    }
}

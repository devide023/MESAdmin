using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.TJ.A1
{
    public class sys_jtfp_form
    {
        /// <summary>
        /// 技通编号
        /// </summary>
        public string jtid { get; set; }
        public List<string> gwh { get; set; }
        public string jxno { get; set; }
        public List<string> statusno { get; set; }
        public string bz { get; set; }
        public string lrr1 { get; set; }
    }
}

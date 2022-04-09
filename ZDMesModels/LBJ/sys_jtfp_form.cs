using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.LBJ;

namespace ZDMesModels.LBJ
{
    public class sys_jtfp_form
    {
        public zxjc_t_jstc jstc { get; set; }
        public string gcdm { get; set; }
        public string scx { get; set; }
        public List<string> fpgw { get; set; }
        public string jxlist { get; set; }
        public string statusno { get; set; }
        public string bz { get; set; }
    }
}

using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.LBJ
{
    public class sys_save_dbrjzx_form
    {
        public string scx { get; set; }
        public string scxzx { get; set; }
        public string sbbh { get; set; }
        public string wlbm { get; set; }
        public List<base_dbrjzx> dbrjzx { get; set; }
    }
}

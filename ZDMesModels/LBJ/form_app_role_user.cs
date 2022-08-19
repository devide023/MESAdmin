using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.LBJ
{
    public class form_app_role_user
    {
        public int roleid { get; set; }
        public List<app_user> users { get; set; }
    }

    public class form_app_role_menu
    {
        public int roleid { get; set; }
        public List<int> menuids { get; set; }
    }
}

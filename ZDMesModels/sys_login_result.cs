using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels
{
    public class sys_login_result:sys_result
    {
        public string token { get; set; }
    }

    public class sys_userinfo_result : sys_result
    {
        public mes_user_entity userinfo { get; set; }
        public List<mes_menu_entity> user_menus { get; set; }
    }
}

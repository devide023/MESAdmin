using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels
{
    public class mes_role_menu
    {
        public int id { get; set; }
        public int roleid { get; set; }
        public int menuid { get; set; }
        public string permis { get; set; }
        public sys_menu_permis menu_permis { get; set; }
    }

    public class mes_role_menu_mapper : ClassMapper<mes_role_menu>
    {
        public mes_role_menu_mapper()
        {
            Map(t => t.id).Key(KeyType.TriggerIdentity);
            Map(t => t.menu_permis).Ignore();
            AutoMap();
        }
    }
}

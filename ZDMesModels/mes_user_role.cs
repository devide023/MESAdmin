using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels
{
    public class mes_user_role
    {
        public int id { get; set; }
        public int userid { get; set; }
        public int roleid { get; set; }
    }

    public class mes_user_role_mapper : ClassMapper<mes_user_role>
    {
        public mes_user_role_mapper()
        {
            Map(t => t.id).Key(KeyType.TriggerIdentity);
            AutoMap();
        }
    }
}

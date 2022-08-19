using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.LBJ
{
    public class app_role
    {
        public int id { get; set; }
        public string name { get; set; }
        public int status { get; set; }
        public DateTime addtime { get; set; }
        public string adduser { get; set; }
        public List<int> rolemenus { get; set; }
        public List<string> roleuser { get; set; }
    }

    public class app_role_mapper : ClassMapper<app_role>
    {
        public app_role_mapper()
        {
            Map(t => t.id).Key(KeyType.TriggerIdentity);
            Map(t => t.rolemenus).Ignore();
            Map(t => t.roleuser).Ignore();
            AutoMap();
        }
    }
}

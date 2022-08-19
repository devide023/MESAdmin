using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.LBJ
{
    public class App_Menu
    {
        public int id { get; set; }
        public int pid { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string icon { get; set; }
        public int seq { get; set; }
        public DateTime addtime { get; set; }
        public string adduser { get; set; }
        public int status { get; set; }
        public string path { get; set; }
        public List<App_Menu> children { get; set; }
    }

    public class App_Menu_mapper : ClassMapper<App_Menu>
    {
        public App_Menu_mapper()
        {
            Map(t => t.id).Key(KeyType.TriggerIdentity);
            Map(t => t.children).Ignore();
            AutoMap();
        }
    }
}

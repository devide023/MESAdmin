using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.Ducar
{
    public class sc_jxgl
    {
        public string id { get; set; }=Guid.NewGuid().ToString();
        public string usercode { get; set; }
        public string username { get; set; }
        public string scx { get; set; }
        public string gwh { get; set; }
        public string gwmc { get; set; }
        public string wtd { get; set; }
        public string khyy { get; set; }
        public string lx { get; set; }
        public decimal jlje { get; set; } = Convert.ToDecimal(0);
        public decimal cfje { get; set; } = Convert.ToDecimal(0);
        public string lrr { get; set; }
        public DateTime lrsj { get; set; }=DateTime.Now;
        public DateTime fsrq { get; set; }
        public string bz { get; set; }
    }

    public class sc_jxgl_mapper : ClassMapper<sc_jxgl>
    {
        public sc_jxgl_mapper()
        {
            Map(t => t.id).Key(KeyType.Assigned);
            Map(t => t.usercode).Column("user_code");
            Map(t => t.username).Ignore();
            Map(t => t.gwmc).Ignore();
            AutoMap();
        }
    }
}

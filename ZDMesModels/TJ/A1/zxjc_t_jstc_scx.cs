using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.TJ.A1
{
    public class zxjc_t_jstc_scx
    {
        public string id { get; set; } = Guid.NewGuid().ToString();
        public string scx { get; set; }
        public string jcbh { get; set; }
        public string lrr { get; set; }
        public string jcmc { get; set; }
        public string wjlj { get; set; }
        public DateTime? lrsj { get; set; } = DateTime.Now;
    }

    public class zxjc_t_jstc_scx_mapper : ClassMapper<zxjc_t_jstc_scx>
    {
        public zxjc_t_jstc_scx_mapper()
        {
            Map(t => t.id).Key(KeyType.Assigned);
            Map(t => t.wjlj).Ignore();
            Map(t=>t.jcmc).Ignore();
            AutoMap();
        }
    }
}

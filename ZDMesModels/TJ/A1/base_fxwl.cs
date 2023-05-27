using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.TJ.A1
{
    /// <summary>
    /// 返修物料
    /// </summary>
    public class base_fxwl
    {
        public string gcdm { get; set; } = "9100";
        public string scx { get; set; }
        public string id { get; set; } = Guid.NewGuid().ToString();
        public string wlfl { get; set; }
        public string wlmc { get; set; }
        public string lrr { get; set; }
        public DateTime lrsj { get; set; } = DateTime.Now;
        public string wlbm { get; set; } = string.Empty;
    }

    public class base_fxwl_mapper:ClassMapper<base_fxwl>
    {
        public base_fxwl_mapper()
        {
            Map(t => t.id).Key(KeyType.Assigned);
            AutoMap();
        }
    }
}

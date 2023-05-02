using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.Ducar
{
    public class zxjc_order_sxh
    {
        public string orderno { get; set; }
        public string xh { get; set; }
        public DateTime sjscsj { get; set; }
        public DateTime lrsj { get; set; } = DateTime.Now;
        public pp_zpjh orderinfo { get; set; }
    }

    public class zxjc_order_sxh_mapper : ClassMapper<zxjc_order_sxh>
    {
        public zxjc_order_sxh_mapper()
        {
            Map(t => t.orderno).Column("order_no");
            Map(t => t.orderinfo).Ignore();
            AutoMap();
        }
    }
}

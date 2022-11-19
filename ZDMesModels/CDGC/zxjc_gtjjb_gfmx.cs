using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.CDGC
{
    /// <summary>
    /// 缸体交接班工废明细
    /// </summary>
    public class zxjc_gtjjb_gfmx
    {
        public string id { get; set; }=Guid.NewGuid().ToString();
        public int detailid { get; set; }
        public string vin { get; set; }
        public string yx { get; set; }
    }

    public class zxjc_gtjjb_gfmx_mapper : ClassMapper<zxjc_gtjjb_gfmx>
    {
        public zxjc_gtjjb_gfmx_mapper()
        {
            Map(t => t.id).Key(KeyType.Assigned);
            AutoMap();
        }
    }

}

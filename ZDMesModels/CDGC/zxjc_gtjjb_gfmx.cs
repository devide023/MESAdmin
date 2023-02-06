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
        public string cpmc { get; set; }
        public string vin { get; set; }
        /// <summary>
        /// 机台号
        /// </summary>
        public string jth { get; set; }
        /// <summary>
        /// 工废原因
        /// </summary>
        public string yx { get; set; }
        /// <summary>
        /// 录入人
        /// </summary>
        public string lrr { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime? lrsj { get; set; }
        /// <summary>
        /// 退废人
        /// </summary>
        public string tfr { get; set; }
        /// <summary>
        /// 退费时间
        /// </summary>
        public DateTime? tfsj { get; set; }
    }

    public class zxjc_gtjjb_gfmx_mapper : ClassMapper<zxjc_gtjjb_gfmx>
    {
        public zxjc_gtjjb_gfmx_mapper()
        {
            Map(t => t.id).Key(KeyType.Assigned);
            Map(t => t.lrsj).Ignore();
            Map(t => t.cpmc).Ignore();
            AutoMap();
        }
    }

}

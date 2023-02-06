using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.CDGC
{
    /// <summary>
    /// 电机壳交接班工废明细
    /// </summary>
    public class zxjc_djkjjb_gfmx
    {
        public string id { get; set; }=Guid.NewGuid().ToString();
        public int detailid { get; set; }
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
        /// 退废人
        /// </summary>
        public string tfr { get; set; }
        /// <summary>
        /// 退费时间
        /// </summary>
        public DateTime? tfsj { get; set; }
    }

    public class zxjc_djkjjb_gfmx_mapper : ClassMapper<zxjc_djkjjb_gfmx>
    {
        public zxjc_djkjjb_gfmx_mapper()
        {
            Map(t=>t.id).Key(KeyType.Assigned);
            AutoMap();
        }
    }
}

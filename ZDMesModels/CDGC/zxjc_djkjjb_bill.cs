using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ZDMesModels.CDGC
{
    public class zxjc_djkjjb_bill
    {
        /// <summary>
        /// ID
        ///</summary>
        public int id { get; set; }
        /// <summary>
        ///日期
        ///</summary>
        public DateTime? rq { get; set; }
        /// <summary>
        ///班次
        ///</summary>
        public string bc { get; set; }
        /// <summary>
        ///交班人
        ///</summary>
        public string jbr { get; set; }
        /// <summary>
        /// 后序人员
        /// </summary>
        public string hxry { get; set; }
        /// <summary>
        ///质量情况
        ///</summary>
        public string zlqk { get; set; }
        /// <summary>
        ///设备情况
        ///</summary>
        public string sbqk { get; set; }
        /// <summary>
        ///其他情况
        ///</summary>
        public string qtqk { get; set; }
        /// <summary>
        ///录入人
        ///</summary>
        public string lrr { get; set; }
        /// <summary>
        ///录入时间
        ///</summary>
        public DateTime? lrsj { get; set; }
        /// <summary>
        /// 机加明细
        /// </summary>
        public List<zxjc_djkjjb_detail> djkjjbdetail { get; set; }
        /// <summary>
        /// 后序明细
        /// </summary>
        public List<zxjc_djkjjb_hx_detail> djkjjbdetailhx { get; set; }

    }

    public class zxjc_djkjjb_bill_mapper : ClassMapper<zxjc_djkjjb_bill>
    {
        public zxjc_djkjjb_bill_mapper()
        {
            Map(t => t.djkjjbdetail).Ignore();
            Map(t => t.djkjjbdetailhx).Ignore();
            Map(t => t.id).Key(KeyType.TriggerIdentity);
            AutoMap();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.CDGC
{
    public class zxjc_gtjjb_bill
    {
        /// <summary>
        ///ID
        ///</summary>
        public int id { get; set; }
        /// <summary>
        ///日期
        ///</summary>
        public DateTime rq { get; set; }
        /// <summary>
        ///班次（白班、中班、夜班）
        ///</summary>
        public string bc { get; set; }
        /// <summary>
        ///交班人
        ///</summary>
        public string jbr { get; set; }
        /// <summary>
        ///当班组长
        ///</summary>
        public string dbzz { get; set; }
        /// <summary>
        ///上料人员
        ///</summary>
        public string slry { get; set; }
        /// <summary>
        ///打毛刺人员
        ///</summary>
        public string mcry { get; set; }
        /// <summary>
        ///检验人员
        ///</summary>
        public string jyry { get; set; }
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
        public DateTime lrsj { get; set; }
        /// <summary>
        /// 明细
        /// </summary>
        public List<zxjc_gtjjb_bill_detail> mxlist { get; set; }

    }

    public class zxjc_gtjjb_bill_mapper : ClassMapper<zxjc_gtjjb_bill>
    {
        public zxjc_gtjjb_bill_mapper()
        {
            Map(t => t.mxlist).Ignore();
            Map(t => t.id).Key(KeyType.TriggerIdentity);
            AutoMap();
        }
    }
}

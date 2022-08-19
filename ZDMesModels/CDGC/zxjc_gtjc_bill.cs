using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.CDGC
{
    public class zxjc_gtjc_bill
    {
        /// <summary>
        ///流水号
        ///</summary>
        public int id { get; set; }
        /// <summary>
        ///产品类型12.T,1.5T
        ///</summary>
        public string cplx { get; set; }
        /// <summary>
        /// 图号
        /// </summary>
        public string th { get; set; }
        /// <summary>
        ///检验日期
        ///</summary>
        public DateTime rq { get; set; }
        /// <summary>
        ///检测人员
        ///</summary>
        public string jyry { get; set; }
        /// <summary>
        ///检验类别
        ///</summary>
        public string jylb { get; set; }
        /// <summary>
        ///机台号
        ///</summary>
        public string jth { get; set; }
        /// <summary>
        ///二维码
        ///</summary>
        public string vin { get; set; }
        /// <summary>
        ///模号
        ///</summary>
        public string mh { get; set; }
        /// <summary>
        ///评定结果
        ///</summary>
        public string pdjg { get; set; }
        /// <summary>
        ///处理结论
        ///</summary>
        public string cljl { get; set; }
        /// <summary>
        ///处理人
        ///</summary>
        public string clr { get; set; }
        /// <summary>
        ///处理时间
        ///</summary>
        public DateTime clsj { get; set; }
        /// <summary>
        ///录入人
        ///</summary>
        public string lrr { get; set; }
        /// <summary>
        ///录入时间
        ///</summary>
        public DateTime lrsj { get; set; }
        /// <summary>
        /// 检测明细
        /// </summary>
        public List<zxjc_gtjc_detail> zxjcgtjcdetail { get; set; }

    }

    public class zxjc_gtjc_bill_mapper : ClassMapper<zxjc_gtjc_bill>
    {
        public zxjc_gtjc_bill_mapper()
        {
            Map(t => t.id).Key(KeyType.TriggerIdentity);
            Map(t => t.zxjcgtjcdetail).Ignore();
            AutoMap();
        }
    }
}

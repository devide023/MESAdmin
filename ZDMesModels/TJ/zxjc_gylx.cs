using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.TJ
{
    /// <summary>
    /// 工艺路线
    /// </summary>
    public class zxjc_gylx
    {
        /// <summary>
        /// rowid
        /// </summary>
        public string rid { get; set; }
        /// <summary>
        /// 工厂代码
        /// </summary>
        public string gcdm { get; set; }
        /// <summary>
        /// 状态编码
        /// </summary>
        public string ztbm { get; set; }
        /// <summary>
        /// 生产线
        /// </summary>
        public string scx { get; set; }
        /// <summary>
        /// 工序编号
        /// </summary>
        public string workno { get; set; }
        /// <summary>
        /// 工序名称
        /// </summary>
        public string workname { get; set; }
        /// <summary>
        /// 装配顺序
        /// </summary>
        public string zpsx { get; set; }
        /// <summary>
        /// 录入人
        /// </summary>
        public string lrr { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime lrsj { get; set; }
        /// <summary>
        /// 免检
        /// </summary>
        public string mj { get; set; }
        /// <summary>
        /// 审核标志
        /// </summary>
        public string shbz { get; set; }
        /// <summary>
        /// 互锁标志
        /// </summary>
        public string fsbz { get; set; }
        /// <summary>
        /// 状态码
        /// </summary>
        public string statusno { get; set; }

    }

    public class zxjc_gylx_mapper : ClassMapper<zxjc_gylx>
    {
        public zxjc_gylx_mapper()
        {
            Map(t => t.workno).Column("work_no");
            Map(t => t.workname).Column("work_name");
            Map(t => t.statusno).Column("status_no");
            Map(t => t.rid).Ignore();
            AutoMap();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.TJ.A1
{
    /// <summary>
    /// 检测结果
    /// </summary>
    public class zxjc_jcjg
    {
        public string gcdm { get; set; }
        public string scx { get; set; }
        /// <summary>
        /// 岗位编号
        /// </summary>
        public string gwh { get; set; }
        /// <summary>
        /// 检测类型
        /// </summary>
        public string jclx { get; set; }
        /// <summary>
        /// 机号
        /// </summary>
        public string engineno { get; set; }
        /// <summary>
        /// 机型
        /// </summary>
        public string jxno { get; set; }
        /// <summary>
        /// 状态码
        /// </summary>
        public string statusno { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string orderno { get; set; }
        /// <summary>
        /// 装配计划号
        /// </summary>
        public string zpjhh { get; set; }
        /// <summary>
        /// 首末台标识
        /// </summary>
        public string smjbs { get; set; }
        /// <summary>
        /// 检测结果
        /// </summary>
        public string jcjg { get; set; }
        /// <summary>
        /// 录入人
        /// </summary>
        public string lrr { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime lrsj { get; set; }
        /// <summary>
        /// 删除标识
        /// </summary>
        public string scbz { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string bz { get; set; }
        /// <summary>
        /// 评审人
        /// </summary>
        public string psr { get; set; }
        /// <summary>
        /// 评审时间
        /// </summary>
        public DateTime pssj { get; set; }
        public string psr1 { get; set; }
        public DateTime pssj1 { get; set; }
        public string psr2 { get; set; }
        public DateTime pssj2 { get; set; }
        public string psr3 { get; set; }
        public DateTime pssj3 { get; set; }
    }

    public class zxjc_jcjg_mapper : ClassMapper<zxjc_jcjg>
    {
        public zxjc_jcjg_mapper()
        {
            Map(t => t.engineno).Column("engine_no");
            Map(t => t.jxno).Column("jx_no");
            Map(t => t.statusno).Column("status_no");
            Map(t => t.orderno).Column("order_no");
            AutoMap();
        }
    }
}

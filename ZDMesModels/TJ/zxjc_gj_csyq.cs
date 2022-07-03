using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DapperExtensions.Mapper;
namespace ZDMesModels.TJ
{
    /// <summary>
    /// 干检参数
    /// </summary>
    public class zxjc_gj_csyq
    {
        public string rid { get; set; }
        /// <summary>
        /// 状态编码
        /// </summary>
        public string ztbm { get; set; }
        /// <summary>
        /// 干检检测压力上限
        /// </summary>
        public int gjylsx { get; set; }
        /// <summary>
        /// 干检检测压力下限
        /// </summary>
        public int gjylxx { get; set; }
        /// <summary>
        /// 干检平衡时间
        /// </summary>
        public int gjphsj { get; set; }
        /// <summary>
        /// 干检检测时间
        /// </summary>
        public int gjjcsj { get; set; }
        /// <summary>
        /// 干检放气时间
        /// </summary>
        public int gjfqsj { get; set; }
        /// <summary>
        /// 干检泄露值
        /// </summary>
        public decimal gjxlz { get; set; }
        /// <summary>
        /// 干检充气时间
        /// </summary>
        public int gjcqsj { get; set; }
        /// <summary>
        /// 审核标志
        /// </summary>
        public string shbz { get; set; }
        /// <summary>
        /// 录入人
        /// </summary>
        public string lrr { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime lrsj { get; set; }
        /// <summary>
        /// 干检属性
        /// </summary>
        public string gjgs { get; set; }
    }

    public class zxjc_gj_csyq_mapper : ClassMapper<zxjc_gj_csyq>
    {
        public zxjc_gj_csyq_mapper()
        {
            Map(t => t.rid).Ignore();
            AutoMap();
        }
    }
}

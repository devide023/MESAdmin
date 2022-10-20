using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.TJ.A1
{
    public class tj_pp_sc_zpjh
    {
        /// <summary>
        /// 排产计划号
        /// </summary>
        public string zpjhh { get; set; }
        /// <summary>
        /// 生产订单号
        /// </summary>
        public string order_no { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string scx { get; set; }
        /// <summary>
        /// 生产订单顺序号
        /// </summary>
        public string xh { get; set; }
        /// <summary>
        /// 具体装配工作中心
        /// </summary>
        public string zpx { get; set; }
        /// <summary>
        /// 生产序号
        /// </summary>
        public string scxh { get; set; }
        /// <summary>
        /// 生产班次
        /// </summary>
        public string scbc { get; set; }
        /// <summary>
        /// 生产订单数量
        /// </summary>
        public string scsl { get; set; }
        /// <summary>
        /// 生产 计划时间
        /// </summary>
        public DateTime? scsj { get; set; }
        /// <summary>
        /// 装配计划数量
        /// </summary>
        public string zpsl { get; set; }
        /// <summary>
        /// 装配时间
        /// </summary>
        public DateTime? zpsj { get; set; }
        /// <summary>
        /// 上线数量
        /// </summary>
        public string sxsl { get; set; }
        /// <summary>
        /// 合格数量
        /// </summary>
        public string hgsl { get; set; }
        /// <summary>
        /// 返修数量
        /// </summary>
        public string fxsl { get; set; }
        /// <summary>
        /// 未下线数量
        /// </summary>
        public string wxssl { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string bz { get; set; }
        /// <summary>
        /// 状态编码
        /// </summary>
        public string ztbm { get; set; }
        /// <summary>
        /// 机型
        /// </summary>
        public string jx { get; set; }
        /// <summary>
        /// 装配订单状态
        /// </summary>
        public string status_flag { get; set; }
        /// <summary>
        /// 客户编码
        /// </summary>
        public string csbm { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string khmc { get; set; }
        /// <summary>
        /// 销售备注
        /// </summary>
        public string xsbz { get; set; }
        /// <summary>
        /// 计通单号
        /// </summary>
        public string jtdh { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? jssj { get; set; }
        /// <summary>
        /// 销售计划号
        /// </summary>
        public string jhh { get; set; }
        /// <summary>
        /// 差缺原因
        /// </summary>
        public string cqyy { get; set; }
    }
}

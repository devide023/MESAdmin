using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.Ducar
{
    /// <summary>
    /// 生产计划
    /// </summary>
    public class pp_zpjh
    {
        /// <summary>
        /// 计划号
        /// </summary>
        public string zpjhh { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string order_no { get; set; }
        /// <summary>
        /// 生产线
        /// </summary>
        public string scx { get; set; }
        public string xh { get; set; }
        /// <summary>
        /// 订单类型
        /// </summary>
        public string scddlx { get; set; }
        /// <summary>
        /// 生产数量
        /// </summary>
        public int scsl { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string scsj { get; set; }
        /// <summary>
        /// 计划时间
        /// </summary>
        public DateTime jhsj { get; set; }
        /// <summary>
        /// 状态编码
        /// </summary>
        public string ztbm { get; set; }
        /// <summary>
        /// 机型
        /// </summary>
        public string jx { get; set; }
        public string first_flag { get; set; }
        /// <summary>
        /// 删除标志
        /// </summary>
        public string scbz { get; set; }
        public string zt { get; set; }
        /// <summary>
        /// 客户代码
        /// </summary>
        public string khdm { get; set; }
        public string khpch { get; set; }
        public string khlsh { get; set; }
        /// <summary>
        /// 销售计划号
        /// </summary>
        public string jhh { get; set; }
        public string xshh { get; set; }
        /// <summary>
        /// 销售备注
        /// </summary>
        public string xsbz { get; set; }
        public string xssx { get; set; }
        public string jtdh { get; set; }
        public string yzpsl { get; set; }
        public string jdcqyy { get; set; }
        public string cqyy { get; set; }
        public string cqyy_lrsj { get; set; }
        public string jypyb { get; set; }
        public string jypyb2 { get; set; }
        public string cpyhjg { get; set; }
        public string yhph { get; set; }
        public string udcg { get; set; }
        public string sapzt { get; set; }
        public string gc { get; set; }
        public string lrsj { get; set; }
        public string yjhh { get; set; }
        public string yxshh { get; set; }
        public string zjxxsl { get; set; }
        public string bzxxsl { get; set; }
        public string rksl { get; set; }
        public string cksl { get; set; }
        public string yksl { get; set; }
        public string zjxxsl_sj { get; set; }
        public string bzxxsl_sj { get; set; }
        public string rksl_sj { get; set; }
        public string cksl_sj { get; set; }
        public string scrq { get; set; }
        public string yksl_sj { get; set; }
        public string shjzt { get; set; }
        public string lshsc { get; set; }
        public string jhlb { get; set; }
        public string plan_type { get; set; }
        public string tsdz { get; set; }
        public string sc_jhc { get; set; }
        public string sc_fxh { get; set; }
        public string qslsh { get; set; }
        public string jslsh { get; set; }
        public string write_req { get; set; }
        public string seq_length { get; set; }
        public string non_series { get; set; }
        public string js_qsh { get; set; }
        public string js_jsh { get; set; }
        public string sjscrq { get; set; }
        public string xsfhrq { get; set; }
        public string ddcjsj { get; set; }
        public string ddcjrq { get; set; }
        public string ddshsj { get; set; }
        public string ddshrq { get; set; }
        public string pxbj { get; set; }
        public string jypyb_sl { get; set; }
        public string jypyb2_sl { get; set; }
        public string sj_scrq { get; set; }
        public string jd_bm { get; set; }
        public string jd_bz { get; set; }
        public string write_flg { get; set; }
        public string bsxcpm { get; set; }
        public string yqjhrq { get; set; }
        public string cljhrq { get; set; }
        public string ychf { get; set; }
        public string write_exc { get; set; }
        public string dcyy { get; set; }
        public string zrbm { get; set; }
        public string yqscsj { get; set; }
        public string jj_bj { get; set; }
        public string jj_bz { get; set; }
        public string csmc { get; set; }
        public string bjmc { get; set; }
        public string yjscrq { get; set; }
        public string jd_lrsj { get; set; }
        public string zhtbsj { get; set; }
        public zxjc_order_jy orderjy { get; set; }
    }
}

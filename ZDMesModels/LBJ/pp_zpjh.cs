using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.LBJ
{
    public class pp_zpjh
    {
        /// <summary>
        ///工厂
        ///</summary>
        public string gcdm { get; set; }
        /// <summary>
        ///排产计划号
        ///</summary>
        public string zpjhh { get; set; }
        /// <summary>
        ///生产订单号
        ///</summary>
        public string orderno { get; set; }
        /// <summary>
        ///工作中心
        ///</summary>
        public string scx { get; set; }
        /// <summary>
        ///生产顺序号
        ///</summary>
        public string xh { get; set; }
        /// <summary>
        ///生产订单类型
        ///</summary>
        public string scddlx { get; set; }
        /// <summary>
        ///生产数量
        ///</summary>
        public string scsl { get; set; }
        /// <summary>
        ///生产时间
        ///</summary>
        public string scsj { get; set; }
        /// <summary>
        ///计划完成日期
        ///</summary>
        public DateTime jhsj { get; set; }
        /// <summary>
        ///状态编码
        ///</summary>
        public string ztbm { get; set; }
        /// <summary>
        ///机型
        ///</summary>
        public string jx { get; set; }
        /// <summary>
        ///初装标志
        ///</summary>
        public string firstflag { get; set; }
        /// <summary>
        ///生产备注
        ///</summary>
        public string scbz { get; set; }
        /// <summary>
        ///处理状态
        ///</summary>
        public string zt { get; set; }
        /// <summary>
        ///SAP状态
        ///</summary>
        public string sapzt { get; set; }
        /// <summary>
        ///客户代码
        ///</summary>
        public string khdm { get; set; }
        /// <summary>
        ///客户批次号
        ///</summary>
        public string khpch { get; set; }
        /// <summary>
        ///销售订单号
        ///</summary>
        public string jhh { get; set; }
        /// <summary>
        ///销售订单行号
        ///</summary>
        public string xshh { get; set; }
        /// <summary>
        ///销售备注
        ///</summary>
        public string xsbz { get; set; }
        /// <summary>
        ///订单原因
        ///</summary>
        public string xssx { get; set; }
        /// <summary>
        ///成品验货结果
        ///</summary>
        public string cpyhjg { get; set; }
        /// <summary>
        ///检验批次号
        ///</summary>
        public string yhph { get; set; }
        /// <summary>
        ///录入人
        ///</summary>
        public string lrr { get; set; }
        /// <summary>
        ///录入时间
        ///</summary>
        public DateTime lrsj { get; set; }
        /// <summary>
        ///上线数量
        ///</summary>
        public string sxsl { get; set; }
        /// <summary>
        ///下线数量
        ///</summary>
        public string xxsl { get; set; }
        /// <summary>
        ///上线时间
        ///</summary>
        public DateTime sxsj { get; set; }
        /// <summary>
        ///下线时间
        ///</summary>
        public DateTime xxsj { get; set; }
        /// <summary>
        ///毛坯发货数量
        ///</summary>
        public decimal mpfhsl { get; set; }
        /// <summary>
        ///生产部门
        ///</summary>
        public string scbm { get; set; }
        /// <summary>
        ///班次
        ///</summary>
        public string bc { get; set; }
        /// <summary>
        ///审核标记
        ///</summary>
        public string shbj { get; set; }
        /// <summary>
        ///审核人
        ///</summary>
        public string shr { get; set; }
        /// <summary>
        ///审核时间
        ///</summary>
        public DateTime shsj { get; set; }
        /// <summary>
        ///反审核人
        ///</summary>
        public string fshr { get; set; }
        /// <summary>
        ///反审核时间
        ///</summary>
        public DateTime fshsj { get; set; }
        /// <summary>
        ///加工日期
        ///</summary>
        public DateTime jgrq { get; set; }
        /// <summary>
        ///压铸班次
        ///</summary>
        public string ztprog { get; set; }
        /// <summary>
        ///压铸压机号
        ///</summary>
        public string zequp { get; set; }
        /// <summary>
        ///压铸模号
        ///</summary>
        public string zmould { get; set; }
        /// <summary>
        ///压铸模具号1
        ///</summary>
        public string zemold { get; set; }
        /// <summary>
        ///压铸模具号2
        ///</summary>
        public string zname { get; set; }
        /// <summary>
        ///压铸熔杯号
        ///</summary>
        public string zrbh { get; set; }
        /// <summary>
        ///原生产订单数量
        ///</summary>
        public string scsl1 { get; set; }
        /// <summary>
        ///未生产完排产标记
        ///</summary>
        public string pcbj { get; set; }
        /// <summary>
        ///半成品sap生产订单创建标记
        ///</summary>
        public string sapbj { get; set; }
        /// <summary>
        ///压射次数
        ///</summary>
        public string ycsl { get; set; }
        /// <summary>
        ///半成品毛坯数量
        ///</summary>
        public string bcpsl { get; set; }
        /// <summary>
        ///原动力生产订单号
        ///</summary>
        public string ordernodl { get; set; }
        /// <summary>
        ///
        ///</summary>
        public DateTime tecosj { get; set; }
    }

    public class pp_zpjh_mapper : ClassMapper<pp_zpjh>
    {
        public pp_zpjh_mapper()
        {
            Map(t => t.orderno).Column("order_no");
            Map(t => t.firstflag).Column("first_flag");
            Map(t => t.sxsj).Column("sx_sj");
            Map(t => t.xxsj).Column("xx_sj");
            Map(t => t.bcpsl).Column("bcp_sl");
            Map(t => t.mpfhsl).Column("mp_fhsl");
            Map(t => t.ordernodl).Column("order_no_dl");
            Map(t => t.tecosj).Column("teco_sj");
            AutoMap();
        }
    }
}

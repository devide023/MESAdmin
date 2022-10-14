using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.TJ.A1
{
    public class zxjc_gwzd_wljl
    {
           /// <summary>
        /// GUID
        /// </summary>
        public string id { get; set; }
               /// <summary>
        /// 工厂
        /// </summary>
        public string gcdm { get; set; }
               /// <summary>
        /// 生产订单号
        /// </summary>
        public string orderno { get; set; }
               /// <summary>
        /// 生产线
        /// </summary>
        public string scx { get; set; }
               /// <summary>
        /// 岗位编码
        /// </summary>
        public string gwh { get; set; }
               /// <summary>
        /// 机号
        /// </summary>
        public string engineno { get; set; }
               /// <summary>
        /// 装配物料编码
        /// </summary>
        public string wlbm { get; set; }
               /// <summary>
        /// 装配厂商编码
        /// </summary>
        public string csbm { get; set; }
               /// <summary>
        /// 装配批次号
        /// </summary>
        public string batch { get; set; }
               /// <summary>
        /// 装配组别号
        /// </summary>
        public string zbh { get; set; }
               /// <summary>
        /// 采购订单号
        /// </summary>
        public string cgjhh { get; set; }
               /// <summary>
        /// 采购订单行号
        /// </summary>
        public int xh { get; set; }
               /// <summary>
        /// 采购厂商
        /// </summary>
        public string csbm2 { get; set; }
               /// <summary>
        /// 是否锁定
        /// </summary>
        public string sfsd { get; set; }
               /// <summary>
        /// 单台用量
        /// </summary>
        public int dtyl { get; set; }
               /// <summary>
        /// 扫描分类（1机号、2部装件、3普通物料、4厂商编码+物料编码  5厂商编码&物料编码&批次号&组别  6采购订单号+行号  7其它, 0错误）
        /// </summary>
        public string smfl { get; set; }
               /// <summary>
        /// 扫描来源（1厂商物料，2采购订单号，3批次号）
        /// </summary>
        public string smly { get; set; }
               /// <summary>
        /// 检测人
        /// </summary>
        public string lrr { get; set; }
               /// <summary>
        /// 检测时间
        /// </summary>
        public DateTime lrsj { get; set; }
            }	

	public class zxjc_gwzd_wljl_mapper : ClassMapper<zxjc_gwzd_wljl>
    {
        public zxjc_gwzd_wljl_mapper()
        {
                      Map(t => t.orderno).Column("order_no");
                        Map(t => t.engineno).Column("engine_no");
                        AutoMap();
        }
    }
}

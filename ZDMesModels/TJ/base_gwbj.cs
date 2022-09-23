using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.TJ
{
    public class base_gwbj
    {
           /// <summary>
        /// 工作中心2
        /// </summary>
        public string gzzx { get; set; }
               /// <summary>
        /// 岗位编码
        /// </summary>
        public string gwh { get; set; }
               /// <summary>
        /// 物料编码
        /// </summary>
        public string wlbm { get; set; }
               /// <summary>
        /// 料箱判断(N不验证 Y首台 A每台)
        /// </summary>
        public string lxpd { get; set; }
               /// <summary>
        /// 是否打印
        /// </summary>
        public string sfdy { get; set; }
               /// <summary>
        /// 料箱类型
        /// </summary>
        public string lxlx { get; set; }
               /// <summary>
        /// 单箱数量
        /// </summary>
        public int dxsl { get; set; }
               /// <summary>
        /// 物料属性（A大件, B小件）
        /// </summary>
        public string wlsx { get; set; }
               /// <summary>
        /// 备注
        /// </summary>
        public string bz { get; set; }
               /// <summary>
        /// 录入人
        /// </summary>
        public string lrr { get; set; }
               /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime lrsj { get; set; }
               /// <summary>
        /// 扫描周期
        /// </summary>
        public int smzq { get; set; }
               /// <summary>
        /// 工厂
        /// </summary>
        public string gcdm { get; set; }
               /// <summary>
        /// 工作中心
        /// </summary>
        public string scx { get; set; }
               /// <summary>
        /// 岗位配比
        /// </summary>
        public int gwpb { get; set; }
               /// <summary>
        /// 前五位编码
        /// </summary>
        public string qwwbm { get; set; }
               /// <summary>
        /// 暂存区料道号
        /// </summary>
        public string zcqld { get; set; }
               /// <summary>
        /// 配送方式
        /// </summary>
        public string psfs { get; set; }
               /// <summary>
        /// 辊道编号
        /// </summary>
        public string gdbh { get; set; }
               /// <summary>
        /// 辊道层
        /// </summary>
        public string gdc { get; set; }
               /// <summary>
        /// 最高线边库
        /// </summary>
        public int zgkc { get; set; }
               /// <summary>
        /// 机型
        /// </summary>
        public string jxno { get; set; }
            }	

	public class base_gwbj_mapper : ClassMapper<base_gwbj>
    {
        public base_gwbj_mapper()
        {
                      Map(t => t.jxno).Column("jx_no");
                        AutoMap();
        }
    }
}

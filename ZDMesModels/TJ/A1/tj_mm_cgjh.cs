using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.TJ.A1
{
    public class tj_mm_cgjh
    {
        /// <summary>
        /// 生产订单号
        /// </summary>
        public string order_no { get; set; }
        /// <summary>
        /// 部件编码
        /// </summary>
        public string bjbm { get; set; }
        /// <summary>
        /// 部件属性
        /// </summary>
        public string bjsx { get; set; }
        /// <summary>
        /// 装配类型
        /// </summary>
        public string zplx { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        public string wlmc { get; set; }
        /// <summary>
        /// 采购数量
        /// </summary>
        public decimal cgsl { get; set; }
        /// <summary>
        /// 到货数量
        /// </summary>
        public decimal dhsl { get; set; }
        /// <summary>
        /// 厂商编码
        /// </summary>
        public string csbm { get; set; }
        /// <summary>
        /// 厂商名称
        /// </summary>
        public string csmc { get; set; }
        /// <summary>
        /// 到货时间
        /// </summary>
        public DateTime? sjdhsj { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? cjrq { get; set; }


    }
}

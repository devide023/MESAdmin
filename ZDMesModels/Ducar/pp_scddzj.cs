using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.Ducar
{
    /// <summary>
    /// 生产订单组件
    /// </summary>
    public class pp_scddzj
    {
        /// <summary>
        /// 计划号
        /// </summary>
        public string pcjhh { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string scddh { get; set; }
        public string gc { get; set; }
        public string ylh { get; set; }
        public string ylxmh { get; set; }
        /// <summary>
        /// 组件物料
        /// </summary>
        public string zjwl { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        public string wlmc { get; set; }
        /// <summary>
        /// 组件数量
        /// </summary>
        public string zjsl { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string zjdw { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string fjck { get; set; }
        /// <summary>
        /// 装配类型
        /// </summary>
        public string zplx { get; set; }
        public string pc { get; set; }
        /// <summary>
        /// 供应商代码
        /// </summary>
        public string gys { get; set; }
        public string xmwb { get; set; }
        /// <summary>
        /// 删除标志
        /// </summary>
        public string scbz { get; set; }
        public string pxzfc { get; set; }
        public string supl_lock { get; set; }
    }
}

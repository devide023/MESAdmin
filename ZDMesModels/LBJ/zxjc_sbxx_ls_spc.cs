using System;
using System.Collections.Generic;
using System.Linq;
namespace ZDMesModels.LBJ
{
    /// <summary>
    /// 设备参数信息流水表
    ///</summary>
    public class zxjc_sbxx_ls_spc
    {
        /// <summary>
        /// 设备编号（资产号） 
        ///</summary>
         public string sbbh { get; set; }
        /// <summary>
        /// 设备名称 
        ///</summary>
         public string sbmc { get; set; }
        /// <summary>
        /// 工厂 
        ///</summary>
         public string gcdm { get; set; }
        /// <summary>
        /// 生产线 
        ///</summary>
         public string scx { get; set; }
        /// <summary>
        /// 岗位号 
        ///</summary>
         public string gwh { get; set; }
        /// <summary>
        /// 开机状态
        /// </summary>
        public string kjzt { get; set; }
        /// <summary>
        /// 运行状态
        /// </summary>
        public string yxzt { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public string lrsj { get; set; }
        /// <summary>
        /// ID
        /// </summary>
        public string id { get; set; }
    }
}

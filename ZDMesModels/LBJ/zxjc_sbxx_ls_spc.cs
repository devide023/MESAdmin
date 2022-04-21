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
        /// 主轴实时转速、清洗温度、烘干温度 
        ///</summary>
         public string data1 { get; set; }
        /// <summary>
        /// 主轴实时进给、清洗压力 
        ///</summary>
         public string data2 { get; set; }
        /// <summary>
        /// 主轴实时负载 
        ///</summary>
         public string data3 { get; set; }
        /// <summary>
        /// 备用 
        ///</summary>
         public string data4 { get; set; }
        /// <summary>
        /// 备用 
        ///</summary>
         public string data5 { get; set; }
        /// <summary>
        /// 备用 
        ///</summary>
         public string data6 { get; set; }
        /// <summary>
        /// 备用 
        ///</summary>
         public string data7 { get; set; }
        /// <summary>
        /// 备用 
        ///</summary>
         public string data8 { get; set; }
        /// <summary>
        /// 备用 
        ///</summary>
         public string data9 { get; set; }
        /// <summary>
        /// 备用 
        ///</summary>
         public string data10 { get; set; }
        /// <summary>
        /// 数据采集时间 
        ///</summary>
         public DateTime? lrsj { get; set; }
    }
}

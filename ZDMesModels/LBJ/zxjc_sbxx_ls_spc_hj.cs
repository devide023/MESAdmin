using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.LBJ
{
    public class zxjc_sbxx_ls_spc_hj
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
        /// 温度
        /// </summary>
        public string wd { get; set; }
        /// <summary>
        /// 湿度
        /// </summary>
        public string sd { get; set; }
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

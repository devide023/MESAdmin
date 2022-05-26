using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.LBJ
{
    public class zxjc_sbxx_ls_ylj
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
        /// 风机状态
        /// </summary>
        public string fjzt { get; set; }
        /// <summary>
        /// 液泵状态
        /// </summary>
        public string ybzt { get; set; }
        /// <summary>
        /// 报警状态
        /// </summary>
        public string bjzt { get; set; }
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

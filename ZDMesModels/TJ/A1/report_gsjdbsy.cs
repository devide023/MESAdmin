using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.TJ.A1
{
    /// <summary>
    /// 干水检对比试验
    /// </summary>
    public class report_gsjdbsy
    {
        /// <summary>
        /// 生产线
        /// </summary>
        public string scx { get; set; }
        /// <summary>
        /// 机型
        /// </summary>
        public string jxno { get; set; }
        /// <summary>
        /// 机号
        /// </summary>
        public string vin { get; set; }
        /// <summary>
        /// 干检参数
        /// </summary>
        public string gjcs { get; set; }
        /// <summary>
        /// 干检结果
        /// </summary>
        public string gjjg { get; set; }
        /// <summary>
        /// 干检时间
        /// </summary>
        public DateTime  gjsj { get; set; }
        /// <summary>
        /// 干检员
        /// </summary>
        public string gjry { get; set; }
        /// <summary>
        /// 水检结果
        /// </summary>
        public string sjjg { get; set; }
        /// <summary>
        /// 水检员
        /// </summary>
        public string sjry { get; set; }
        /// <summary>
        /// 水检时间
        /// </summary>
        public DateTime sjsj { get; set; }
        /// <summary>
        /// 原因
        /// </summary>
        public string yy { get; set; }
        /// <summary>
        /// 处理意见
        /// </summary>
        public string clyj { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.TJ.A1
{
    /// <summary>
    /// 检测结果
    /// </summary>
    public class report_jcjg
    {
        /// <summary>
        /// 流水编号
        /// </summary>
        public int rowno { get; set; }
        /// <summary>
        /// 机型
        /// </summary>
        public string jxno { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string statusno { get; set; }
        /// <summary>
        /// 首台机号
        /// </summary>
        public string firstvin { get; set; }
        /// <summary>
        /// 末台机号
        /// </summary>
        public string lastvin { get; set; }
        /// <summary>
        /// 检测类别
        /// </summary>
        public string jclb { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int xh { get; set; }
        /// <summary>
        /// 检测要求
        /// </summary>
        public string jcyq { get; set; }
        /// <summary>
        /// 首台记录
        /// </summary>
        public string firstjcjg { get; set; }
        /// <summary>
        /// 末台记录
        /// </summary>
        public string lastjcjg { get; set; }
        /// <summary>
        /// 单项判定
        /// </summary>
        public string dxpd { get; set; }
    }
}

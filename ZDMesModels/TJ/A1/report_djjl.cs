using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.TJ.A1
{
    /// <summary>
    /// 点检记录表
    /// </summary>
    public class report_djjl
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int rowno { get; set; }
        /// <summary>
        /// 生产线
        /// </summary>
        public string scx { get; set; }
        public DateTime rq { get; set; }
        /// <summary>
        /// 岗位名称
        /// </summary>
        public string gwmc { get; set; }
        /// <summary>
        /// 点检项目
        /// </summary>
        public string jclb { get; set; }
        /// <summary>
        /// 点检内容
        /// </summary>
        public string jcyq { get; set; }
        /// <summary>
        /// 点检时间
        /// </summary>
        public string jcd { get; set; }
        /// <summary>
        /// 点检结果
        /// </summary>
        public string jcjg { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string bz { get; set; }
    }
}

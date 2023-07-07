using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.TJ.A1
{
    /// <summary>
    /// 毛巾更换记录
    /// </summary>
    public class report_mjghjl
    {
        public int rowno { get; set; }
        public DateTime rq { get; set; }
        public string gwmc { get; set; }
        /// <summary>
        /// 开始机号
        /// </summary>
        public string ksjh { get; set; }
        /// <summary>
        /// 结束机号
        /// </summary>
        public string jsjh { get; set; }
        /// <summary>
        /// 岗位人员
        /// </summary>
        public string gwry { get; set; }
        /// <summary>
        /// 巡检确认
        /// </summary>
        public string xjqr { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string bz { get; set; }
    }
}

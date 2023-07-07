using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.TJ.A1
{
    public class sys_app_jc_form
    {
        /// <summary>
        /// 生产线
        /// </summary>
        public string scx { get; set; }
        /// <summary>
        /// 检测类型
        /// </summary>
        public string jclx { get; set; }
        /// <summary>
        /// 检测时间
        /// </summary>
        public DateTime? jcrq { get; set; }
        /// <summary>
        /// 资产编号
        /// </summary>
        public string zcbh { get; set; }
        /// <summary>
        /// 设备管理员
        /// </summary>
        public string sbgly { get; set; }
        /// <summary>
        /// 总检测结果
        /// </summary>
        public string zjcjg { get; set; }
        /// <summary>
        /// 录入人
        /// </summary>
        public string lrr { get; set; }
        /// <summary>
        /// 监督确认人
        /// </summary>
        public string jdqr { get; set; }
        public string jcxid { get; set; }
        public string jcjg { get; set; }
        public string jcz { get; set; }
    }
}

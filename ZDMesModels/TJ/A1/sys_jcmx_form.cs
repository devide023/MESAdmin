using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.TJ.A1
{
    public class sys_jcmx_form
    {
        /// <summary>
        /// 生产线
        /// </summary>
        public string scx { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string zcbh { get; set; }
        /// <summary>
        /// 检测类型
        /// </summary>
        public string jclx { get; set; }
        /// <summary>
        /// 班次（班前、班中、班后）
        /// </summary>
        public string bc { get; set; }
        /// <summary>
        /// 录入人
        /// </summary>
        public string lrr { get; set; }
    }
}

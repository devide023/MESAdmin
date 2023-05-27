using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.TJ.A1
{
    /// <summary>
    /// 返修记录表
    /// </summary>
    public class report_fxjlb
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
        /// 返修类型
        /// </summary>
        public string fxlx { get; set; }
        /// <summary>
        /// 机号
        /// </summary>
        public string vin { get; set; }
        /// <summary>
        /// 故障描述
        /// </summary>
        public string faultname { get; set; }
        /// <summary>
        /// 原因分析
        /// </summary>
        public string yyfx { get; set; }
        /// <summary>
        /// 零部件名称
        /// </summary>
        public string wlmc { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public string gys { get; set; }
        /// <summary>
        /// 处理方式
        /// </summary>
        public string clfs { get; set; }
        /// <summary>
        /// 返修人
        /// </summary>
        public string fxr { get; set; }
        /// <summary>
        /// 检测人
        /// </summary>
        public string jcry { get; set; }
        /// <summary>
        /// 检测时间
        /// </summary>
        public DateTime jcsj { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string bz { get; set; }
    }
}

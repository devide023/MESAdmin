using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.LBJ
{
    /// <summary>
    /// 维保周期过滤表单
    /// </summary>
    public class sys_wbzq_gl_form
    {
        /// <summary>
        /// 生产线
        /// </summary>
        public string scx { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public List<string> sbbh { get; set; }
    }
}

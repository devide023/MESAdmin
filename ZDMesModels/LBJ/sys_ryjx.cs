using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.LBJ
{
    /// <summary>
    /// 员工绩效
    /// </summary>
    public class sys_ryjx
    {
        /// <summary>
        /// 生产线
        /// </summary>
        public string scx { get; set; }
        /// <summary>
        /// 岗位号
        /// </summary>
        public string gwh { get; set; }
        /// <summary>
        /// 总加工数
        /// </summary>
        public int zjgs { get; set; }
        /// <summary>
        /// 工料废
        /// </summary>
        public int glf { get; set; }
        /// <summary>
        /// 待定
        /// </summary>
        public int dd { get; set; }
        /// <summary>
        /// 打包数
        /// </summary>
        public int dbs { get; set; }
        /// <summary>
        /// 合格率
        /// </summary>
        public decimal hgl { get; set; }
    }
}

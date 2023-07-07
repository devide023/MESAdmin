using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.TJ.A1
{
    /// <summary>
    /// 检测明细
    /// </summary>
    public class zxjc_jcmx
    {
        public string id { get; set; }
        /// <summary>
        /// 检测项id
        /// </summary>
        public string jcxid { get; set; }
        public string gcdm { get; set; }
        public string scx { get; set; }
        public string gwh { get; set; }
        /// <summary>
        /// 检测类型
        /// </summary>
        public string jclx { get; set; }
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
        /// 标准值
        /// </summary>
        public string bzz { get; set; }
        /// <summary>
        /// 标准上限
        /// </summary>
        public string bzsx { get; set; }
        /// <summary>
        /// 标准下限
        /// </summary>
        public string bzxx { get; set; }
        /// <summary>
        /// 检测结果
        /// </summary>
        public string jcjg { get; set; }
        /// <summary>
        /// 检测值
        /// </summary>
        public string jcz { get; set; }
        /// <summary>
        /// 首末件标识
        /// </summary>
        public string smjbs { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string bz { get; set; }
        public string lrr { get; set; }
        public DateTime lrsj { get; set; }
        /// <summary>
        /// 删除标志
        /// </summary>
        public string scbz { get; set; }
        public string jcjg_id { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.TJ.A1
{
    public class zxjc_qzcdjc
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
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 机号
        /// </summary>
        public string vin { get; set; }
        /// <summary>
        /// 测试值（mm）
        /// </summary>
        public string a { get; set; }
        /// <summary>
        /// 测试值（mm）
        /// </summary>
        public string b { get; set; }
        /// <summary>
        /// 合箱垫厚度
        /// </summary>
        public string c { get; set; }
        /// <summary>
        /// 等高块厚度
        /// </summary>
        public string d { get; set; }
        /// <summary>
        /// 窜动间隙
        /// </summary>
        public string e { get; set; }
        /// <summary>
        /// 录入人
        /// </summary>
        public string lrr { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime lrsj { get; set; }
        /// <summary>
        /// 工厂代码
        /// </summary>
        public string gcdm { get; set; }
        /// <summary>
        /// 检测结果
        /// </summary>
        public string jcjg { get; set; }
    }
}

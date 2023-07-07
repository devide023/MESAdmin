using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.LBJ
{
    public class zxjc_base_check
    {
        public int id { get; set; }
        /// <summary>
        /// 产品方位
        /// </summary>
        public string cpfw { get; set; }
        /// <summary>
        /// 产品型号
        /// </summary>
        public string cpxh { get; set; }
        /// <summary>
        /// 图号
        /// </summary>
        public string th { get; set; }
        /// <summary>
        /// 检测项目
        /// </summary>
        public string jcxm { get; set; }
        /// <summary>
        /// 检测频次
        /// </summary>
        public string jcpc { get; set; }
        /// <summary>
        /// 检测工具
        /// </summary>
        public string jcgj { get; set; }
        /// <summary>
        /// 检测值
        /// </summary>
        public string jcz { get; set; }
        /// <summary>
        /// 下限
        /// </summary>
        public string jcxx { get; set; }
        /// <summary>
        /// 上限
        /// </summary>
        public string jcsx { get; set; }
        /// <summary>
        /// 输入类型
        /// </summary>
        public string srlx { get; set; }
        /// <summary>
        /// 权重
        /// </summary>
        public int seq { get; set; }
        /// <summary>
        /// 录入人
        /// </summary>
        public string lrr { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime lrsj { get; set; } = DateTime.Now;
        /// <summary>
        /// 检测项值
        /// </summary>
        public string checkval { get; set; }
        /// <summary>
        /// 检查项图片
        /// </summary>
        public List<string> checkimages { get; set;}
    }

    public class zxjc_base_check_mapper : ClassMapper<zxjc_base_check>
    {
        public zxjc_base_check_mapper()
        {
            Map(t => t.id).Key(KeyType.Assigned);
            Map(t => t.checkval).Ignore();
            Map(t => t.checkimages).Ignore();
            AutoMap();
        }
    }
}

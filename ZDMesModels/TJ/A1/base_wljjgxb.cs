using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.TJ.A1
{
    /// <summary>
    /// 物料检具关系表
    /// </summary>
    public class base_wljjgxb
    {
        public string id { get; set; }=Guid.NewGuid().ToString();
        /// <summary>
        /// 物料编码
        /// </summary>
        public string wlbm { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        public string wlmc { get; set; }
        /// <summary>
        /// 检具编号
        /// </summary>
        public string jjbh { get; set; }
        /// <summary>
        /// 录入人
        /// </summary>
        public string lrr { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime lrsj { get; set; } = DateTime.Now;
    }

    public class base_wljjgxb_mapper : ClassMapper<base_wljjgxb>
    {
        public base_wljjgxb_mapper()
        {
            Map(t => t.id).Key(KeyType.Assigned);
            AutoMap();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.Ducar
{
    /// <summary>
    /// 故障分类信息
    /// </summary>
    public class zxjc_fault_flxx
    {
        public string id { get; set; }=Guid.NewGuid().ToString();
        /// <summary>
        /// 大类名称
        /// </summary>
        public string dlmc { get; set; }
        /// <summary>
        /// 中类名称
        /// </summary>
        public string zlmc { get; set; }
        /// <summary>
        /// 小类名称
        /// </summary>
        public string xlmc { get; set; }
        /// <summary>
        /// 故障名称
        /// </summary>
        public string faultname { get; set; }
        public string lrr { get; set; }
        public DateTime lrsj { get; set; }=DateTime.Now;
    }

    public class zxjc_fault_flxx_mapper : ClassMapper<zxjc_fault_flxx>
    {
        public zxjc_fault_flxx_mapper()
        {
            Map(t => t.id).Key(KeyType.Assigned);
            AutoMap();
        }
    }
}

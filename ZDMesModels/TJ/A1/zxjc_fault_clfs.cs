using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.TJ.A1
{
    public class zxjc_fault_clfs
    {
        /// <summary>
        /// 工厂
        /// </summary>
        public string gcdm { get; set; }
        /// <summary>
        /// 生产线
        /// </summary>
        public string scx { get; set; }
        /// <summary>
        /// 岗位编码
        /// </summary>
        public string gwh { get; set; }
        /// <summary>
        /// 故障代码
        /// </summary>
        public string faultno { get; set; }
        /// <summary>
        /// 处理方式代码
        /// </summary>
        public string handno { get; set; }
        /// <summary>
        /// 处理方式名称
        /// </summary>
        public string handname { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 录入人
        /// </summary>
        public string lrr { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime lrsj { get; set; }
    }

    public class zxjc_fault_clfs_mapper : ClassMapper<zxjc_fault_clfs>
    {
        public zxjc_fault_clfs_mapper()
        {
            Map(t => t.faultno).Column("fault_no");
            Map(t => t.handno).Column("hand_no");
            Map(t => t.handname).Column("hand_name");
            AutoMap();
        }
    }
}

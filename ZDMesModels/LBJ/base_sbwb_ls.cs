using System;
using System.Collections.Generic;
using System.Linq;
using DapperExtensions.Mapper;
namespace ZDMesModels.LBJ
{
    /// <summary>
    /// 设备维保周期表
    ///</summary>
    public class base_sbwb_ls
    {
        /// <summary>
        /// ID 主键 
        ///</summary>
         public string autoid { get; set; }
        /// <summary>
        /// 工厂 
        ///</summary>
         public string gcdm { get; set; }
        /// <summary>
        /// 生产线 
        ///</summary>
         public string scx { get; set; }
        /// <summary>
        /// 岗位号 
        ///</summary>
         public string gwh { get; set; }
        /// <summary>
        /// 维保顺序 
        ///</summary>
         public decimal wbsh { get; set; }
        /// <summary>
        /// 维保内容 
        ///</summary>
         public string wbxx { get; set; }
        /// <summary>
        /// 维保计划时间 
        ///</summary>
         public DateTime? wbjhsj { get; set; }
        /// <summary>
        /// 维保状态 计划中 已完成 已取消 
        ///</summary>
         public string wbzt { get; set; }
        /// <summary>
        /// 维保完成时间 
        ///</summary>
         public DateTime? wbwcsj { get; set; }
        /// <summary>
        /// 维保完成人 
        ///</summary>
         public string wbwcr { get; set; }
        /// <summary>
        /// 录入人 
        ///</summary>
         public string lrr { get; set; }
        /// <summary>
        /// 录入时间 
        ///</summary>
         public DateTime? lrsj { get; set; }
        /// <summary>
        /// 是否维保
        /// </summary>
        public string sfwb { get; set; }
    }

    public class base_sbwb_ls_mapper : ClassMapper<base_sbwb_ls>
    {
        public base_sbwb_ls_mapper()
        {
            Map(t => t.autoid).Key(KeyType.Assigned);
            Map(t => t.sfwb).Ignore();
            AutoMap();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using DapperExtensions.Mapper;
namespace ZDMesModels.LBJ
{
    /// <summary>
    /// 设备维保基础信息表
    ///</summary>
    public class base_sbwb
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
        /// 备注 
        ///</summary>
         public string bz { get; set; }
        /// <summary>
        /// 录入人 
        ///</summary>
         public string lrr { get; set; }
        /// <summary>
        /// 记录日期 
        ///</summary>
         public DateTime? lrsj { get; set; }
        /// <summary>
        /// 删除标记（Y N） 
        ///</summary>
         public string scbz { get; set; }
    }

    public class base_sbwb_mapper : ClassMapper<base_sbwb>
    {
        public base_sbwb_mapper()
        {
            Map(t => t.autoid).Key(KeyType.Assigned);
            AutoMap();
        }
    }
}

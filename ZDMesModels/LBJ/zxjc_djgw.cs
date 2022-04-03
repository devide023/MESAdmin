using System;
using System.Collections.Generic;
using System.Linq;
using DapperExtensions.Mapper;
namespace ZDMesModels.LBJ
{
    /// <summary>
    /// 点检基础信息表
    ///</summary>
    public class zxjc_djgw
    {
        /// <summary>
        /// 工厂 
        ///</summary>
         public string gcdm { get; set; }
        /// <summary>
        /// 生产线 
        ///</summary>
         public string scx { get; set; }
        /// <summary>
        /// 岗位编码 
        ///</summary>
         public string gwh { get; set; }
        /// <summary>
        /// 产品状态 
        ///</summary>
         public string statusno { get; set; }
        /// <summary>
        /// 点检编号 
        ///</summary>
         public string djno { get; set; }
        /// <summary>
        /// 点检内容 
        ///</summary>
         public string djxx { get; set; }
        /// <summary>
        /// 删除标志 
        ///</summary>
         public string scbz { get; set; }
        /// <summary>
        /// 录入人 
        ///</summary>
         public string lrr { get; set; }
        /// <summary>
        /// 录入时间 
        ///</summary>
         public DateTime? lrsj { get; set; }
    }

    public class zxjc_djgw_mapper : ClassMapper<zxjc_djgw>
    {
        public zxjc_djgw_mapper()
        {
            Map(t => t.djno).Key(KeyType.Assigned);
            Map(t => t.statusno).Column("status_no");
            AutoMap();
        }
    }
}

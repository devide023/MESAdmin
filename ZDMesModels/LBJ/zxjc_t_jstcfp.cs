using System;
using System.Collections.Generic;
using System.Linq;
using DapperExtensions.Mapper;
namespace ZDMesModels.LBJ
{
    /// <summary>
    /// 技通文件与生产线关系表
    ///</summary>
    public class zxjc_t_jstcfp
    {
        /// <summary>
        ///  Id
        ///</summary>
         public int id { get; set; }
        /// <summary>
        /// 技通文件ID 
        ///</summary>
         public string jtid { get; set; }
        /// <summary>
        /// 工厂 
        ///</summary>
         public string gcdm { get; set; }
        /// <summary>
        /// 生产线（质量班长维护） 
        ///</summary>
         public string scx { get; set; }
        /// <summary>
        /// 岗位号（质量组长维护） 
        ///</summary>
         public string gwh { get; set; }
        /// <summary>
        /// 机型??? （质量班长维护） 
        ///</summary>
         public string jxno { get; set; }
        /// <summary>
        /// 状态码（质量班长维护） 
        ///</summary>
         public string statusno { get; set; }
        /// <summary>
        /// 备注 
        ///</summary>
         public string bz { get; set; }
        /// <summary>
        /// 班长录入人 
        ///</summary>
         public string lrr1 { get; set; }
        /// <summary>
        /// 班长录入时间 
        ///</summary>
         public DateTime? lrsj1 { get; set; }
        /// <summary>
        /// 组长录入人 
        ///</summary>
         public string lrr2 { get; set; }
        /// <summary>
        /// 组长录入时间 
        ///</summary>
         public DateTime? lrsj2 { get; set; }
    }

    public class zxjc_t_jstcfp_mapper : ClassMapper<zxjc_t_jstcfp>
    {
        public zxjc_t_jstcfp_mapper()
        {
            Map(t => t.id).Key(KeyType.Assigned);
            AutoMap();
        }
    }
}

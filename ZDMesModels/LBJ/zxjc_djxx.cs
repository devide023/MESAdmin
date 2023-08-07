using System;
using System.Collections.Generic;
using System.Linq;
using DapperExtensions.Mapper;
namespace ZDMesModels.LBJ
{
    /// <summary>
    /// 点检信息记录表
    ///</summary>
    public class zxjc_djxx
    {
        /// <summary>
        /// 唯一ID(GUID) 
        ///</summary>
         public string id { get; set; }
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
         public List<option_list> gwhs { get; set; }
        /// <summary>
        /// 机型 
        ///</summary>
        public string engineno { get; set; }
        /// <summary>
        /// 状态码 
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
        /// 点检结果(Y/N) 
        ///</summary>
         public string djjg { get; set; }
        /// <summary>
        /// 点检备注 
        ///</summary>
         public string bz { get; set; }
        /// <summary>
        /// 点检人 
        ///</summary>
         public string lrr { get; set; }
        /// <summary>
        /// 点检时间 
        ///</summary>
         public DateTime? lrsj { get; set; }

        public string scxzx { get; set; }
        public List<option_list> scxzxs { get; set; }
    }

   public class zxjc_djxx_mapper : ClassMapper<zxjc_djxx>
    {
        public zxjc_djxx_mapper()
        {
            Map(t => t.id).Key(KeyType.Assigned);
            Map(t => t.statusno).Column("status_no");
            Map(t => t.engineno).Column("engine_no");
            Map(t => t.scxzxs).Ignore();
            Map(t => t.gwhs).Ignore();
            AutoMap();
        }
    }
}

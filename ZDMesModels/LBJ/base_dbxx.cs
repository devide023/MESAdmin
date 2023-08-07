using System;
using System.Collections.Generic;
using System.Linq;
using DapperExtensions.Mapper;
namespace ZDMesModels.LBJ
{
    /// <summary>
    /// 刀柄基础信息表
    ///</summary>
    public class base_dbxx
    {
        public string id { get; set; }
        /// <summary>
        /// 工厂 
        ///</summary>
         public string gcdm { get; set; }
        /// <summary>
        /// 刀柄名称 
        ///</summary>
         public string dbmc { get; set; }
        /// <summary>
        /// 刀柄类型 
        ///</summary>
         public string dblx { get; set; }
        /// <summary>
        /// 刀柄号 
        ///</summary>
         public string dbh { get; set; }
        /// <summary>
        /// 采购时间 
        ///</summary>
         public DateTime? cgsj { get; set; }
        /// <summary>
        /// 录入人 
        ///</summary>
         public string lrr { get; set; }
        /// <summary>
        /// 录入时间 
        ///</summary>
         public DateTime? lrsj { get; set; }
        /// <summary>
        /// 刀柄状态（已报废、使用中、空闲中） 
        ///</summary>
         public string dbzt { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string dbxxbz { get; set; }
        /// <summary>
        /// 刀柄刃具关系
        /// </summary>
        public List<base_dbrjgx> children { get; set; }

        public string label { get; set; }
        /// <summary>
        /// 刀柄是否在线
        /// </summary>
        public int isused { get; set; } = 0;


    }

    public class base_dbxx_mapper : ClassMapper<base_dbxx>
    {
        public base_dbxx_mapper()
        {
            Map(t => t.dbh).Key(KeyType.Assigned);
            Map(t => t.dbxxbz).Column("bz");
            Map(t => t.children).Ignore();
            Map(t => t.label).Ignore();
            Map(t => t.id).Ignore();
            Map(t => t.isused).Ignore();
            AutoMap();
        }
    }
}

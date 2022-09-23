using System;
using System.Collections.Generic;
using System.Linq;
using DapperExtensions.Mapper;
namespace ZDMesModels.LBJ
{
    /// <summary>
    /// 刀柄、刃具对应关系表
    ///</summary>
    public class base_dbrjgx
    {
        /// <summary>
        /// ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 刃具基础信息id
        /// </summary>
        public int rjid { get; set; }
        /// <summary>
        /// 工厂 
        ///</summary>
        public string gcdm { get; set; }
        /// <summary>
        /// 刀柄号 
        ///</summary>
         public string dbh { get; set; }
        /// <summary>
        /// 产品状态 
        ///</summary>
         public string cpzt { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        public string wlmc { get; set; }
        /// <summary>
        /// 匹配刃具类型 
        ///</summary>
        public string djlx { get; set; }
        /// <summary>
        /// 刀柄类型
        /// </summary>
        public string dblx { get; set; }
        /// <summary>
        /// 刃具名称
        /// </summary>
        public string rjmc { get; set; }
        /// <summary>
        /// 加工位置
        /// </summary>
        public string jgwz { get; set; }

        public List<base_dbrjgx> children { get; set; }
        /// <summary>
        /// 刃具信息
        /// </summary>
        public base_rjxx baserjxx { get; set; }
    }

    public class base_dbrjgx_mapper : ClassMapper<base_dbrjgx>
    {
        public base_dbrjgx_mapper()
        {
            Map(t => t.id).Key(KeyType.TriggerIdentity);
            Map(t => t.children).Ignore();
            Map(t => t.baserjxx).Ignore();
            Map(t => t.wlmc).Ignore();
            Map(t => t.rjmc).Ignore();
            Map(t => t.jgwz).Ignore();
            AutoMap();
        }
    }
}

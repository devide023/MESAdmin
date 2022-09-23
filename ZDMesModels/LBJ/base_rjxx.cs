using System;
using System.Collections.Generic;
using System.Linq;
using DapperExtensions.Mapper;
namespace ZDMesModels.LBJ
{
    /// <summary>
    /// 刃具基础信息表
    ///</summary>
    public class base_rjxx
    {
        public int id { get; set; }
        /// <summary>
        /// 工厂 
        ///</summary>
        public string gcdm { get; set; }
        /// <summary>
        /// 刃具类型 
        ///</summary>
        public string rjlx { get; set; }
        /// <summary>
        /// 刃具名称 
        ///</summary>
        public string rjmc { get; set; }
        /// <summary>
        /// 标准寿命 
        ///</summary>
        public int rjbzsm { get; set; }
        /// <summary>
        /// 加工位置
        /// </summary>
        public string jgwz { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string rjxxbz { get; set; }
    }

    public class base_rjxx_mapper : ClassMapper<base_rjxx>
    {
        public base_rjxx_mapper()
        {
            Map(t => t.id).Key(KeyType.TriggerIdentity);
            Map(t => t.rjxxbz).Column("bz");
            AutoMap();
        }
    }
}

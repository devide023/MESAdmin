﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.LBJ
{
    /// <summary>
    /// 刃具寿命消耗
    /// </summary>
    public class base_rjsmxh
    {
        public string rid { get; set; }
        /// <summary>
        /// 工厂 
        ///</summary>
        public string gcdm { get; set; }
        /// <summary>
        /// 刃具类型 
        ///</summary>
        public string rjlx { get; set; }
        /// <summary>
        /// 匹配产品类型 
        ///</summary>
        public string cpzt { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        public string wlmc { get; set; }
        /// <summary>
        /// 生产线 
        ///</summary>
        public string scx { get; set; }
        /// <summary>
        /// 设备号 
        ///</summary>
        public string sbbh { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string sbmc { get; set; }
        /// <summary>
        /// 每件消耗寿命 
        ///</summary>
        public int mjxhsm { get; set; }
    }

    public class base_rjsmxh_mapper:ClassMapper<base_rjsmxh>
    {
        public base_rjsmxh_mapper()
        {
            Map(t => t.rid).Ignore();
            Map(t => t.wlmc).Ignore();
            Map(t => t.sbmc).Ignore();
            AutoMap();
        }
    }
}

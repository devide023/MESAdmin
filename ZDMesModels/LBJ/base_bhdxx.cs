﻿using System;
using System.Collections.Generic;
using System.Linq;
using DapperExtensions.Mapper;
namespace ZDMesModels.LBJ
{
    /// <summary>
    /// 变化点基础信息表
    ///</summary>
    public class base_bhdxx
    {
        /// <summary>
        /// 工厂 
        ///</summary>
         public string gcdm { get; set; }
        /// <summary>
        /// 变化点编号 
        ///</summary>
         public string bhdbh { get; set; }
        /// <summary>
        /// 变化点内容 
        ///</summary>
         public string bhdnr { get; set; }
        /// <summary>
        /// 变化等级 
        ///</summary>
         public string bhddj { get; set; }
        /// <summary>
        /// 识别方式 
        ///</summary>
         public string sbfs { get; set; }
        /// <summary>
        /// 识别条件 
        ///</summary>
         public string sbtj { get; set; }
    }

    public class base_bhdxx_mapper : ClassMapper<base_bhdxx>
    {
        public base_bhdxx_mapper()
        {
            Map(t => t.bhdbh).Key(KeyType.Assigned);
            AutoMap();
        }
    }
}

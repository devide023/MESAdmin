using System;
using System.Collections.Generic;
using System.Linq;
using DapperExtensions.Mapper;
namespace ZDMesModels.LBJ
{
    /// <summary>
    /// 刀柄刃具使用信息流水表
    ///</summary>
    public class base_dbrjzx_ls
    {
        /// <summary>
        /// ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 刃具id
        /// </summary>
        public int rjid { get; set; }
        /// <summary>
        /// 工厂 
        ///</summary>
        public string gcdm { get; set; }
        /// <summary>
        /// 生产线 
        ///</summary>
         public string scx { get; set; }
        /// <summary>
        /// 刀柄号 
        ///</summary>
         public string dbh { get; set; }
        /// <summary>
        /// 设备号 
        ///</summary>
         public string sbbh { get; set; }
        /// <summary>
        /// 刃具类型 
        ///</summary>
         public string djlx { get; set; }
        /// <summary>
        /// 刃具标准寿命 
        ///</summary>
         public int djbzsm { get; set; }
        /// <summary>
        /// 刃具安装时的寿命 
        ///</summary>
         public int djazsm { get; set; }
        /// <summary>
        /// 刃具当前寿命 
        ///</summary>
         public int djdqsm { get; set; }
        /// <summary>
        /// 刃具安装时的设备加工数 
        ///</summary>
         public long djazjgs { get; set; }
        /// <summary>
        /// 当前加工数 
        ///</summary>
         public long dqjgs { get; set; }
        /// <summary>
        /// 刀柄领用时间 
        ///</summary>
         public DateTime? dblysj { get; set; }
        /// <summary>
        /// 刀柄领用人 
        ///</summary>
         public string dblyr { get; set; }
        /// <summary>
        /// 刃具领用时间 
        ///</summary>
         public DateTime? rjlysj { get; set; }
        /// <summary>
        /// 刃具领用人 
        ///</summary>
         public string rjlyr { get; set; }
        /// <summary>
        /// 刃具刃磨次数 
        ///</summary>
         public string rjrmcs { get; set; }
        /// <summary>
        /// 刃具最后刃磨时间 
        ///</summary>
         public DateTime? rjzhrmsj { get; set; }
    }

    public class base_dbrjzx_ls_mapper : ClassMapper<base_dbrjzx_ls>
    {
        public base_dbrjzx_ls_mapper()
        {
            Map(t => t.id).Key(KeyType.TriggerIdentity);
            AutoMap();
        }
    }
}

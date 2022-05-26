using System;
using System.Collections.Generic;
using System.Linq;
using DapperExtensions.Mapper;
namespace ZDMesModels.LBJ
{
    /// <summary>
    /// 刀柄刃具使用信息表
    ///</summary>
    public class base_dbrjzx
    {
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
        /// 刀柄类型
        /// </summary>

        public string dblx { get; set; }

        /// <summary>
        /// 设备号 
        ///</summary>
        public string sbbh { get; set; }
        /// <summary>
        /// 刃具类型 
        ///</summary>
         public string rjlx { get; set; }
        
        /// <summary>
        /// 刃具标准寿命 
        ///</summary>
         public int rjbzsm { get; set; }
        /// <summary>
        /// 刃具安装时的寿命 
        ///</summary>
         public int rjazsm { get; set; }
        /// <summary>
        /// 刃具当前寿命 
        ///</summary>
         public int rjdqsm { get; set; }
        /// <summary>
        /// 刃具安装时的设备加工数 
        ///</summary>
         public long rjazjgs { get; set; }
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
         public int rjrmcs { get; set; }
        /// <summary>
        /// 刃具最后刃磨时间 
        ///</summary>
         public DateTime? rjzhrmsj { get; set; }
        /// <summary>
        /// 超限值
        /// </summary>
        public int cxz { get; set; }
        /// <summary>
        /// 岗位号
        /// </summary>
        public string gwh { get; set; }
        public decimal rjzt { get; set; }
        /// <summary>
        /// 刃具信息
        /// </summary>
        public base_rjxx baserjxx { get; set; }
        /// <summary>
        /// 刀柄信息
        /// </summary>
        public base_dbxx basedbxx { get; set; }
        /// <summary>
        /// 设备信息
        /// </summary>
        public base_sbxx basesbxx { get; set; }
        /// <summary>
        /// 刀柄刃具对应关系
        /// </summary>
        public List<base_dbrjgx> dbrjdygx { get; set; }
        /// <summary>
        /// 绑定刃具类型ids
        /// </summary>
        public List<int> rjids { get; set; } = new List<int>();
    }

    public class base_dbrjzx_mapper : ClassMapper<base_dbrjzx>
    {
        public base_dbrjzx_mapper()
        {
            Map(t => t.id).Key(KeyType.TriggerIdentity);
            Map(t => t.dblx).Ignore();
            Map(t => t.baserjxx).Ignore();
            Map(t => t.basedbxx).Ignore();
            Map(t => t.basesbxx).Ignore();
            Map(t => t.dbrjdygx).Ignore();
            Map(t => t.rjids).Ignore();
            Map(t => t.rjzt).Ignore();
            AutoMap();
        }
    }
}

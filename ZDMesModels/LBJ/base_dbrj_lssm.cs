using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.LBJ
{
    public class base_dbrj_lssm
    {
        public Int64 id { get; set; }
        /// <summary>
        /// 工厂代码
        /// </summary>
        public string gcdm { get; set; }
        /// <summary>
        /// 生产线
        /// </summary>
        public string scx { get; set; }
        /// <summary>
        /// 生产线子线
        /// </summary>
        public string scxzx { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string sbbh { get; set; }
        /// <summary>
        /// 岗位号
        /// </summary>
        public string gwh { get; set; }
        /// <summary>
        /// 刀柄编号
        /// </summary>
        public int dbh { get; set; }
        /// <summary>
        /// 刃具id
        /// </summary>
        public int rjid { get; set; }
        /// <summary>
        /// 刃具类型
        /// </summary>
        public string rjlx { get; set; }
        /// <summary>
        /// 刃具加工位置
        /// </summary>
        public string rjwz { get; set; }
        /// <summary>
        /// 刃具标准寿命
        /// </summary>
        public int rjbzsm { get; set; }
        /// <summary>
        /// 刃具当前寿命
        /// </summary>
        public int rjdqsm { get; set; }
        /// <summary>
        /// 刀柄领用时间
        /// </summary>
        public DateTime? dblysj { get; set; }
        /// <summary>
        /// 刀柄领用人
        /// </summary>
        public string dblyr { get; set; }
        /// <summary>
        /// 刃具领用时间
        /// </summary>
        public DateTime? rjlysj { get; set; }
        /// <summary>
        /// 刃具领用人
        /// </summary>
        public string rjlyr { get; set; }
        /// <summary>
        /// 刃具刃磨次数
        /// </summary>
        public int rjrmcs { get; set; }
        /// <summary>
        /// 刃具最后刃磨时间
        /// </summary>
        public DateTime? rjzhrmsj { get; set; }
        /// <summary>
        /// 刃具刃磨人
        /// </summary>
        public string rjrmr { get; set; }
        /// <summary>
        /// 录入人
        /// </summary>
        public string lrr { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime lrsj { get; set; } = DateTime.Now;
    }

    public class base_dbrj_lssm_mapper : ClassMapper<base_dbrj_lssm>
    {
        public base_dbrj_lssm_mapper()
        {
            Map(t => t.id).Key(KeyType.Assigned);
            AutoMap();
        }
    }
}

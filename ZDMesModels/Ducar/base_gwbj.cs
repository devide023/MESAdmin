using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.Ducar
{
    public class base_gwbj
    {
        public string rid { get; set; }
        public string gcdm { get; set; } = "101";
        /// <summary>
        /// 生产线
        /// </summary>
        public string scx { get; set; }
        /// <summary>
        /// 岗位号
        /// </summary>
        public string gwh { get; set; }
        public string gwmc { get; set; }
        /// <summary>
        /// 物料编码
        /// </summary>
        public string wlbm { get; set; }
        public string wlmc { get; set; }
        /// <summary>
        /// 岗位配比
        /// </summary>
        public string gwpb { get; set; }
        /// <summary>
        /// 单箱数量
        /// </summary>
        public int dxsl { get; set; }
        /// <summary>
        /// 前5位编码
        /// </summary>
        public string qwwbm { get; set; }
        /// <summary>
        /// 物料属性
        /// </summary>
        public string wlsx { get; set; }
        /// <summary>
        /// 是否打印
        /// </summary>
        public string sfdy { get; set; } = "Y";
        /// <summary>
        /// 备注
        /// </summary>
        public string bz { get; set; }
        public string lrr { get; set; }
        public DateTime lrsj { get; set; }=DateTime.Now;
        public string jxno { get; set; }
        /// <summary>
        /// 生产线岗位号
        /// </summary>
        public List<sys_option_item> gwhs { get; set; }
    }

    public class base_gwbj_mapper : ClassMapper<base_gwbj>
    {
        public base_gwbj_mapper()
        {
            Map(t => t.jxno).Column("jx_no");
            Map(t => t.rid).Ignore();
            Map(t => t.gwmc).Ignore();
            Map(t => t.wlmc).Ignore();
            Map(t => t.gwhs).Ignore();
            AutoMap();
        }
    }
}

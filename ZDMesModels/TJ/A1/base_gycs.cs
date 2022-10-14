using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.TJ.A1
{
    public class base_gycs
    {
        /// <summary>
        /// 工厂
        /// </summary>
        public string gcdm { get; set; } = "9100";
        /// <summary>
        /// 生产线
        /// </summary>
        public string scx { get; set; } = "1";
        /// <summary>
        /// 岗位编码
        /// </summary>
        public string gwh { get; set; }
        /// <summary>
        /// 机型
        /// </summary>
        public string jxno { get; set; }
        /// <summary>
        /// 状态码
        /// </summary>
        public string statusno { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string sbbh { get; set; }
        /// <summary>
        /// 程序号
        /// </summary>
        public string sbcxh { get; set; }
        /// <summary>
        /// 最小值
        /// </summary>
        public string gymin { get; set; }
        /// <summary>
        /// 最大值
        /// </summary>
        public string gymax { get; set; }
        /// <summary>
        /// 标准值
        /// </summary>
        public string gybz { get; set; }
        /// <summary>
        /// 审核标志
        /// </summary>
        public string shbz { get; set; } = "Y";
        /// <summary>
        /// 录入人
        /// </summary>
        public string lrr { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime lrsj { get; set; } = DateTime.Now;
        /// <summary>
        /// 审核人
        /// </summary>
        public string shr { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime shsj { get; set; } = DateTime.Now;
        /// <summary>
        /// 免检标志
        /// </summary>
        public string mj { get; set; }
        /// <summary>
        /// 螺钉数
        /// </summary>
        public string parm1 { get; set; }
    }

    public class base_gycs_mapper : ClassMapper<base_gycs>
    {
        public base_gycs_mapper()
        {
            Map(t => t.jxno).Column("jx_no");
            Map(t => t.statusno).Column("status_no");
            Map(t => t.gymin).Column("gy_min");
            Map(t => t.gymax).Column("gy_max");
            Map(t => t.gybz).Column("gy_bz");
            AutoMap();
        }
    }
}

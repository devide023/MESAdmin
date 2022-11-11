using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.TJ.A1
{
    public class mes_zxjc_gylx
    {
        public string rid { get; set; }
        /// <summary>
        /// 工厂
        /// </summary>
        public string gcdm { get; set; } = "9100";
        /// <summary>
        /// 生产线
        /// </summary>
        public string scx { get; set; } = "1";
        /// <summary>
        /// 机型
        /// </summary>
        public string jxno { get; set; }
        /// <summary>
        /// 状态码
        /// </summary>
        public string statusno { get; set; }
        /// <summary>
        /// 岗位编码
        /// </summary>
        public string gwh { get; set; }
        /// <summary>
        /// 装配顺序
        /// </summary>
        public int zpsx { get; set; }
        /// <summary>
        /// 是否免检(每台检 N， 首检S，免检Y)
        /// </summary>
        public string mj { get; set; } = "N";
        /// <summary>
        /// 互锁标志
        /// </summary>
        public string fsbz { get; set; } = "Y";
        /// <summary>
        /// 审核标志
        /// </summary>
        public string shbz { get; set; } = "Y";
        /// <summary>
        /// 是否装配
        /// </summary>
        public string sfzp { get; set; } = "Y";
        /// <summary>
        /// 复检编号
        /// </summary>
        public string fjbh { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string bz { get; set; }
        /// <summary>
        /// 录入人
        /// </summary>
        public string lrr { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime lrsj { get; set; } = DateTime.Now;
        public IEnumerable<dynamic> statusno_list { get; set; }
    }

    public class zxjc_gylx_mapper : ClassMapper<mes_zxjc_gylx>
    {
        public zxjc_gylx_mapper()
        {
            Map(t => t.jxno).Column("jx_no");
            Map(t => t.statusno).Column("status_no");
            Map(t => t.rid).Ignore();
            Map(t => t.statusno_list).Ignore();
            AutoMap();
        }
    }
}

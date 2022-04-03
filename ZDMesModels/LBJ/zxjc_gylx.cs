using System;
using System.Collections.Generic;
using System.Linq;
using DapperExtensions.Mapper;
namespace ZDMesModels.LBJ
{
    /// <summary>
    /// 岗位工艺路线表
    ///</summary>
    public class zxjc_gylx
    {
        /// <summary>
        /// 工厂 
        ///</summary>
         public string gcdm { get; set; }
        /// <summary>
        /// 生产线 
        ///</summary>
         public string scx { get; set; }
        /// <summary>
        /// 状态码 
        ///</summary>
         public string statusno { get; set; }
        /// <summary>
        /// 岗位编码 
        ///</summary>
         public string gwh { get; set; }
        /// <summary>
        /// 装配顺序 
        ///</summary>
         public decimal? zpsx { get; set; }
        /// <summary>
        /// 是否免检(每台检 N， 首检S，免检Y) 
        ///</summary>
         public string mj { get; set; }
        /// <summary>
        /// 互锁标志 
        ///</summary>
         public string fsbz { get; set; }
        /// <summary>
        /// 审核标志 
        ///</summary>
         public string shbz { get; set; }
        /// <summary>
        /// 是否装配 
        ///</summary>
         public string sfzp { get; set; }
        /// <summary>
        /// 复检编号 
        ///</summary>
         public string fjbh { get; set; }
        /// <summary>
        /// 备注 
        ///</summary>
         public string bz { get; set; }
        /// <summary>
        /// 录入人 
        ///</summary>
         public string lrr { get; set; }
        /// <summary>
        /// 录入时间 
        ///</summary>
         public DateTime? lrsj { get; set; }
        /// <summary>
        /// 审核人 
        ///</summary>
         public string shr { get; set; }
        /// <summary>
        /// 审核时间 
        ///</summary>
         public DateTime? shsj { get; set; }
        /// <summary>
        /// 互锁人 
        ///</summary>
         public string fsr { get; set; }
        /// <summary>
        /// 互锁时间 
        ///</summary>
         public DateTime? fssj { get; set; }
    }

    public class zxjc_gylx_mapper : ClassMapper<zxjc_gylx>
    {
        public zxjc_gylx_mapper()
        {
            Map(t => t.statusno).Column("status_no");
            AutoMap();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using DapperExtensions.Mapper;
namespace ZDMesModels.LBJ
{
    /// <summary>
    /// 奖惩管理
    ///</summary>
    public class zxjc_jcgl
    {
        public string rid { get; set; }
        /// <summary>
        /// 工厂 
        ///</summary>
        public string gcdm { get; set; }
        /// <summary>
        /// 人员账号 
        ///</summary>
         public string usercode { get; set; }
        /// <summary>
        /// 生产线 
        ///</summary>
         public string scx { get; set; }
        /// <summary>
        /// 班组 
        ///</summary>
         public string bzxx { get; set; }
        /// <summary>
        /// 岗位号 
        ///</summary>
         public string gwh { get; set; }
        /// <summary>
        /// 奖惩明细信息 
        ///</summary>
         public string jcxx { get; set; }
        /// <summary>
        /// 奖惩来源 
        ///</summary>
         public string jcly { get; set; }
        /// <summary>
        /// 发生产品数量 
        ///</summary>
         public string sl { get; set; }
        /// <summary>
        /// 奖惩金额 
        ///</summary>
         public string jcje { get; set; }
        /// <summary>
        /// 发生日期 
        ///</summary>
         public DateTime fsrq { get; set; }
        /// <summary>
        /// 考核人 
        ///</summary>
         public string khr { get; set; }
        /// <summary>
        /// 考核部门 
        ///</summary>
         public string khbm { get; set; }
        /// <summary>
        /// 考核类型 
        ///</summary>
         public string lx { get; set; }
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
         public DateTime lrsj { get; set; }
    }

    public class zxjc_jcgl_maper : ClassMapper<zxjc_jcgl>
    {
        public zxjc_jcgl_maper()
        {
            Map(t => t.rid).Ignore();
            Map(t => t.usercode).Column("user_code");
            AutoMap();
        }
    }
}

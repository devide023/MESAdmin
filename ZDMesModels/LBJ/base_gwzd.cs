using System;
using System.Collections.Generic;
using System.Linq;
using DapperExtensions.Mapper;
namespace ZDMesModels.LBJ
{
    /// <summary>
    /// 岗位站点表
    ///</summary>
    public class base_gwzd
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
        /// 岗位编号 
        ///</summary>
         public string gwh { get; set; }
        /// <summary>
        /// 岗位名称 
        ///</summary>
         public string gwmc { get; set; }
        /// <summary>
        /// 岗位分类（机加、检测、打包） 
        ///</summary>
         public string gwlx { get; set; }
        /// <summary>
        /// 岗位分类（人工、自动） 
        ///</summary>
         public string gwfl { get; set; }
        /// <summary>
        /// 管理该岗位的岗位号 
        ///</summary>
         public string glgwh { get; set; }
        /// <summary>
        /// 审核标志 
        ///</summary>
         public string shbz { get; set; }
        /// <summary>
        /// 故障停用 
        ///</summary>
         public string gzty { get; set; }
        /// <summary>
        /// pcsip地址 
        ///</summary>
         public string pcsip { get; set; }
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
        /// 超级权限登录 
        ///</summary>
         public string cjqxdl { get; set; }
        /// <summary>
        /// 最后登录时间 
        ///</summary>
         public DateTime? dlsj { get; set; }
        /// <summary>
        /// 最后登录员工代码 
        ///</summary>
         public string usercode { get; set; }
        /// <summary>
        /// 自动合格标识 
        ///</summary>
         public string iszdhg { get; set; }
    }

    public class basegwzd_mapper : ClassMapper<base_gwzd>
    {
        public basegwzd_mapper()
        {
            Map(t => t.gwh).Key(KeyType.Assigned);
            Map(t => t.usercode).Column("user_code");
            AutoMap();
        }
    }
}

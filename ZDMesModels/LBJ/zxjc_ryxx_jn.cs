using System;
using System.Collections.Generic;
using System.Linq;
using DapperExtensions.Mapper;
namespace ZDMesModels.LBJ
{
    /// <summary>
    /// 人员技能信息
    ///</summary>
    public class zxjc_ryxx_jn
    {
        /// <summary>
        /// 工厂 
        ///</summary>
         public string gcdm { get; set; }
        /// <summary>
        /// 人员账号 
        ///</summary>
         public string usercode { get; set; }
        /// <summary>
        /// 人员姓名
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 技能编号 
        ///</summary>
        public string jnbh { get; set; }
        /// <summary>
        /// 技能信息 
        ///</summary>
         public string jnxx { get; set; }
        /// <summary>
        /// 适应生产线 
        ///</summary>
         public string scx { get; set; }
        /// <summary>
        /// 适应岗位 
        ///</summary>
         public string gwh { get; set; }
        /// <summary>
        /// 是否合格 
        ///</summary>
         public string sfhg { get; set; }
        /// <summary>
        /// 录入人 
        ///</summary>
         public string lrr { get; set; }
        /// <summary>
        /// 录入时间 
        ///</summary>
         public DateTime? lrsj { get; set; }
        /// <summary>
        /// 技能分配 
        ///</summary>
         public string jnfl { get; set; }
        /// <summary>
        /// 技能时间 
        ///</summary>
         public DateTime? jnsj { get; set; }
        /// <summary>
        /// 技能熟练度 
        ///</summary>
         public int jnsld { get; set; }
        /// <summary>
        /// 岗位列选项
        /// </summary>
        public List<sys_column_options> gwhoptions { get; set; }
        /// <summary>
        /// 生产线人员选项
        /// </summary>
        public List<sys_column_options> useroptions { get; set; }        
    }
    public class zxjcryxxjn_mapper : ClassMapper<zxjc_ryxx_jn>
    {
        public zxjcryxxjn_mapper()
        {
            Map(t => t.jnbh).Key(KeyType.Assigned);
            Map(t => t.usercode).Column("user_code");
            Map(t => t.username).Ignore();
            Map(t => t.gwhoptions).Ignore();
            Map(t => t.useroptions).Ignore();
            AutoMap();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using DapperExtensions.Mapper;
namespace ZDMesModels.LBJ
{
    /// <summary>
    /// 电子工艺表
    ///</summary>
    public class zxjc_t_dzgy
    {
        /// <summary>
        /// 工艺文件ID（GUID） 
        ///</summary>
         public string gyid { get; set; }
        /// <summary>
        /// 工艺文件编号 
        ///</summary>
         public string gybh { get; set; }
        /// <summary>
        /// 工艺文件名称 
        ///</summary>
         public string gymc { get; set; }
        /// <summary>
        /// 工艺文件描述 
        ///</summary>
         public string gyms { get; set; }
        /// <summary>
        /// 工厂 
        ///</summary>
         public string gcdm { get; set; }
        /// <summary>
        /// 生产线 
        ///</summary>
         public string scx { get; set; }
        /// <summary>
        /// 产品信息 
        ///</summary>
         public string statusno { get; set; }
        /// <summary>
        /// 文件路径 
        ///</summary>
         public string wjlj { get; set; }
        /// <summary>
        /// 文件大小 
        ///</summary>
         public string jwdx { get; set; }
        /// <summary>
        /// 启用日期 
        ///</summary>
         public DateTime? qyrq { get; set; }
        /// <summary>
        /// 文件分类：视频、工艺图纸 
        ///</summary>
         public string wjfl { get; set; }
        /// <summary>
        /// 上传电脑名 
        ///</summary>
         public string scpc { get; set; }
        /// <summary>
        /// 上传日期 
        ///</summary>
         public DateTime? scsj { get; set; }
    }

    public class zxjc_t_dzgy_mapper : ClassMapper<zxjc_t_dzgy>
    {
        public zxjc_t_dzgy_mapper()
        {
            Map(t => t.gyid).Key(KeyType.Assigned);
            Map(t => t.statusno).Column("status_no");
            AutoMap();
        }
    }
}

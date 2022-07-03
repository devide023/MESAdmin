using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
using Dapper;
namespace ZDMesModels.TJ
{
    /// <summary>
    /// 工序站点
    /// </summary>
    public class zxjc_gxzd
    {
        public string rid { get; set; }
        /// <summary>
        /// 工序编号
        /// </summary>
        public string workno { get; set; }
        /// <summary>
        /// 工序名称    
        /// </summary>
        public string workname { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string lx { get; set; }
        /// <summary>
        /// 录入人
        /// </summary>
        public string lrr { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime lrsj { get; set; }
        /// <summary>
        /// 审核标志
        /// </summary>
        public string shbz { get; set; }
    }

    public class zxjc_gxzd_mapper : ClassMapper<zxjc_gxzd>
    {
        public zxjc_gxzd_mapper()
        {
            Map(t => t.workno).Column("work_no");
            Map(t => t.workname).Column("work_name");
            Map(t => t.rid).Ignore();
            AutoMap();
        }
    }
}

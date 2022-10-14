using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.TJ.A1
{
    /// <summary>
    /// 金润工艺
    /// </summary>
    public class zxjc_jrgy
    {
        /// <summary>
        /// rowid
        /// </summary>
        public string rid { get; set; }
        /// <summary>
        /// 工序号
        /// </summary>
        public string workno { get; set; }
        /// <summary>
        /// 岗位名称
        /// </summary>
        public string workname { get; set; }
        /// <summary>
        /// 生产线
        /// </summary>
        public string scx { get; set; }
        /// <summary>
        /// 状态编码
        /// </summary>
        public string ztbm { get; set; }
        /// <summary>
        /// 机器人程序号
        /// </summary>
        public string jqrcs { get; set; }
        /// <summary>
        /// 最小值
        /// </summary>
        public string min { get; set; }
        /// <summary>
        /// 最大值
        /// </summary>
        public string max { get; set; }
        /// <summary>
        /// 标准值
        /// </summary>
        public string bz { get; set; }
        /// <summary>
        /// 箱体物料编码
        /// </summary>
        public string xtwlbm { get; set; }
        /// <summary>
        /// 打印模板
        /// </summary>
        public string dymb { get; set; }
        /// <summary>
        /// 油封工装/水平垂直（1.水平 2.垂直）
        /// </summary>
        public string gzlx2 { get; set; }
        /// <summary>
        /// 是否涂油
        /// </summary>
        public string sfty { get; set; }
        /// <summary>
        /// 是否注油
        /// </summary>
        public string sfzy { get; set; }
        /// <summary>
        /// 是否油封压装
        /// </summary>
        public string sfyfyz { get; set; }
        /// <summary>
        /// 是否轴承压装
        /// </summary>
        public string sfzcyz { get; set; }
        /// <summary>
        /// 轴承工装（其它工装）/拧紧程序号/机器人工装号
        /// </summary>
        public string gzlx1 { get; set; }
        /// <summary>
        /// 审核标志
        /// </summary>
        public string shbz { get; set; }
        /// <summary>
        /// 录入人
        /// </summary>
        public string lrr { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime lrsj { get; set; }
    }

    public class zxjc_jrgy_mapper : ClassMapper<zxjc_jrgy>
    {
        public zxjc_jrgy_mapper()
        {
            Map(t => t.rid).Ignore();
            Map(t => t.workno).Column("work_no");
            Map(t => t.workname).Column("work_name");
            AutoMap();
        }
    }
}

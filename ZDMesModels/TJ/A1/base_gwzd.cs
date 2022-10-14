using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.TJ.A1
{
    public class base_gwzd
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
        /// 岗位编码
        /// </summary>
        public string gwh { get; set; }
        /// <summary>
        /// 岗位名称
        /// </summary>
        public string gwmc { get; set; }
        /// <summary>
        /// 岗位分类（部装、装配、测试、返工）
        /// </summary>
        public string gwlx { get; set; } = "装配";
        /// <summary>
        /// 岗位分类（人工、自动）
        /// </summary>
        public string gwfl { get; set; } = "人工";
        /// <summary>
        /// 工序号 对应原程序的工序号
        /// </summary>
        public string workflow { get; set; }
        /// <summary>
        /// 审核标志（Y已审核 N未审核）
        /// </summary>
        public string shbz { get; set; } = "Y";
        /// <summary>
        /// 故障停用（Y停用 N启用）
        /// </summary>
        public string gzty { get; set; } = "N";
        /// <summary>
        /// PCSIP地址
        /// </summary>
        public string pcsip { get; set; }
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
        /// <summary>
        /// 审核人
        /// </summary>
        public string shr { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime shsj { get; set; } = DateTime.Now;
        /// <summary>
        /// 看护岗位
        /// </summary>
        public string khgw { get; set; }
        /// <summary>
        /// 是否开启机号缓存功能,0=不缓存,1=缓存
        /// </summary>
        public int iscache { get; set; }
    }

    public class base_gwzd_mapper : ClassMapper<base_gwzd>
    {
        public base_gwzd_mapper()
        {
            Map(t => t.gwh).Key(KeyType.Assigned);
            Map(t => t.workflow).Column("work_flow");
            Map(t => t.rid).Ignore();
            AutoMap();
        }
    }
}

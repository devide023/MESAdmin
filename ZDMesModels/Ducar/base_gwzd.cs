using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.Ducar
{
    public class base_gwzd
    {
        public string rid { get; set; }
        /// <summary>
        /// 工厂
        /// </summary>
        public string gcdm { get; set; } = "101";
        /// <summary>
        /// 生产线
        /// </summary>
        public string scx { get; set; }
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
        public string gwlx { get; set; }
        /// <summary>
        /// 岗位分类（人工、自动）
        /// </summary>
        public string gwfl { get; set; }
        /// <summary>
        /// 审核标志（Y已审核 N未审核）
        /// </summary>
        public string shbz { get; set; } = "Y";
        /// <summary>
        /// 故障停用（Y停用 N启用）
        /// </summary>
        public string gzty { get; set; } = "N";
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
        public DateTime? lrsj { get; set; } = DateTime.Now;
        /// <summary>
        /// 审核人
        /// </summary>
        public string shr { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime? shsj { get; set; }
        /// <summary>
        /// PCS人工岗位PCIP
        /// </summary>
        public string ip { get; set; }
        /// <summary>
        /// 看护岗位
        /// </summary>
        public string iskhgw { get; set; }
        /// <summary>
        /// 返修岗位号
        /// </summary>
        public string fxgwh { get; set; }
        /// <summary>
        /// 当前机型平台
        /// </summary>
        public string dqjx { get; set; }
        /// <summary>
        /// 工序 1：托盘绑定 2：扭力 3：耐压仪 4：辅料绑定
        /// </summary>
        public string workflow { get; set; }
        /// <summary>
        /// 首末岗位(S 首岗位 M 末岗位)
        /// </summary>
        public string smgw { get; set; }
        /// <summary>
        /// 解绑发动机（Y/N）
        /// </summary>
        public string jbfdj { get; set; }
        /// <summary>
        /// 是否自动合格
        /// </summary>
        public string iszdhg { get; set; }
        /// <summary>
        /// 是否换产校验
        /// </summary>
        public string ishcjy { get; set; }


    }
    public class base_gwzd_mapper : ClassMapper<base_gwzd>
    {
        public base_gwzd_mapper()
        {
            Map(t => t.gcdm).Key(KeyType.Assigned);
            Map(t => t.scx).Key(KeyType.Assigned);
            Map(t => t.gwh).Key(KeyType.Assigned);
            Map(t => t.rid).Ignore();
            Map(t => t.iskhgw).Ignore();
            Map(t => t.workflow).Column("work_flow");
            AutoMap();
        }
    }
}

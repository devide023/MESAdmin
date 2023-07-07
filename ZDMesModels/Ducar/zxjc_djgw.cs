using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.Ducar
{
    public class zxjc_djgw
    {
        /// <summary>
        /// 工厂
        /// </summary>
        public string gcdm { get; set; } = "101";
        /// <summary>
        /// 生产线
        /// </summary>
        public string scx { get; set; }
        /// <summary>
        /// 点检编号
        /// </summary>
        public string djno { get; set; }
        /// <summary>
        /// 点检类型
        /// </summary>
        public string djlx { get; set; }
        /// <summary>
        /// 岗位编码
        /// </summary>
        public string gwh { get; set; }
        public string gwmc { get; set; }
        /// <summary>
        /// 机型
        /// </summary>
        public string jxno { get; set; }
        /// <summary>
        /// 状态码
        /// </summary>
        public string statusno { get; set; }

        /// <summary>
        /// 点检内容
        /// </summary>
        public string djxx { get; set; }
        /// <summary>
        /// 删除标志
        /// </summary>
        public string scbz { get; set; } = "N";
        /// <summary>
        /// 录入人
        /// </summary>
        public string lrr { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime lrsj { get; set; } = DateTime.Now;

        public List<sys_options_list> gwhs { get; set; }
    }

    public class zxjc_djgw_mapper : ClassMapper<zxjc_djgw>
    {
        public zxjc_djgw_mapper()
        {
            Map(t => t.djno).Key(KeyType.Assigned);
            Map(t => t.jxno).Column("jx_no");
            Map(t => t.statusno).Column("status_no");
            Map(t => t.gwmc).Ignore();
            Map(t => t.gwhs).Ignore();
            AutoMap();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.TJ.A1
{
    public class zxjc_t_jstcfp
    {
        /// <summary>
        ///  Guid
        /// </summary>
        public string id { get; set; } = Guid.NewGuid().ToString();
        /// <summary>
        /// 技通文件ID
        /// </summary>
        public string jtid { get; set; }
        /// <summary>
        /// 工厂
        /// </summary>
        public string gcdm { get; set; } = "9100";
        /// <summary>
        /// 生产线（质量班长维护）
        /// </summary>
        public string scx { get; set; } = "1";
        /// <summary>
        /// 岗位号（质量组长维护）
        /// </summary>
        public string gwh { get; set; }
        /// <summary>
        /// 机型??? （质量班长维护）
        /// </summary>
        public string jxno { get; set; }
        /// <summary>
        /// 状态码（质量班长维护）
        /// </summary>
        public string statusno { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string bz { get; set; }
        /// <summary>
        /// 班长录入人
        /// </summary>
        public string lrr1 { get; set; }
        /// <summary>
        /// 班长录入时间
        /// </summary>
        public DateTime lrsj1 { get; set; }=DateTime.Now;
        /// <summary>
        /// 组长录入人
        /// </summary>
        public string lrr2 { get; set; }
        /// <summary>
        /// 组长录入时间
        /// </summary>
        public DateTime lrsj2 { get; set; } = DateTime.Now;
        /// <summary>
        /// 文件路径
        /// </summary>
        public string wjlj { get; set; }
        /// <summary>
        /// 技通名称
        /// </summary>
        public string jcmc { get; set; }
        /// <summary>
        /// 技通编号
        /// </summary>
        public string jcbh { get; set; }
    }

    public class zxjc_t_jstcfp_mapper : ClassMapper<zxjc_t_jstcfp>
    {
        public zxjc_t_jstcfp_mapper()
        {
            Map(t => t.id).Key(KeyType.Assigned);
            Map(t => t.jxno).Column("jx_no");
            Map(t => t.statusno).Column("status_no");
            Map(t => t.wjlj).Ignore();
            Map(t => t.jcbh).Ignore();
            Map(t => t.jcmc).Ignore();
            AutoMap();
        }
    }
}

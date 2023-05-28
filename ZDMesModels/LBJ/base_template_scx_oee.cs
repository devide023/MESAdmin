using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.LBJ
{
    public class base_template_scx_oee
    {
        public string rid { get; set; }
        public string gcdm { get; set; } = "9902";
        public string scx { get; set; }
        /// <summary>
        /// 计划作息时间(小时)
        /// </summary>
        public decimal jhzxsj { get; set; }
        /// <summary>
        /// 早中晚 班前会(小时)
        /// </summary>
        public decimal zzwbqh { get; set; }
        /// <summary>
        /// 早中晚 吃饭(小时)
        /// </summary>
        public decimal zzwcf { get; set; }
        /// <summary>
        /// 早中晚 班中休息(小时)
        /// </summary>
        public decimal zzwbzxx { get; set; }
        /// <summary>
        /// 早中晚 班后5S整理或者设备保养(小时)
        /// </summary>
        public decimal zzwsbby { get; set; }
        /// <summary>
        /// 培训（小时）
        /// </summary>
        public decimal px { get; set; }
        /// <summary>
        /// 休息（小时）
        /// </summary>
        public decimal xx { get; set; }
        /// <summary>
        /// 堵料时间(小时)
        /// </summary>
        public decimal dlsjjam { get; set; }
        /// <summary>
        /// 待料时间(小时)
        /// </summary>
        public decimal dlsjwait { get; set; }
        /// <summary>
        /// 换刀时间(小时)
        /// </summary>
        public decimal hdsj { get; set; }
        /// <summary>
        /// 换型时间(小时)
        /// </summary>
        public decimal hxsj { get; set; }
        /// <summary>
        /// 故障时间(小时)
        /// </summary>
        public decimal gzsj { get; set; }
        /// <summary>
        /// 其他停机时间(小时)
        /// </summary>
        public decimal qttjsj { get; set; }
        /// <summary>
        /// 理论节拍（秒）
        /// </summary>
        public decimal lljp { get; set; }
        /// <summary>
        /// 目标OEE(%)
        /// </summary>
        public decimal oeetarget { get; set; }
    }

    public class base_template_scx_oee_mapper : ClassMapper<base_template_scx_oee>
    {
        public base_template_scx_oee_mapper()
        {
            Map(t => t.gcdm).Key(KeyType.Assigned);
            Map(t => t.scx).Key(KeyType.Assigned);
            Map(t => t.dlsjjam).Column("dlsj_jam");
            Map(t => t.dlsjwait).Column("dlsj_wait");
            Map(t => t.oeetarget).Column("oee_target");
            Map(t => t.rid).Ignore();
            AutoMap();
        }
    }
}

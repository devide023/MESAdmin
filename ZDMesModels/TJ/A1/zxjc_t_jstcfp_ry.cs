using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.TJ.A1
{
    /// <summary>
    /// 技通分配人员设置
    /// </summary>
    public class zxjc_t_jstcfp_ry
    {
        public string id { get; set; } = Guid.NewGuid().ToString();
        /// <summary>
        /// 用户编码
        /// </summary>
        public string usercode { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 可查看、分配对应生产线通知
        /// </summary>
        public string zbid { get; set; }
        /// <summary>
        /// 录入人
        /// </summary>
        public string lrr { get; set; }
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime? lrsj { get; set; }
    }

    public class zxjc_t_jstcfp_ry_mapper : ClassMapper<zxjc_t_jstcfp_ry>
    {
        public zxjc_t_jstcfp_ry_mapper()
        {
            Map(t => t.id).Key(KeyType.Assigned);
            Map(t => t.username).Ignore();
            AutoMap();
        }
    }
}

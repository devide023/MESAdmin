using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.TJ.A1
{
    public class zxjc_t_jstcfp_group
    {
        public string id { get; set; } = Guid.NewGuid().ToString();
        /// <summary>
        /// 组别id
        /// </summary>
        public string zbid { get; set; }
        /// <summary>
        /// 组别名称
        /// </summary>
        public string zbmc { get; set; }
        /// <summary>
        /// 是否分配到岗位
        /// </summary>
        public string istogwh { get; set; }
    }

    public class zxjc_t_jstcfp_group_mapper : ClassMapper<zxjc_t_jstcfp_group>
    {
        public zxjc_t_jstcfp_group_mapper()
        {
            Map(t => t.id).Key(KeyType.Assigned);
            AutoMap();
        }
    }
}

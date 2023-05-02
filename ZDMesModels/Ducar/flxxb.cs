using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.Ducar
{
    public class flxxb
    {
        public string rid { get; set; }
        public string id { get; set; }
        public string scx { get; set; }
        public string gwh { get; set; }
        public string jxno { get; set; }
        public string statusno { get; set; }
        /// <summary>
        /// 辅料类型
        /// </summary>
        public string fllx { get; set; }
        /// <summary>
        /// 物料描述
        /// </summary>
        public string wlms { get; set; }
        public string lrr { get; set; }
        public DateTime lrsj { get; set; } = DateTime.Now;
    }
    public class flxxb_mapper : ClassMapper<flxxb>
    {
        public flxxb_mapper()
        {
            Map(t => t.id).Key(KeyType.Assigned);
            Map(t => t.rid).Ignore();
            Map(t => t.jxno).Column("jx_no");
            Map(t => t.statusno).Column("status_no");
            AutoMap();
        }
    }
}

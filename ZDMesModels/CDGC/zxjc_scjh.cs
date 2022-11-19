using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.CDGC
{
    public class zxjc_scjh
    {
        public string id { get; set; } = Guid.NewGuid().ToString();
        public string orderno { get; set; }
        public string scx { get; set; }
        public int xh { get; set; }
        public string scbc { get; set; }
        public Int32 scsl { get; set; }
        public DateTime? scsj { get; set; }
        public Int32 zpsl { get; set; }
        public DateTime? zpsj { get; set; }
        public string jx { get; set; }
        public string ztbm { get; set; }
        public string bz { get; set; }
        public string scbz { get; set; } = "N";
        public string lrr { get; set; }
        public DateTime? lrsj { get; set; }
    }

    public class zxjc_scjh_mapper : ClassMapper<zxjc_scjh>
    {
        public zxjc_scjh_mapper()
        {
            Map(t => t.id).Key(KeyType.Assigned);
            AutoMap();
        }
    }
}

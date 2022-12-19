using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
using DapperExtensions.Predicate;

namespace ZDMesModels.CDGC
{
    public class zxjc_nok_mark
    {
        public string id { get; set; }=Guid.NewGuid().ToString();
        public string gcdm { get; set; }
        public string scx { get; set; }
        public string vin { get; set; }
        public string lx { get; set; }
        public string jcjg { get; set; }
        public string gzxx { get; set; }
        public string yxxx { get; set; }
        public string jth { get; set; }
        public string czr { get; set; }
        public DateTime? czsj { get; set; }
        public string tfr { get; set; }
        public DateTime? tfsj { get; set; }
        public string lrr { get; set; }
        public DateTime? lrsj { get; set; }
    }

    public class zxjc_nok_mark_mapper : ClassMapper<zxjc_nok_mark>
    {
        public zxjc_nok_mark_mapper()
        {
            Map(t => t.id).Key(KeyType.Assigned);
            AutoMap();
        }
    }
}

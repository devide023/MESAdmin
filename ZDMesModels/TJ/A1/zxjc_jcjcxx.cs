using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.TJ.A1
{
    public class zxjc_jcjcxx
    {
        public string id { get; set; }=Guid.NewGuid().ToString();
        public string gcdm { get; set; } = "9100";
        public string scx { get; set; }
        public string gwh { get; set; }
        public string jclx { get; set; }
        public string jclb { get; set; }
        public int xh { get; set; }
        public string jcyq { get; set; }
        public string bzz { get; set; }
        public string bzsx { get; set; }
        public string bzxx { get; set; }
        public string lrr { get; set; }
        public DateTime lrsj { get; set; }=DateTime.Now;
        public string scbz { get; set; } = "N";
    }

    public class zxjc_jcjcxx_mapper : ClassMapper<zxjc_jcjcxx>
    {
        public zxjc_jcjcxx_mapper()
        {
            Map(t => t.id).Key(KeyType.Assigned);
            AutoMap();
        }
    }
}

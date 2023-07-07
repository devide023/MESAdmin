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
        /// <summary>
        /// 检测点（班前、班中、班后）
        /// </summary>
        public string jcd { get; set; }
        //检测结果（虚拟）
        public string jcjg { get; set; }
        public string zjcjg { get; set; }
        public string scxmc { get; set; }
        /// <summary>
        /// 是否监督确认
        /// </summary>
        public string jdqr { get; set; }
    }

    public class zxjc_jcjcxx_mapper : ClassMapper<zxjc_jcjcxx>
    {
        public zxjc_jcjcxx_mapper()
        {
            Map(t => t.id).Key(KeyType.Assigned);
            Map(t => t.jcjg).Ignore();
            Map(t => t.zjcjg).Ignore();
            Map(t => t.scxmc).Ignore();
            Map(t => t.jdqr).Ignore();
            AutoMap();
        }
    }
}

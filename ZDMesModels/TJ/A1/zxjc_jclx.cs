using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.TJ.A1
{
    /// <summary>
    /// 检测类型
    /// </summary>
    public class zxjc_jclx
    {
        public string id { get; set; }=Guid.NewGuid().ToString();
        public string gcdm { get; set; } = "9100";
        public string scx { get; set; }
        public string jclx { get; set; }
        public string lrr { get; set; }
        public DateTime lrsj { get; set; }=DateTime.Now;
        public string scbz { get; set; } = "N";
    }

    public class zxjc_jclx_mapper : ClassMapper<zxjc_jclx>
    {
        public zxjc_jclx_mapper()
        {
            Map(t => t.id).Key(KeyType.Assigned);
            AutoMap();
        }
    }
}

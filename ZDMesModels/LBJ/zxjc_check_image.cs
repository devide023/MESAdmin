using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.LBJ
{
    public class zxjc_check_image
    {
        public int id { get; set; }
        public string cpxh { get; set; }
        public string cpfw { get; set; }
        public string tplj { get; set; }
        public string tpmc { get; set; }
        public string lrr { get; set; }
        public DateTime lrsj { get; set; }
    }

    public class zxjc_check_image_mapper : ClassMapper<zxjc_check_image>
    {
        public zxjc_check_image_mapper()
        {
            Map(t => t.id).Key(KeyType.Assigned);
            AutoMap();
        }
    }
}

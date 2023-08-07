using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels
{
    public class base_param
    {
        public string rid { get; set; }
        public string gcdm { get; set; }
        public string scx { get; set; }
        public string scxzx { get; set; }
        public string paramkey { get; set; }
        public string paramvalue { get; set; }
        public string bz { get; set; }
        /// <summary>
        /// 生产线子线
        /// </summary>
        public List<option_list> scxzxs { get; set; }
    }

    public class base_param_mapper:ClassMapper<base_param>
    {
        public base_param_mapper()
        {
            Map(t => t.gcdm).Key(KeyType.Assigned);
            Map(t => t.scx).Key(KeyType.Assigned);
            Map(t => t.scxzx).Key(KeyType.Assigned);
            Map(t => t.paramkey).Key(KeyType.Assigned);
            Map(t => t.paramkey).Column("param_key");
            Map(t => t.paramvalue).Column("param_value");
            Map(t => t.rid).Ignore();
            Map(t => t.scxzxs).Ignore();
            AutoMap();
        }
    }
}

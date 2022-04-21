using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.LBJ
{
    public class base_cnc
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        public string sbbh { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string sbmc { get; set; }
    }

    public class base_cnc_mapper : ClassMapper<base_cnc>
    {
        public base_cnc_mapper()
        {
            Map(t => t.sbbh).Key(KeyType.Assigned);
            AutoMap();
        }
    }
}

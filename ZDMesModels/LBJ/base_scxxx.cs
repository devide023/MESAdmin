using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.LBJ
{
    public class base_scxxx
    {
        /// <summary>
        /// 工厂代码
        /// </summary>
        public string gcdm { get; set; }
        /// <summary>
        /// 生产线
        /// </summary>
        public string scx { get; set; }
        /// <summary>
        /// 生产线名称
        /// </summary>
        public string scxmc { get; set; }
    }

    public class base_scxxx_maper : ClassMapper<base_scxxx>
    {
        public base_scxxx_maper()
        {
            AutoMap();
        }
    }
}

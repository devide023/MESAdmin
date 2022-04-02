using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.LBJ
{
    public class base_gcxx
    {
        /// <summary>
        /// 工厂代码
        /// </summary>
        public string gcdm { get; set; }
        /// <summary>
        /// 工厂名称
        /// </summary>
        public string gcmc { get; set; }
    }

    public class base_gcxx_maper : ClassMapper<base_gcxx>
    {
        public base_gcxx_maper()
        {
            AutoMap();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.LBJ
{
    public class sys_dbrjly
    {
        /// <summary>
        /// 刃具id
        /// </summary>
        public int rjid { get; set; }
        /// <summary>
        /// 产品状态
        /// </summary>
        public string cpzt { get; set; }
        /// <summary>
        /// 刀柄号
        /// </summary>
        public string dbh { get; set; }
        /// <summary>
        /// 刃具类型
        /// </summary>
        public string rjlx { get; set; }
        public string rjmc { get; set; }
        /// <summary>
        /// 刃具标准寿命
        /// </summary>
        public int rjbzsm { get; set; }
    }
}

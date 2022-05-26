using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.LBJ
{
    public class dbrjlyform
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
        /// 设备编号
        /// </summary>
        public string sbbh { get; set; }
        /// <summary>
        /// 刀柄号
        /// </summary>
        public List<string> dbh { get; set; }
        /// <summary>
        /// 刃具类型
        /// </summary>
        public List<dynamic> rjlx { get; set; }
        /// <summary>
        /// 领用人
        /// </summary>
        public string lyr { get; set; }
    }
}

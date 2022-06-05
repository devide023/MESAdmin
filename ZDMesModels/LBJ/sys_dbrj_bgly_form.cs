using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.LBJ
{
    /// <summary>
    /// 更换刀柄刃具领用2022-06-03
    /// </summary>
    public class sys_dbrj_bgly_form
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
        /// 刀柄刃具对应关系id
        /// </summary>
        public List<int> dbrjgxid { get; set; }
        /// <summary>
        /// 领用人
        /// </summary>
        public string lyr { get; set; }
    }
}

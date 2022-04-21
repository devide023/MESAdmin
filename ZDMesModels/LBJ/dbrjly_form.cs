using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.LBJ
{
    public class dbrjly_form
    {
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
        public string dbh { get; set; }
        /// <summary>
        /// 领用人
        /// </summary>
        public string lyr { get; set; }
        /// <summary>
        /// 新生产线
        /// </summary>
        public string newscx { get; set; }
        /// <summary>
        /// 新设备编号
        /// </summary>
        public string newsbbh { get; set; }
        /// <summary>
        /// 新刀柄号
        /// </summary>
        public string newdbh { get; set; }
        /// <summary>
        /// 换刀时，原领用刃具
        /// </summary>
        public List<base_dbrjzx> olddbrjzx { get; set; }
        /// <summary>
        /// 新刀柄刃具在线
        /// </summary>
        public List<base_dbrjzx> dbrjzx { get; set; }
    }
}

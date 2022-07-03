using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.LBJ;

namespace ZDMesModels.LBJ
{
    public class sys_jtfp_form
    {
        /// <summary>
        /// 技通
        /// </summary>
        public zxjc_t_jstc jstc { get; set; }
        /// <summary>
        /// 工厂代码
        /// </summary>
        public string gcdm { get; set; }
        /// <summary>
        /// 生产线
        /// </summary>
        public string scx { get; set; }
        public List<string> fpgw { get; set; }
        public string jxlist { get; set; }
        public string statusno { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string bz { get; set; }
        /// <summary>
        /// 人员列表
        /// </summary>
        public List<int> rylist { get; set; }
    }
}

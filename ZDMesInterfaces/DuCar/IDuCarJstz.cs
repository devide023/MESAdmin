using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using ZDMesModels.Ducar;

namespace ZDMesInterfaces.DuCar
{
    /// <summary>
    /// 技术通知接口
    /// </summary>
    public interface IDuCarJstz
    {
        /// <summary>
        /// 技通分配明细
        /// </summary>
        /// <param name="jtid"></param>
        /// <returns></returns>
        IEnumerable<zxjc_t_jstcfp> Jtfp_Detail(string jtid);
        /// <summary>
        /// 未分配技通列表
        /// </summary>
        /// <param name="parm"></param>
        /// <param name="resultcount"></param>
        /// <returns></returns>
        IEnumerable<zxjc_t_jstc> Wfp_JtTz_List(sys_page parm, out int resultcount);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using ZDMesModels.TJ.A1;

namespace ZDMesInterfaces.TJ
{
    /// <summary>
    /// 我的文档
    /// </summary>
    public interface IA1MyDoc
    {
        /// <summary>
        /// 我的技术通知列表
        /// </summary>
        /// <param name="parm"></param>
        /// <param name="resultcount"></param>
        /// <returns></returns>
        IEnumerable<zxjc_t_jstc> Get_MyJstz_List(sys_page parm, out int resultcount);
        /// <summary>
        /// 技术通知分配列表
        /// </summary>
        /// <param name="parm"></param>
        /// <param name="resultcount"></param>
        /// <returns></returns>
        IEnumerable<zxjc_t_jstc> Get_PDMFP_List(sys_page parm, out int resultcount);
    }
}

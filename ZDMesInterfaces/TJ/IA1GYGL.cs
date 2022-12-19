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
    /// 工艺管理接口
    /// </summary>
    public interface IA1GYGL
    {
        /// <summary>
        /// 工艺视频列表
        /// </summary>
        /// <param name="parm"></param>
        /// <param name="resultcount"></param>
        /// <returns></returns>
        IEnumerable<zxjc_t_dzgy> Get_GySpList(sys_page parm, out int resultcount);
        
    }
}

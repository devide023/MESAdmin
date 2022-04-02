using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using ZDMesModels.LBJ;

namespace ZDMesInterfaces.LBJ.RyMgr
{
    /// <summary>
    /// 人员管理接口
    /// </summary>
    public interface IRyJx
    {
        /// <summary>
        /// 获取人员绩效
        /// </summary>
        /// <param name="parm"></param>
        /// <param name="resultcount"></param>
        /// <returns></returns>
        IEnumerable<sys_ryjx> Get_RyJxList(sys_page parm,out int resultcount);
        

    }
}

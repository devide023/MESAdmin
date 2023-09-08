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
        /// <summary>
        /// 装配指导列表
        /// </summary>
        /// <param name="parm"></param>
        /// <param name="resultcount"></param>
        /// <returns></returns>
        IEnumerable<ZDMesModels.TJ.A1.GYZD.zxjc_t_dzgy> Get_ZpzdList(sys_page parm, out int resultcount);
        /// <summary>
        /// 操作规程列表
        /// </summary>
        /// <param name="parm"></param>
        /// <param name="resultcount"></param>
        /// <returns></returns>
        IEnumerable<ZDMesModels.TJ.A1.CZGC.zxjc_t_dzgy> Get_Czgc_List(sys_page parm, out int resultcount);
        /// <summary>
        /// MDS表列表
        /// </summary>
        /// <param name="parm"></param>
        /// <param name="resultcount"></param>
        /// <returns></returns>
        IEnumerable<ZDMesModels.TJ.A1.MDS.zxjc_t_dzgy> Get_Mds_List(sys_page parm, out int resultcount);
        /// <summary>
        /// 历史问题列表
        /// </summary>
        /// <param name="parm"></param>
        /// <param name="resultcount"></param>
        /// <returns></returns>
        IEnumerable<ZDMesModels.TJ.A1.LSWT.zxjc_t_dzgy> Get_Lswt_List(sys_page parm, out int resultcount);

    }
}

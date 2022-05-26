using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using ZDMesModels.LBJ;
namespace ZDMesInterfaces.LBJ.DaoJu
{
    public interface IDjReport
    {
        IEnumerable<base_dbrjzx> GetRjZxList(sys_page parm, out int resultcount);
        /// <summary>
        /// 查询生产线设备列表
        /// </summary>
        /// <param name="scx"></param>
        /// <returns></returns>
        IEnumerable<base_sbxx> Get_SbxxBy_Scx(string scx);
        /// <summary>
        /// 设备在线刀柄信息
        /// </summary>
        /// <param name="sbbh"></param>
        /// <returns></returns>
        IEnumerable<base_dbxx> Get_ZxDbXX_By_Sbbh(string scx,string sbbh);
    }
}

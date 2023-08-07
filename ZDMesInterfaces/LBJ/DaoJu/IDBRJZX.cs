using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.LBJ;

namespace ZDMesInterfaces.LBJ.DaoJu
{
    /// <summary>
    /// 刀柄刃具在线
    /// </summary>
    public interface IDBRJZX
    {
        /// <summary>
        /// 获取当前刀柄刃具在线信息
        /// </summary>
        /// <returns></returns>
        IEnumerable<base_dbrjzx> Get_DbRjZx_List(sys_dbrjgx_form form);
        /// <summary>
        /// 保存刀柄刃具在线信息
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        bool Save_DbRjZx_New(sys_save_dbrjzx_form form);
    }
}

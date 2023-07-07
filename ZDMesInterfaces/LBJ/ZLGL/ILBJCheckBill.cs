using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using ZDMesModels.LBJ;

namespace ZDMesInterfaces.LBJ.ZLGL
{
    public interface ILBJCheckBill
    {
        /// <summary>
        /// 互检待检列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<zxjc_check_bill> Hj_Check_Bill_List(sys_page parm, out int resultcount);
        /// <summary>
        /// 巡检待检列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<zxjc_check_bill> Xj_Check_Bill_List(sys_page parm, out int resultcount);
        /// <summary>
        /// 巡检审核
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        bool Xj_Audit(List<zxjc_check_bill> entitys);
        /// <summary>
        /// 互检审核
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        bool Hj_Audit(List<zxjc_check_bill> entitys);
    }
}

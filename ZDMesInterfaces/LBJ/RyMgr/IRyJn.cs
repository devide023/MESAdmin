using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesInterfaces.LBJ.RyMgr
{
    public interface IRyJn
    {
        /// <summary>
        /// 最大技能编号
        /// </summary>
        /// <returns></returns>
        int MaxJnNo();
        /// <summary>
        /// 是否存在技能编号
        /// </summary>
        /// <param name="jnno"></param>
        /// <returns></returns>
        bool IsExistJnNo(string jnno);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesInterfaces.LBJ.RyMgr
{
    public interface IRyXx
    {
        /// <summary>
        /// 获取员工最大数
        /// </summary>
        /// <returns></returns>
        int MaxUserCode();
        /// <summary>
        /// 用户编码是否存在
        /// </summary>
        /// <param name="usercode"></param>
        /// <returns></returns>
        bool IsExistUserCode(string usercode);
    }
}

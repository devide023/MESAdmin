using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesInterfaces.LBJ.DJGW
{
    public interface IDjGw
    {
        /// <summary>
        /// 最大点检岗位编号
        /// </summary>
        /// <returns></returns>
        int MaxDjNo();
        /// <summary>
        /// 是否存在点检编号
        /// </summary>
        /// <param name="djno"></param>
        /// <returns></returns>
        bool IsExistDjNo(string djno);
    }
}

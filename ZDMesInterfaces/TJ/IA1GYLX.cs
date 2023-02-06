using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesInterfaces.TJ
{
    public interface IA1GYLX
    {
        /// <summary>
        /// 机型是否存在
        /// </summary>
        /// <param name="jxno"></param>
        /// <returns></returns>
        bool IsExsit(string jxno);
        /// <summary>
        /// 机型+状态是否存在
        /// </summary>
        /// <param name="jxno"></param>
        /// <param name="statusno"></param>
        /// <returns></returns>
        bool IsExsit(string jxno,string statusno);
    }
}

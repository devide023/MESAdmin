using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesInterfaces.TJ
{
    public interface IJTFPSCX
    {
        /// <summary>
        /// 是否可以删除已分配到生产线的技通
        /// </summary>
        /// <param name="jcbh"></param>
        /// <returns></returns>
        bool CanRemove(string jcbh);
    }
}

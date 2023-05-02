using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesInterfaces.TJ
{
    public interface IA1JtFpzt
    {
        /// <summary>
        /// 设置技通已分配
        /// </summary>
        /// <param name="jtbh"></param>
        /// <returns></returns>
        bool Set_JtFpZt(string jtbh);
        /// <summary>
        /// 设置技通未分配
        /// </summary>
        /// <param name="jtbh"></param>
        /// <returns></returns>
        bool UnSet_JtFpZt(string jtbh);
    }
}

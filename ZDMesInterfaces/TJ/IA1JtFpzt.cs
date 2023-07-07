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
        /// 设置技通已分配(技通分配员功能)
        /// </summary>
        /// <param name="jtbh"></param>
        /// <returns></returns>
        bool Set_JtFpZt(string jtbh);
        /// <summary>
        /// 设置技通未分配(技通分配员功能)
        /// </summary>
        /// <param name="jtbh"></param>
        /// <returns></returns>
        bool UnSet_JtFpZt(string jtbh);
        /// <summary>
        /// 设置技通已分配(线长分配技通到岗位功能)
        /// </summary>
        /// <param name="jtbh"></param>
        /// <returns></returns>
        bool Set_JtFpYfpGwh(string jtbh);
        /// <summary>
        /// 设置技通未分配(线长分配技通到岗位功能)
        /// </summary>
        /// <param name="jtbh"></param>
        /// <returns></returns>
        bool UnSet_JtFpYfpGwh(string jtbh);
    }
}

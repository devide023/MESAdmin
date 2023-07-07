using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.TJ.A1;

namespace ZDMesInterfaces.TJ
{
    /// <summary>
    /// 无纸化检测
    /// </summary>
    public interface IA1JC
    {
        /// <summary>
        /// 所有检测类型
        /// </summary>
        /// <returns></returns>
        IEnumerable<zxjc_jclx> Get_All_JCLX();
    }
}

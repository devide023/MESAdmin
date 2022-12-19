using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.CDGC;

namespace ZDMesInterfaces.CDGC
{
    public interface I4MBHD
    {
        /// <summary>
        /// 闭环4M变化点
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        bool BHBHD(IEnumerable<lbj_qms_4mbhd> list);
    }
}

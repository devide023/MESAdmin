using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.CDGC;

namespace ZDMesInterfaces.CDGC
{
    /// <summary>
    /// 电机壳交接班
    /// </summary>
    public interface IDjkjjb
    {
        bool Save_Djkjjb(zxjc_djkjjb_bill bill);
    }
}

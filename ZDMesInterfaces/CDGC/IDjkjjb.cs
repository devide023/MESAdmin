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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="bill">表单</param>
        /// <param name="jjmx">机加明细</param>
        /// <param name="hxmx">后序明细</param>
        /// <returns></returns>
        bool Save_Djkjjb(zxjc_djkjjb_bill bill,List<zxjc_djkjjb_detail> jjmx,List<zxjc_djkjjb_hx_detail> hxmx);
    }
}

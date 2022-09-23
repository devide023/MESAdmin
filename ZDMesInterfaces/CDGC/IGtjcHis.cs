using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.CDGC;

namespace ZDMesInterfaces.CDGC
{
    public interface IGtjcHis
    {
        /// <summary>
        /// 缸体检测数据
        /// </summary>
        /// <param name="billid"></param>
        /// <returns></returns>
        zxjc_gtjc_bill Get_GtjcInfo(int billid);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.CDGC;

namespace ZDMesInterfaces.CDGC
{
    public interface IGtjc
    {
        /// <summary>
        /// 获取缸体产品类型
        /// </summary>
        /// <returns></returns>
        IEnumerable<zxjc_base_gtjc> Get_CPLX_List();
        /// <summary>
        /// 通过产品类型获取检测数据
        /// </summary>
        /// <param name="lx"></param>
        /// <returns></returns>
        IEnumerable<zxjc_base_gtjc> Get_Gtjc_By_LX(string lx);
    }
}

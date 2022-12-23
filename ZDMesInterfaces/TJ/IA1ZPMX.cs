using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.TJ.A1;
namespace ZDMesInterfaces.TJ
{
    public interface IA1ZPMX
    {
        /// <summary>
        /// 获取装配明细
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        IEnumerable<zxjc_data_detail_mx> Get_ZPMX_List(zxjc_data_list8 parm);
        /// <summary>
        /// 获取明细
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        IEnumerable<zxjc_data_detail> Get_Detail(zxjc_data_list8 parm);
    }
}

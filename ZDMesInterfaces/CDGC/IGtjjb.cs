using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.CDGC;
using ZDMesInterceptor.CDGC;
using Autofac.Extras.DynamicProxy;

namespace ZDMesInterfaces.CDGC
{
    [Intercept(typeof(JJBLog))]
    public interface IGtjjb
    {
        /// <summary>
        /// 保存缸体交接班信息
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        bool Save_Gtjjb(zxjc_gtjjb_bill bill);
        /// <summary>
        /// 根据日期，班次获取缸体交接班信息
        /// </summary>
        /// <param name="rq"></param>
        /// <param name="bc"></param>
        /// <returns></returns>
        IEnumerable<zxjc_gtjjb_bill> Get_Gtjjb_List_ByBc(string rq, string bc);
        /// <summary>
        /// 获取产品列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<dynamic> Get_CpList();
    }
}

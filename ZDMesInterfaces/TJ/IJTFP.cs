using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.TJ.A1;
using Autofac.Extras.DynamicProxy;
using ZDMesInterceptor.A1;

namespace ZDMesInterfaces.TJ
{
    [Intercept(typeof(JtFpLog))]
    public interface IJTFP
    {
        /// <summary>
        /// 判断提交的数据是否已分配
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        List<zxjc_t_jstcfp> IsDistribute(List<zxjc_t_jstcfp> list);
        /// <summary>
        /// 分配技通到生产线
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        bool Jstz_To_Scx(List<zxjc_t_jstc_scx> list);
        /// <summary>
        /// 是否已分配到生产线
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        List<zxjc_t_jstc_scx> IsJtToScx(List<zxjc_t_jstc_scx> list);
    }
}

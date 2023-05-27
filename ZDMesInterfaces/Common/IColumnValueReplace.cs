using Autofac.Extras.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterceptor;
using ZDMesInterceptor.LBJ;
using ZDMesModels;

namespace ZDMesInterfaces.Common
{
    /// <summary>
    /// 批量替换列值接口
    /// </summary>
    /// 
    [Intercept(typeof(ColValReplaceLog))]
    public interface IColumnValueReplace
    {
        /// <summary>
        /// 批量替换列值
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        int Replace_Column_Value(sys_colval_replace parm);
    }
}

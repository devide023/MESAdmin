using Autofac.Extras.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterceptor.LBJ;
using ZDMesModels;

namespace ZDMesInterfaces.LBJ.ImportData
{
    /// <summary>
    /// 数据导入接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Intercept(typeof(ImportLog))]
    public interface IImportData<T> where T: class, new()
    {
        /// <summary>
        /// 新增导入
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        sys_import_result<T> NewImportData(List<T> data);
        /// <summary>
        /// 替换导入
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        sys_import_result<T> ReaplaceImportData(List<T> data);
        /// <summary>
        /// 综合导入
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        sys_import_result<T> ZhImportData(List<T> data);
    }
}

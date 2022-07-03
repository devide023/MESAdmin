using Autofac.Extras.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterceptor.LBJ;
using ZDMesModels;

namespace ZDMesInterfaces.Common
{
    /// <summary>
    /// 基本增、删、改、查操作
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Intercept(typeof(CUDLogger))]
    public interface IDbOperate<T> where T : class, new()
    {
        /// <summary>
        /// 根据条件查询记录
        /// </summary>
        /// <param name="parm"></param>
        /// <param name="resultcount"></param>
        /// <returns></returns>
        IEnumerable<T> GetList(sys_page parm, out int resultcount);
        /// <summary>
        /// 新增记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        int Add(T entity);
        /// <summary>
        /// 批量新增记录
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        int Add(IEnumerable<T> entitys);
        /// <summary>
        /// 批量新增记录
        /// </summary>
        /// <param name="entitys"></param>
        /// <param name="noklist"></param>
        /// <returns></returns>
        int Add(IEnumerable<T> entitys,out IEnumerable<T> noklist);
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Del(T entity);
        /// <summary>
        /// 批量删除记录
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        bool Del(IEnumerable<T> entitys);
        /// <summary>
        /// 修改记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Modify(T entity);
        /// <summary>
        /// 批量修改记录
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        bool Modify(IEnumerable<T> entitys);

    }
}

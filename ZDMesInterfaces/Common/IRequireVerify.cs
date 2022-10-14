using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesInterfaces.Common
{
    /// <summary>
    /// 必填字段校验
    /// </summary>
    public interface IRequireVerify
    {
        /// <summary>
        /// 配置文件
        /// </summary>
        string VerifyConfigPath { get; set; }
        /// <summary>
        /// 必填字段校验
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entitys"></param>
        /// <returns></returns>
        bool VerifyRequire<T>(List<T> entitys);
        /// <summary>
        /// 必填字段校验,用于过滤器
        /// </summary>
        /// <param name="type"></param>
        /// <param name="entitys"></param>
        /// <returns></returns>
        bool VerifyRequire(Type type, List<object> entitys);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;

namespace ZDMesInterfaces.Common
{
    /// <summary>
    /// 数据检验接口
    /// </summary>
    public interface IVerifyData
    {
        /// <summary>
        /// 是否验证
        /// </summary>
        bool IsVerify { get; set; }
        /// <summary>
        /// 验证配置文件路径
        /// </summary>
        string VerifyConfigpath { get; set; }
        /// <summary>
        /// 验证数据合法性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entitys"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool Verify_Data<T>(List<T> entitys, out sys_result msg);
    }
}

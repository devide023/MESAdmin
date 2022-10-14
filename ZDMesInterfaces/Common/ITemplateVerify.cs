using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesInterfaces.Common
{
    /// <summary>
    /// 模板文件校验
    /// </summary>
    public interface ITemplateVerify
    {
        /// <summary>
        /// 配置文件
        /// </summary>
        string VerifyConfigPath { get; set; }
        /// <summary>
        /// 模板格式校验
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="templatename">上传模板文件名称</param>
        /// <returns></returns>
        bool ImportTemplateVerify<T>(string templatename);
        /// <summary>
        /// 模板格式校验，并输入模板内容数据
        /// </summary>
        /// <param name="entityname"></param>
        /// <param name="templatename">上传模板文件名称</param>
        /// <param name="list">返回模板文件内容</param>
        /// <returns></returns>
        bool ImportTemplateVerify(string entityname, string templatename, out List<object> list);
    }
}

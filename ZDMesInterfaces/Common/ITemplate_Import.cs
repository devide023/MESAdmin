using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;

namespace ZDMesInterfaces.Common
{
    /// <summary>
    /// 数据模板导入接口
    /// </summary>
    public interface ITemplate_Import
    {
        string TemplateConfig { get; set; }
        /// <summary>
        /// 读取数据模板数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filepath"></param>
        /// <param name="nofields"></param>
        /// <returns></returns>
        IEnumerable<T> ReadData<T>(string filepath);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesInterfaces.DuCar
{
    /// <summary>
    /// 故障及处理方式
    /// </summary>
    public interface IDuCarFault_CLFS
    {
        /// <summary>
        /// 生成处理方式代码
        /// </summary>
        /// <returns></returns>
        string Create_CLFS_No();
        /// <summary>
        /// 通过故障名称返回故障代码
        /// </summary>
        /// <param name="faultname"></param>
        /// <returns></returns>
        string GetFaultNoByName(string faultname);
    }
}

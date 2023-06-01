using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using ZDMesModels.Ducar;

namespace ZDMesInterfaces.DuCar
{
    /// <summary>
    /// 夹具绑定关系
    /// </summary>
    public interface IDuCarJjGxb
    {
        /// <summary>
        /// 绑定夹具与件号关系
        /// </summary>
        /// <returns></returns>
        sys_result BindJjGxb(sys_bind_parm parm);
        /// <summary>
        /// 解绑夹具与件号关系
        /// </summary>
        /// <returns></returns>
        sys_result UnbindJjGxb(sys_bind_parm parm);
    }
}

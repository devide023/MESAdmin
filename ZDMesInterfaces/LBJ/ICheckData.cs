using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesInterfaces.LBJ
{
    /// <summary>
    ///数据检查
    /// </summary>
    public interface ICheckData
    {
        /// <summary>
        /// 数据是否有效
        /// </summary>
        /// <returns></returns>
        bool Valid<T>(string colname, object data);

    }
}

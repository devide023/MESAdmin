using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesInterfaces.TJ
{
    /// <summary>
    /// 批量赋值
    /// </summary>
    public  interface IBatAtachValue<T>
    {
        List<T> BatSetValue(List<T> list);
    }
}

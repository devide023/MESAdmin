using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesInterfaces.Common
{
    public interface IEntityDetail<T>
    {
        IEnumerable<T> Details(params object[] parm);
    }
}

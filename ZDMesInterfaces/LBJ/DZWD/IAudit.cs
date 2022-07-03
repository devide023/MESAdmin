using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesInterfaces.LBJ.DZWD
{
    public interface IAudit<T>
    {
        /// <summary>
        /// 单据审核
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        bool AuditBill(List<string> jtids);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.Ducar;

namespace ZDMesServices.Ducar.SbMgr
{
    /// <summary>
    /// 夹具关系
    /// </summary>
    internal class DuCarJjGxService : BaseDao<jjgxb>
    {
        public DuCarJjGxService(string constr) : base(constr)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.Ducar;

namespace ZDMesServices.Ducar.SbMgr
{
    /// <summary>
    /// 夹具关系流水
    /// </summary>
    public class DuCarJjGxLsService : BaseDao<jjgxbls>
    {
        public DuCarJjGxLsService(string constr) : base(constr)
        {
        }
    }
}

using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.TJ;
using ZDMesModels.Ducar;

namespace ZDMesServices.Ducar.GyMgr
{
    public class DuCarGwzdService : BaseDao<base_gwzd>
    {
        public DuCarGwzdService(string constr) : base(constr)
        {
        }

    }
}

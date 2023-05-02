using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.Ducar;

namespace ZDMesServices.Ducar.ZlMgr
{
    public class DuCarFxmxService : BaseDao<zxjc_gwzd_fxmx>
    {
        public DuCarFxmxService(string constr) : base(constr)
        {
        }
    }
}

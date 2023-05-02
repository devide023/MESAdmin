using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.Ducar;

namespace ZDMesServices.Ducar.RyMgr
{
    public class DuCarRyjcService : BaseDao<zxjc_jcgl>
    {
        public DuCarRyjcService(string constr) : base(constr)
        {
        }
    }
}

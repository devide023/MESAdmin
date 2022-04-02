using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.Common;
using ZDMesModels.LBJ;
namespace ZDMesServices.LBJ.RyMgr
{
    public class RYJCService:BaseDao<zxjc_jcgl>
    {
        public RYJCService(string constr):base(constr)
        {

        }
    }
}

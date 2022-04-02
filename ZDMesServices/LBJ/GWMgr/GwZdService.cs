using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.Common;
using ZDMesModels.LBJ;
namespace ZDMesServices.LBJ.GWMgr
{
    public class GwZdService:BaseDao<base_gwzd>
    {
        public GwZdService(string constr):base(constr)
        {

        }
    }
}

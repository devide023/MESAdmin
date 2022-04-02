using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.Common;
using ZDMesModels.LBJ;
namespace ZDMesServices.LBJ.SBMgr
{
    public class SbxxService:BaseDao<base_sbxx>
    {
        public SbxxService(string constr):base(constr)
        {

        }
    }
}

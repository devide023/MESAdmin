using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.CDGC;

namespace ZDMesServices.CDGC.SBGL
{
    public class SBGLService: BaseDao<base_sbxx>
    {
        public SBGLService(string constr) : base(constr)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.Common;
using ZDMesModels.CDGC;

namespace ZDMesServices.CDGC.YCGL
{
    public class YCGLService:BaseDao<ad_bjxx>
    {
        public YCGLService(string constr) : base(constr)
        {
        }
    }
}

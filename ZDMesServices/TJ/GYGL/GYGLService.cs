using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.TJ;

namespace ZDMesServices.TJ.GYGL
{
    public class GYGLService: BaseDao<zxjc_jrgy>
    {
        public GYGLService(string constr) : base(constr)
        {

        }
    }
}

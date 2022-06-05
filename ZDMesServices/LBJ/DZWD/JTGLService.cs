using Autofac.Extras.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterceptor.LBJ;
using ZDMesModels.LBJ;
namespace ZDMesServices.LBJ.DZWD
{
    public class JTGLService:BaseDao<zxjc_t_jstc>
    {
        public JTGLService(string constr):base(constr)
        {

        }
    }
}

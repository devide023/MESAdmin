using Autofac.Extras.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterceptor.LBJ;
using ZDMesModels.LBJ;
namespace ZDMesServices.LBJ.DJXX
{
    public class DJXXService:BaseDao<zxjc_djxx>
    {
        public DJXXService(string constr):base(constr)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesModels.TJ.A1;
using ZDMesInterfaces.Common;
namespace MesAdmin.Controllers.A1.SBGL
{
    [RoutePrefix("api/a1/sbxx")]
    public class A1SBXXController : BaseApiController<base_sbxx>
    {
        public A1SBXXController(IDbOperate<base_sbxx> sbxxservice):base(sbxxservice)
        {

        }
    }
}
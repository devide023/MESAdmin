using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesModels.TJ.A1;
using ZDMesInterfaces.Common;
namespace MesAdmin.Controllers.A1.JTGL
{
    [RoutePrefix("api/a1/scxtz")]
    public class A1SCXTZController : BaseApiController<zxjc_scx_tz>
    {
        public A1SCXTZController(IDbOperate<zxjc_scx_tz> scxtzservice):base(scxtzservice)
        {

        }
    }
}
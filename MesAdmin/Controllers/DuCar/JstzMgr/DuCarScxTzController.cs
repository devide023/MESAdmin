using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesModels.Ducar;

namespace MesAdmin.Controllers.DuCar.JstzMgr
{
    [RoutePrefix("api/ducar/scxtz")]
    public class DuCarScxTzController : BaseApiController<zxjc_scx_tz>
    {
        public DuCarScxTzController(IDbOperate<zxjc_scx_tz> baseservice) : base(baseservice)
        {
        }
    }
}
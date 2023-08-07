using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesModels.Ducar;

namespace MesAdmin.Controllers.DuCar.Test
{
    [RoutePrefix("api/ducar/test")]
    public class DuCarTestController : BaseApiController<base_fxwl>
    {
        public DuCarTestController(IDbOperate<base_fxwl> baseservice) : base(baseservice)
        {
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesModels.Ducar;

namespace MesAdmin.Controllers.DuCar.ZlMgr
{
    [RoutePrefix("api/ducar/djjl")]
    public class DuCarDjJlController : BaseApiController<zxjc_djxx>
    {
        public DuCarDjJlController(IDbOperate<zxjc_djxx> baseservice) : base(baseservice)
        {
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesModels.Ducar;

namespace MesAdmin.Controllers.DuCar.SbMgr
{
    [RoutePrefix("api/ducar/sbxx")]
    public class DuCarSbxxController : BaseApiController<base_sbxx>
    {
        public DuCarSbxxController(IDbOperate<base_sbxx> baseservice) : base(baseservice)
        {
        }
    }
}
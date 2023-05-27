using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesModels.TJ.A1;

namespace MesAdmin.Controllers.A1.Report
{
    [RoutePrefix("api/a1/mjghjl")]
    public class A1MjghjlController : BaseApiController<zxjc_mjghjl>
    {
        public A1MjghjlController(IDbOperate<zxjc_mjghjl> baseservice) : base(baseservice)
        {
        }
    }
}
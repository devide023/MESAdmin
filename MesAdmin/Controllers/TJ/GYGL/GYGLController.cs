using MesAdmin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesModels;
using ZDMesModels.LBJ;
using ZDMesModels.TJ;

namespace MesAdmin.Controllers.TJ.GYGL
{
    [RoutePrefix("api/tj/gygl/jrgy")]
    public class GYGLController : BaseApiController<zxjc_jrgy>
    {
        public GYGLController(IDbOperate<zxjc_jrgy> baseservice):base(baseservice)
        {

        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesModels.TJ;

namespace MesAdmin.Controllers.TJ.GYGL
{
    [RoutePrefix("api/tj/gygl/jrgy")]
    public class JRGYController : BaseApiController<zxjc_jrgy>
    {
        public JRGYController(IDbOperate<zxjc_jrgy> baseservice) : base(baseservice)
        {

        }        
    }
}
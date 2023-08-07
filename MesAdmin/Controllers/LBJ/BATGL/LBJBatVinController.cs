using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesModels.LBJ;

namespace MesAdmin.Controllers.LBJ.BATGL
{
    [RoutePrefix("api/lbj/batgl")]
    public class LBJBatVinController : BaseApiController<barcode_print>
    {
        public LBJBatVinController(IDbOperate<barcode_print> baseservice) : base(baseservice)
        {
        }
    }
}
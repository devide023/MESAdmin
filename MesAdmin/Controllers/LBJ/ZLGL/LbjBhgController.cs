using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesModels.LBJ;

namespace MesAdmin.Controllers.LBJ.ZLGL
{
    [RoutePrefix("api/lbj/scbhg")]
    public class LbjBhgController : BaseApiController<sc_zxjc_bhg>
    {
        public LbjBhgController(IDbOperate<sc_zxjc_bhg> baseservice) : base(baseservice)
        {
        }
    }
}
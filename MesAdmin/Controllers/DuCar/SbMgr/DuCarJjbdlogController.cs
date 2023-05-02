using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesModels;
using ZDMesModels.Ducar;

namespace MesAdmin.Controllers.DuCar.SbMgr
{
    [RoutePrefix("api/ducar/jjgxls")]
    public class DuCarJjbdlogController : BaseApiController<jjgxbls>
    {
        public DuCarJjbdlogController(IDbOperate<jjgxbls> baseservice) : base(baseservice)
        {
        }
        public override IHttpActionResult GetList(sys_page parm)
        {
            parm.default_order_colname = "bdsj";
            return base.GetList(parm);
        }
    }
}
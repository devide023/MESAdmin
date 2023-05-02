using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesModels;
using ZDMesModels.Ducar;

namespace MesAdmin.Controllers.DuCar.PLC
{
    [RoutePrefix("api/ducar/iozt")]
    public class DuCarIOZTController : BaseApiController<pcs_stationinout_state>
    {
        public DuCarIOZTController(IDbOperate<pcs_stationinout_state> baseservice) : base(baseservice)
        {
        }
        public override IHttpActionResult GetList(sys_page parm)
        {
            parm.default_order_colname = "time";
            return base.GetList(parm);
        }
    }
}
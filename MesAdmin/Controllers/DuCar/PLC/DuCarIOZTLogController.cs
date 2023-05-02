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
    [RoutePrefix("api/ducar/iolog")]
    public class DuCarIOZTLogController : BaseApiController<pcs_stationinout_log>
    {
        public DuCarIOZTLogController(IDbOperate<pcs_stationinout_log> baseservice) : base(baseservice)
        {
        }
        public override IHttpActionResult GetList(sys_page parm)
        {
            parm.default_order_colname = "time";
            return base.GetList(parm);
        }
    }
}
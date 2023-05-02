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
    [RoutePrefix("api/ducar/plclog")]
    public class DuCarPLCJHZTLogController : BaseApiController<pcs_plcdata_log>
    {
        public DuCarPLCJHZTLogController(IDbOperate<pcs_plcdata_log> baseservice) : base(baseservice)
        {
        }
        public override IHttpActionResult GetList(sys_page parm)
        {
            try
            {
                parm.default_order_colname = "time";
                return base.GetList(parm);
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
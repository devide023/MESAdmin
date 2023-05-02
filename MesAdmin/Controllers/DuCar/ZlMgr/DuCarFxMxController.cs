using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesModels;
using ZDMesModels.Ducar;

namespace MesAdmin.Controllers.DuCar.ZlMgr
{
    [RoutePrefix("api/ducar/fxmx")]
    public class DuCarFxMxController : BaseApiController<zxjc_gwzd_fxmx>
    {
        public DuCarFxMxController(IDbOperate<zxjc_gwzd_fxmx> baseservice) : base(baseservice)
        {
        }
        public override IHttpActionResult GetList(sys_page parm)
        {
            try
            {
                parm.default_order_colname = "jcsj";
                return base.GetList(parm);
            }
            catch (Exception)
            {

                throw;
            }            
        }
    }
}
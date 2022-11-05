using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesModels.LBJ;
using ZDMesInterfaces.Common;
namespace MesAdmin.Controllers.LBJ.DAOJU
{
    [RoutePrefix("api/lbj/rjrmls")]
    public class LbjRjRmLsController : BaseApiController<zxjc_rjrm_ls>
    {
        public LbjRjRmLsController(IDbOperate<zxjc_rjrm_ls> rjrmlsservice):base(rjrmlsservice)
        {

        }
    }
}
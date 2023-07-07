using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesModels.Ducar;

namespace MesAdmin.Controllers.DuCar.WlMgr
{
    /// <summary>
    /// 物料校验记录 
    /// </summary>
    /// 
    [RoutePrefix("api/ducar/wljy")]
    public class DuCarWLJYController : BaseApiController<zxjc_gwzd_wljl>
    {
        public DuCarWLJYController(IDbOperate<zxjc_gwzd_wljl> baseservice) : base(baseservice)
        {
        }
    }
}
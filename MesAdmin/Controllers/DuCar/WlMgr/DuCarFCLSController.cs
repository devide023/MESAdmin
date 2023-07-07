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
    /// 辅材流水
    /// </summary>
    /// 
    [RoutePrefix("api/ducar/fcls")]
    public class DuCarFCLSController : BaseApiController<zxjc_fcls>
    {
        public DuCarFCLSController(IDbOperate<zxjc_fcls> baseservice) :base(baseservice)
        {

        }
    }
}
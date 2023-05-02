using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesModels.Ducar;

namespace MesAdmin.Controllers.DuCar.JhMgr
{
    /// <summary>
    /// 生产订单组件
    /// </summary>
    [RoutePrefix("api/ducar/scddzj")]
    public class DuCarDDZPController : BaseApiController<pp_scddzj>
    {
        public DuCarDDZPController(IDbOperate<pp_scddzj> baseservice) : base(baseservice)
        {
        }
    }
}
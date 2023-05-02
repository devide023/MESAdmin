using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesModels;
using ZDMesModels.TJ.A1;

namespace MesAdmin.Controllers.A1.JTGL
{
    /// <summary>
    /// 技通浏览记录
    /// </summary>
    /// 
    [RoutePrefix("api/a1/jtck")]
    public class A1JTLLJLController : BaseApiController<mes_pdm_jstz_yd>
    {
        public A1JTLLJLController(IDbOperate<mes_pdm_jstz_yd> baseservice) : base(baseservice)
        {
        }

    }
}
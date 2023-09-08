using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesModels.TJ.A1;

namespace MesAdmin.Controllers.A1.SJZSGL
{
    /// <summary>
    /// 曲轴窜动检测
    /// </summary>
    /// 
    [RoutePrefix("api/a1/qzcdjc")]
    public class A1QZCDJCController : BaseApiController<zxjc_qzcdjc>
    {
        public A1QZCDJCController(IDbOperate<zxjc_qzcdjc> baseservice) : base(baseservice)
        {
        }
    }
}
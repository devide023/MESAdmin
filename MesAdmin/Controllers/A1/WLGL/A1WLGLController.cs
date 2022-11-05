using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesModels.TJ.A1;
using ZDMesInterfaces.Common;
namespace MesAdmin.Controllers.A1.WLGL
{
    [RoutePrefix("api/a1/gwwl")]
    public class A1WLGLController : BaseApiController<v_base_gwbj>
    {
        public A1WLGLController(IDbOperate<v_base_gwbj> gwbjservice):base(gwbjservice)
        {

        }
    }
}
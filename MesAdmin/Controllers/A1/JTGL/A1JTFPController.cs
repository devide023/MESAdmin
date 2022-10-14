using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MesAdmin.Filters;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ.ImportData;
using ZDMesModels.TJ.A1;

namespace MesAdmin.Controllers.A1.JTGL
{
    [RoutePrefix("api/a1/jtfp")]
    public class A1JTFPController : BaseApiController<zxjc_t_jstcfp>
    {
        public A1JTFPController(IDbOperate<zxjc_t_jstcfp> jtfpservice):base(jtfpservice)
        {

        }
    }
}
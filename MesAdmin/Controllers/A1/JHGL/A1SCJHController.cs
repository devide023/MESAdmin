using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesModels.TJ.A1;
using ZDMesInterfaces.Common;
namespace MesAdmin.Controllers.A1.JHGL
{
    /// <summary>
    /// 生产计划
    /// </summary>
    [RoutePrefix("api/a1/scjh")]
    public class A1SCJHController : BaseApiController<tj_pp_sc_zpjh>
    {
        public A1SCJHController(IDbOperate<tj_pp_sc_zpjh> scjhservice):base(scjhservice)
        {

        }
    }
}
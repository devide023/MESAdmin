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
    /// 采购计划
    /// </summary>
    [RoutePrefix("api/a1/cgjh")]
    public class A1CGJHController : BaseApiController<tj_mm_cgjh>
    {
        public A1CGJHController(IDbOperate<tj_mm_cgjh> cgjhservice):base(cgjhservice)
        {

        }
    }
}
using MesAdmin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.TJ;
using ZDMesModels;

namespace MesAdmin.Controllers.A1.SJZSGL
{
    [RoutePrefix("api/a1/sjzs/ddzs")]
    public class A1DDZSController : ApiController
    {
        private IA1DDZS _ddzsservice;
        public A1DDZSController(IA1DDZS ddzsservice)
        {
            _ddzsservice = ddzsservice;
        }

        [HttpPost, SearchFilter, Route("list")]
        public virtual IHttpActionResult GetList(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _ddzsservice.Get_VinList(parm, out resultcount);
                return Json(new
                {
                    code = 1,
                    msg = "ok",
                    resultcount = resultcount,
                    list = list
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
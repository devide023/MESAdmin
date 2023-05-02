using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.TJ;
using ZDMesModels;
using ZDMesModels.TJ.A1;

namespace MesAdmin.Controllers.A1.JTGL
{
    [RoutePrefix("api/a1/myjt")]
    public class A1MyJTController : BaseApiController<zxjc_t_jstc>
    {
        private IA1MyDoc _mydoc;
        public A1MyJTController(IDbOperate<zxjc_t_jstc> baseservice,IA1MyDoc mydoc) : base(baseservice)
        {
            _mydoc= mydoc;
        }

        public override IHttpActionResult GetList(sys_page parm)
        {
            int resultcount = 0;
            var list = _mydoc.Get_MyJstz_List(parm, out resultcount);
            return Json(new
            {
                code = 1,
                msg = "ok",
                resultcount = resultcount,
                list = list
            });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Web.Http;
using ZDMesInterfaces.ZSKJ;

namespace MesAdmin.Controllers.LBJ.ZLGL
{
    [RoutePrefix("api/lbj/zskj")]
    public class LBJSZBController : ApiController
    {
        private IZSKJ _zskjservice;
        public LBJSZBController(IZSKJ zskjservice)
        {
            _zskjservice = zskjservice;
        }

        [HttpGet,Route("dxdh_list")]
        public IHttpActionResult Get_CXDH_List()
        {
            try
            {
                var list = _zskjservice.Get_CXDH_List();
                return Json(new { code = 1, msg = "ok", list = list });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet, Route("jcsx_list")]
        public IHttpActionResult Get_JCSX_List(string cxdh)
        {
            try
            {
                var list = _zskjservice.Get_Jcsx_By_CXDH(cxdh);
                return Json(new { code = 1, msg = "ok", list = list });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
using MesAdmin.Filters;
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

namespace MesAdmin.Controllers.A1.SJZSGL
{
    /// <summary>
    /// 单台追溯
    /// </summary>
    /// 
    [RoutePrefix("api/a1/sjzs/dtzs")]
    public class A1DTZSController : ApiController
    {
        private IDbOperate<zxjc_data_list8> _zxjcservice;
        private IA1ZPMX _zpmxservice;
        public A1DTZSController(IDbOperate<zxjc_data_list8> zxjcservice, IA1ZPMX zpmxservice)
        {
            _zxjcservice = zxjcservice;
            _zpmxservice = zpmxservice; 
        }

        [HttpPost, SearchFilter, Route("list")]
        public virtual IHttpActionResult GetList(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _zxjcservice.GetList(parm, out resultcount);
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
        [HttpPost,Route("zpmx")]
        public IHttpActionResult Get_ZPMX_List(zxjc_data_list8 parm)
        {
            try
            {
               var list = _zpmxservice.Get_ZPMX_List(parm);
                return Json(new
                {
                    code = 1,
                    msg = "ok",
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
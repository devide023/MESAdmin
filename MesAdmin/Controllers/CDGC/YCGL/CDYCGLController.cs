using MesAdmin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesModels;
using ZDMesModels.CDGC;

namespace MesAdmin.Controllers.CDGC.YCGL
{
    [RoutePrefix("api/cdgc/cdycgl")]
    public class CDYCGLController : ApiController
    {
        private IDbOperate<ad_bjxx> _bjxxservice;
        private IDbOperate<ad_bjxxls> _bjlsxxservice;
        public CDYCGLController(IDbOperate<ad_bjxx> bjxxservice, IDbOperate<ad_bjxxls> bjlsxxservice)
        {
            _bjxxservice = bjxxservice;
            _bjlsxxservice = bjlsxxservice;
        }
        [HttpPost,SearchFilter,Route("list")]
        public IHttpActionResult Get_List(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _bjxxservice.GetList(parm, out resultcount);
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
        [HttpPost,SearchFilter,Route("ycls_list")]
        public IHttpActionResult Get_Ycls_List(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _bjlsxxservice.GetList(parm, out resultcount);
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
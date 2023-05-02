using MesAdmin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ.SJBL;
using ZDMesModels;
using ZDMesModels.LBJ;

namespace MesAdmin.Controllers.LBJ.CPGL
{
    [RoutePrefix("api/lbj/cpgl")]
    public class LBJSjBlController : ApiController
    {
        private IDbOperate<zxjc_gdxxb> _gdxxservice;
        private IZxjcData _zxjcdata;
        public LBJSjBlController(IDbOperate<zxjc_gdxxb> gdxxservice, IZxjcData zxjcdata)
        {
            _gdxxservice = gdxxservice;
            _zxjcdata = zxjcdata;
        }
        [HttpPost,SearchFilter,Route("gdxxlist")]
        public IHttpActionResult GetList(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _gdxxservice.GetList(parm, out resultcount);
                return Json(new sys_search_result()
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

        [HttpPost, SearchFilter, Route("datalist")]
        public IHttpActionResult Get_Zxjc_DataList(sys_page parm)
        {
            try
            {
                var list = _zxjcdata.Get_Lbj_Zxjc_Data_List(parm);
                return Json(new sys_search_result()
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
using MesAdmin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesModels;
using ZDMesModels.CDGC;

namespace MesAdmin.Controllers.CDGC.JJBGL
{
    [RoutePrefix("api/cdgc/gttf")]
    public class CDGC_GTTF_Controller : ApiController
    {
        private IDbOperate<zxjc_gtjjb_gfmx> _gttfservice;
        public CDGC_GTTF_Controller(IDbOperate<zxjc_gtjjb_gfmx> gttfservice)
        {
            _gttfservice = gttfservice;
        }

        [HttpPost, SearchFilter, Route("list")]
        public IHttpActionResult GetList(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _gttfservice.GetList(parm, out resultcount);
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
        [HttpPost, Route("edit")]
        public IHttpActionResult Edit(List<zxjc_gtjjb_gfmx> entitys)
        {
            try
            {
                var q = entitys.Where(t => string.IsNullOrEmpty(t.tfr));
                if (q.Count() > 0)
                {
                    return Json(new 
                    {
                        code = 0,
                        msg = "请录入退废人"
                    });
                }
                var ret = _gttfservice.Modify(entitys);
                if (ret)
                {
                    return Json(new sys_result()
                    {
                        code = 1,
                        msg = "数据保存成功"
                    });
                }
                else
                {
                    return Json(new sys_result()
                    {
                        code = 0,
                        msg = "数据保存失败"
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
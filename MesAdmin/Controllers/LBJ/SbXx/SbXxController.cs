using MesAdmin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls.WebParts;
using ZDMesInterfaces.Common;
using ZDMesModels;
using ZDMesModels.LBJ;
namespace MesAdmin.Controllers.LBJ.SbXx
{
    [RoutePrefix("api/lbj/sbxx")]
    public class SbXxController : ApiController
    {
        private IDbOperate<base_sbxx> _sbxxservice;
        public SbXxController(IDbOperate<base_sbxx> sbxxservice)
        {
            _sbxxservice = sbxxservice;
        }
        [HttpPost, SearchFilter, Route("list")]
        public IHttpActionResult GetList(sys_page parm)
        {
            int resultcount = 0;
            var list = _sbxxservice.GetList(parm, out resultcount);
            return Json(new sys_search_result()
            {
                code = 1,
                msg = "ok",
                resultcount = resultcount,
                list = list
            });
        }
        [HttpPost, Route("edit")]
        public IHttpActionResult Edit(List<base_sbxx> entitys)
        {
            try
            {
                var ret = _sbxxservice.Modify(entitys);
                if (ret)
                {
                    return Json(new sys_result()
                    {
                        code = 1,
                        msg = "数据修改成功"
                    });
                }
                else
                {
                    return Json(new sys_result()
                    {
                        code = 0,
                        msg = "数据修改失败"
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
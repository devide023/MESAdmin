using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesModels.CDGC;
using System.Text;
using ZDMesModels;

namespace MesAdmin.Controllers.CDGC.RYGL
{
    [RoutePrefix("api/cdgc/ryxx")]
    public class RYGLController : BaseApiController<zxjc_ryxx>
    {
        private IDbOperate<zxjc_ryxx> _ryxxservice;
        public RYGLController(IDbOperate<zxjc_ryxx> ryxxservice) : base(ryxxservice)
        {
            _ryxxservice = ryxxservice;
        }
        [HttpPost,Route("add_bycode")]
        public override IHttpActionResult Add(List<zxjc_ryxx> entitys)
        {
            try
            {
                var ret = _ryxxservice.Add(entitys);
                if (ret > 0)
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
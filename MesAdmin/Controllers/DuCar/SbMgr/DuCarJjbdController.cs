using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI.WebControls.WebParts;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.DuCar;
using ZDMesModels;
using ZDMesModels.Ducar;

namespace MesAdmin.Controllers.DuCar.SbMgr
{
    [RoutePrefix("api/ducar/jjgx")]
    public class DuCarJjbdController : BaseApiController<jjgxb>
    {
        private IDuCarJjGxb _jjgxbs;
        public DuCarJjbdController(IDbOperate<jjgxb> baseservice, IDuCarJjGxb jjgxbs) : base(baseservice)
        {
            _jjgxbs = jjgxbs;
        }
        public override IHttpActionResult GetList(sys_page parm)
        {
            parm.default_order_colname = "bdsj";
            return base.GetList(parm);
        }

        [HttpPost,Route("bind")]
        public IHttpActionResult Bind_JJGX(sys_bind_parm parm)
        {
            try
            {
                var ret = _jjgxbs.BindJjGxb(parm);
                return Json(ret);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost,Route("unbind")]
        public IHttpActionResult UnBind_JJGX(List<jjgxb> entitys)
        {
            try
            {
                foreach (var item in entitys)
                {
                    var ret = _jjgxbs.UnbindJjGxb(new sys_bind_parm() {jjh = item.jjh,vin=item.engine_no,scx=item.scx,gwh=item.gwh});
                }
                return Json(new { code = 1, msg = "ok" });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
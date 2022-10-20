using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MesAdmin.Filters;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.TJ;
using ZDMesModels.TJ.A1;

namespace MesAdmin.Controllers.A1.JTGL
{
    [RoutePrefix("api/a1/jtfp")]
    public class A1JTFPController : BaseApiController<zxjc_t_jstcfp>
    {
        private IUser _user;
        private IJTFP _jtfp;
        public A1JTFPController(IDbOperate<zxjc_t_jstcfp> jtfpservice, IUser user, IJTFP jtfp) : base(jtfpservice)
        {
            _user = user;
            _jtfp = jtfp;
        }
        [HttpPost, Route("jstztoscx")]
        public IHttpActionResult JstzToScx(sys_jstz_to_scx_form form)
        {
            try
            {
                List<zxjc_t_jstc_scx> postdata = new List<zxjc_t_jstc_scx>();
                postdata.Add(new zxjc_t_jstc_scx()
                {
                    jcbh = form.jcbh,
                    lrr = _user.CurrentUser().name,
                    scx = form.scx,
                });
                var sftoscx = _jtfp.IsJtToScx(postdata);
                if (sftoscx.Count == 0)
                {
                    var isok = _jtfp.Jstz_To_Scx(postdata);
                    if (isok)
                    {
                        return Json(new { code = 1, msg = $"技通编号:{form.jcbh}配到生产线" });
                    }
                    else
                    {
                        return Json(new { code = 0, msg = $"技通分配到生产线失败" });
                    }
                }
                else
                {
                    string msg = string.Empty;
                    sftoscx.Select(t => t.jcbh).Distinct().ToList().ForEach(t => msg = msg + t + ",");
                    return Json(new { code = 0, msg = $"技通编号:{msg}已分配到生产线,请勿重复分配" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost, Route("distribute")]
        public IHttpActionResult Distribute(sys_jtfp_form form)
        {
            try
            {
                List<zxjc_t_jstcfp> postdata = new List<zxjc_t_jstcfp>();
                foreach (var item in form.gwh)
                {
                    foreach (var sitem in form.statusno)
                    {
                        postdata.Add(new zxjc_t_jstcfp()
                        {
                            jtid = form.jtid,
                            jxno = form.jxno,
                            gwh = item,
                            statusno = sitem,
                            lrr1 = _user.CurrentUser().name,
                            lrsj1 = DateTime.Now,
                            bz = form.bz,
                        });
                    }
                }
                var retlist = _jtfp.IsDistribute(postdata);
                if (retlist.Count > 0)
                {
                    string msg = string.Empty;
                    retlist.Select(t => t.jtid).Distinct().ToList().ForEach(t => msg = msg + t + ",");
                    return Json(new { code = 0, msg = $"技通编号:{msg}已分配到该岗位、机型、状态下" });
                }
                else
                {
                    return base.Add(postdata);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
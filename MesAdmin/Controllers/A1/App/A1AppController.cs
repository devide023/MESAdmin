using System;
using System.Linq;
using System.Web.Http;
using ZDMesInterfaces.TJ;
using ZDMesModels.TJ.A1;

namespace MesAdmin.Controllers.A1.App
{
    [RoutePrefix("api/tjapp")]
    public class A1AppController : ApiController
    {
        private IA1App _appservice;
        private  IA1BaseInfo _a1baseinfo;
        public A1AppController(IA1App appservice, IA1BaseInfo a1baseinfo)
        {
            _appservice= appservice;
            _a1baseinfo = a1baseinfo;
        }
        [HttpGet,AllowAnonymous, Route("scx_list")]
        public IHttpActionResult Get_ScxList()
        {
            try
            {
                var list = _a1baseinfo.Get_All_ScxList();
                return Json(new { code = 1, msg = "ok", list = list });
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost, AllowAnonymous,Route("jclx_list")]
        public IHttpActionResult Get_JCLX(sys_jclx_form parm)
        {
            try
            {
                var list = _appservice.Get_JCLX(parm);
                return Json(new { code = 1, msg = "ok", list = list });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost, AllowAnonymous, Route("jcxm_list")]
        public IHttpActionResult Get_JCXM_List(sys_jcmx_form parm)
        {
            try
            {
                var list = _appservice.Get_JCXM(parm);
                return Json(new { code = 1, msg = "ok", list = list });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost, AllowAnonymous, Route("jcbills")]
        public IHttpActionResult Get_JCLX(zxjc_jcbill bill)
        {
            try
            {
                var list = _appservice.Get_JCBills(bill);
                return Json(new { code = 1, msg = "ok", list = list });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet, AllowAnonymous, Route("jdqr_jcxs")]
        public IHttpActionResult Get_JDQRList(string billid)
        {
            try
            {
                var list = _appservice.Get_JDQR_JcxList(billid);
                return Json(new { code = 1, msg = "ok", list = list });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost, AllowAnonymous,Route("savebill")]
        public IHttpActionResult Save_JCBill(sys_app_jc_form form)
        {
            try
            {
                var ret = _appservice.Save_App_JCMX(form);
                if (ret)
                {
                    return Json(new { code = 1, msg = "数据保存成功" });
                }
                else
                {
                    return Json(new { code = 0, msg = "数据保存失败" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost,AllowAnonymous,Route("jdqr")]
        public IHttpActionResult JDQR(sys_jcbill_jdqr form)
        {
            try
            {
                var ret = _appservice.JcBill_JDQR(form);
                if (ret)
                {
                    return Json(new { code = 1, msg = "数据保存成功" });
                }
                else
                {
                    return Json(new { code = 0, msg = "数据保存失败" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost, AllowAnonymous, Route("jcxmbyzcbh")]
        public IHttpActionResult Get_JcxmByZcbh(sys_app_dj_form form)
        {
            try
            {
                var list = _appservice.Get_Jcxm_ByZcbh(form);
                var scxinfo =  list.GroupBy(t => new {t.scx,t.scxmc,t.zjcjg,t.jdqr }).Select(t=>new { t.Key.scx,t.Key.scxmc,t.Key.zjcjg, t.Key.jdqr}).FirstOrDefault();
                return Json(new { code = 1, msg = "ok", list = list, scx = scxinfo.scx, scxmc = scxinfo.scxmc, zjcjg = scxinfo.zjcjg, jdqr = scxinfo.jdqr });
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
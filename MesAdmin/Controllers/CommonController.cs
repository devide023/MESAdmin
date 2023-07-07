using System;
using System.Text.RegularExpressions;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesModels;

namespace MesAdmin.Controllers
{
    [RoutePrefix("api/common")]
    public class CommonController : ApiController
    {
        private IPageConfig _pageconfig;
        public CommonController(IPageConfig pageconfig)
        {
            _pageconfig = pageconfig;
        }
        [HttpGet,Route("scopefuns")]
        public IHttpActionResult GetPageScopeFn(string path)
        {
            try
            {
                string config = _pageconfig.GetPageConf(path);
                Regex reg = new Regex(@"(?<scopefuns>scopefuns[\s\S]*}[\s]*},?)");
                var scopefuns = reg.Match(config).Groups["scopefuns"].Value;
                if (scopefuns.Trim().Length > 0)
                {
                    var pos = scopefuns.LastIndexOf(",");
                    if (pos == scopefuns.Length - 1)
                    {
                        scopefuns = scopefuns.Remove(scopefuns.Length - 1).Replace("scopefuns:", "");
                    }
                    else
                    {
                        scopefuns = scopefuns.Replace("scopefuns:", "");
                    }
                }
                else
                {
                    scopefuns = "{}";
                }
                return Json(new { code = 1, msg = "ok", js = scopefuns });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet, Route("pageconf")]
        public IHttpActionResult GetPageConfig(string path)
        {
            try
            {
                //前端页面配置项
                string config = _pageconfig.GetPageConf(path);
                var token = ZDToolHelper.TokenHelper.GetToken;
                var pagepermis = _pageconfig.GetPagePermis(path, token);
                //页面按钮
                var pagebtns = _pageconfig.GetPageFnList(path, token);
                //批量操作按钮
                var batbtns = _pageconfig.GetPageBatList(path, token);
                return Json(new
                {
                    code = 1,
                    msg = "ok",
                    pageconfig = config,
                    pagepermis = pagepermis,
                    pagebtns = pagebtns,
                    batbtns = batbtns,
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet,Route("pagefields")]
        public IHttpActionResult GetPageFields(int pageid)
        {
            try
            {
                var fields_infos = _pageconfig.GetPageFields(pageid);
                return Json(new
                {
                    code = 1,
                    msg = "ok",
                    fields = fields_infos
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet,Route("routecomponent")]
        public IHttpActionResult GetRountComponent()
        {
            try
            {
                return Json(new { code = 1, msg = "ok", list = _pageconfig.GetRouteComponent() });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost,Route("saveconfig")]
        public IHttpActionResult Save_Page_Config(sys_page_config entity)
        {
            try
            {
                var ret = _pageconfig.Save_Page_Config(entity);
                return Json(new { code = 1, msg = "配置保存成功"});
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
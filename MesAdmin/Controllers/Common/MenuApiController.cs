using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using ZDMesInterfaces.Common;
namespace MesAdmin.Controllers.Common
{
    [RoutePrefix("api/lbj/menuapi")]
    public class MenuApiController : ApiController
    {
        private IMesMenuApi _menuapi;
        public MenuApiController(IMesMenuApi menuapi)
        {
            _menuapi = menuapi;
        }
        [HttpGet,Route("update")]
        public IHttpActionResult Update()
        {
            try
            {
                var path = HttpContext.Current.Server.MapPath("~/Config/");
                DirectoryInfo di = new DirectoryInfo(path);
                var fileinfo = di.GetFiles();
                Regex reg = new Regex("(?<url>.*?url:.*?[\'\"].*?[\'\"])");
                foreach (var item in fileinfo)
                {
                    var jspath = path + item.Name;
                    using (StreamReader sr = new StreamReader(jspath))
                    {
                        List<string> url = new List<string>();
                        var txt = sr.ReadToEnd();
                        var mlist = reg.Matches(txt);
                        for (int i = 0; i < mlist.Count; i++)
                        {
                            url.Add(mlist[i].Value.Replace("url:", "").Replace("'","").Trim());
                        }
                        _menuapi.Update_Mes_Api(new ZDMesModels.sys_menu_api()
                        {
                            filename = item.Name,
                            apis = url
                        });
                    }                    
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MesAdmin.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Content("服务已启动");
        }
    }
}

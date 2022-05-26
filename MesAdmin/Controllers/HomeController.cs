using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;
using System.IO;
using System.Text;
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

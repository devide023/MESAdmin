using MesAdmin;
using MesAdmin.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using Aspose.Cells;
using Newtonsoft.Json;
using System.Collections.Generic;
using ZDMesModels;

namespace MesAdmin.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            try
            {
                var t = ZDToolHelper.Tool.CheckTelNumber("13272712304");
                System.Console.WriteLine(t);
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}

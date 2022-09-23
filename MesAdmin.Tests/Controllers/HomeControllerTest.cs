using MesAdmin;
using MesAdmin.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using Aspose.Cells;
using Newtonsoft.Json;
using System.Collections.Generic;
using ZDMesModels.LBJ;
using ZDMesInterfaces.Common;
using ZDMesServices.Common;
using System;
namespace MesAdmin.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            DateTime d = DateTime.Now;
            decimal a = 10m;
            System.Console.WriteLine("a".GetType().Name);
            System.Console.WriteLine(2.GetType().Name);
            System.Console.WriteLine(2.2d.GetType().Name);
            System.Console.WriteLine(a.GetType().Name);
            System.Console.WriteLine(d.GetType().Name);
        }
    }
}

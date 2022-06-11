using MesAdmin;
using MesAdmin.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using Aspose.Cells;
namespace MesAdmin.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            for (int i = 0; i < 5; i++)
            {
               var token = new ZDToolHelper.JWTHelper().CreateToken();
            }
        }
    }
}

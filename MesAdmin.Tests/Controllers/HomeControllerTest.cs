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
            Workbook wk = new Workbook(@"D:\DeskTop\gwzd.xlsx");
            Cells cells = wk.Worksheets[0].Cells;
            System.Data.DataTable dataTable1 = cells.ExportDataTable(1, 0, cells.MaxDataRow, cells.MaxColumn);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
namespace ZDToolHelper
{
    public  class ExcelHelper
    {
        IWorkbook workbook;
        public ExcelHelper()
        {
            
        }

        public IWorkbook ReadExcel(string filePath)
        {
            
            string fileExt = Path.GetExtension(filePath);
            try
            {

                using (var file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    if (fileExt == ".xls")
                    {
                        workbook = new HSSFWorkbook(file);
                    }
                    else if (fileExt == ".xlsx")
                    {
                        workbook = new XSSFWorkbook(file);
                    }
                    return workbook;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
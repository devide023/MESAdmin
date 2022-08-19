using Aspose.Cells;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ZDMesInterfaces.CDGC;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ.ImportData;
using ZDMesModels;
using ZDMesModels.CDGC;

namespace MesAdmin.Controllers.CDGC.GTJC
{
    /// <summary>
    /// 缸体检测基础数据
    /// </summary>
    [RoutePrefix("api/cdgc/gtjc/jcsj")]
    public class GTJCBaseDataController : BaseApiController<zxjc_base_gtjc>
    {
        private IImportData<zxjc_base_gtjc> _importservice;
        private IDbOperate<zxjc_base_gtjc> _gtbasedataservice;
        private IGtjc _gtjc;
        public GTJCBaseDataController(IDbOperate<zxjc_base_gtjc> gtjcbaseservice, IImportData<zxjc_base_gtjc> importservice, IGtjc gtjc) : base(gtjcbaseservice)
        {
            _gtbasedataservice = gtjcbaseservice;
            _importservice = importservice;
            _gtjc = gtjc;
        }

        [HttpGet, Route("cplx")]
        public IHttpActionResult Get_CpLx_List()
        {
            try
            {
               var list =  _gtjc.Get_CPLX_List();
                return Json(new
                {
                    code = 1,
                    msg = "ok",
                    list = list
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet, Route("get_jcdata_lx")]
        public IHttpActionResult Get_CpLx_List(string lx)
        {
            try
            {
                var list = _gtjc.Get_Gtjc_By_LX(lx).OrderBy(t=>t.seq);
                return Json(new
                {
                    code = 1,
                    msg = "ok",
                    list = list
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet, Route("readxls")]
        public IHttpActionResult ReadTempFile(string fileid)
        {
            string filepath = HttpContext.Current.Server.MapPath($"~/Upload/Excel/{fileid}");
            FileInfo finfo = new FileInfo(filepath);
            try
            {
                List<zxjc_base_gtjc> list = new List<zxjc_base_gtjc>();
                if (!string.IsNullOrEmpty(fileid))
                {
                    list = ReadData(fileid);
                    sys_result msg = new sys_result();

                    var ret = _importservice.NewImportData(list);
                    if (ret.oklist.Count == list.Count)
                    {
                        return Json(new sys_result()
                        {
                            code = 1,
                            msg = $"成功导入数据{list.Count()}条"
                        });
                    }
                    else if (ret.repeatlist.Count > 0)
                    {
                        return Json(new sys_result()
                        {
                            code = 2,
                            msg = $"文件数据{list.Count()}条，重复{ret.repeatlist.Count}条"
                        });
                    }
                    else
                    {
                        return Json(new sys_result()
                        {
                            code = 0,
                            msg = $"数据导入失败"
                        });
                    }
                }
                else
                {
                    return Json(new { code = 0, msg = "读取文件失败,请确认文件是否上传成功" });
                }
            }
            catch (Exception)
            {
                finfo.Delete();
                throw;
            }
        }

        private List<zxjc_base_gtjc> ReadData(string fileid)
        {
            string filepath = HttpContext.Current.Server.MapPath($"~/Upload/Excel/{fileid}");
            FileInfo finfo = new FileInfo(filepath);
            List<zxjc_base_gtjc> list = new List<zxjc_base_gtjc>();
            try
            {
                Workbook wk = new Workbook(filepath);
                Cells cells = wk.Worksheets[0].Cells;
                DataTable dataTable = cells.ExportDataTableAsString(1, 0, cells.MaxDataRow, cells.MaxColumn + 1);
                int i = 1;
                foreach (DataRow item in dataTable.Rows)
                {
                    var vkjlx = string.Empty;
                    var vsdlx = string.Empty;
                    switch (item[7].ToString())
                    {
                        case "单选":
                            vkjlx = "radio";
                            break;
                        case "输入":
                            vkjlx = "text";
                            break;
                        case "不填":
                            vkjlx = "none";
                            break;
                        default:
                            break;
                    }
                    switch (item[8].ToString())
                    {
                        case "单选":
                            vsdlx = "radio";
                            break;
                        case "输入":
                            vsdlx = "text";
                            break;
                        case "不填":
                            vsdlx = "none";
                            break;
                        default:
                            break;
                    }
                    list.Add(new zxjc_base_gtjc()
                    {
                        cplx = item[0].ToString(),
                        th = item[1].ToString(),
                        mh = item[2].ToString(),
                        cpfw = item[3].ToString(),
                        kxmc = item[4].ToString(),
                        kjzsz = item[5].ToString(),
                        sdzsz = item[6].ToString(),
                        kjtype = vkjlx,
                        sdtype = vsdlx,
                        kjcc = item[9].ToString(),
                        kjccsx = item[10].ToString(),
                        kjccxx = item[11].ToString(),
                        sdmj = item[12].ToString(),
                        sdmjsx = item[13].ToString(),
                        sdmjxx = item[14].ToString(),
                        seq = i,
                        lrsj = DateTime.Now,
                    });
                    i++;
                }
                finfo.Delete();
                return list;
            }
            catch (Exception)
            {
                finfo.Delete();
                throw;
            }
        }
    }
}
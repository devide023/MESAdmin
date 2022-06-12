using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesModels.LBJ;
using ZDMesInterfaces;
using ZDMesInterfaces.Common;
using MesAdmin.Filters;
using ZDMesModels;
using System.IO;
using ZDToolHelper;
using System.Web;
using ZDMesInterfaces.LBJ.RyMgr;
using System.Data;
using Aspose.Cells;
using ZDMesInterfaces.LBJ.ImportData;
namespace MesAdmin.Controllers.LBJ.RYGL
{
    [RoutePrefix("api/lbj/ryxx")]
    public class RYXXController : ApiController
    {
        private IDbOperate<zxjc_ryxx> _ryxxservice;
        private IImportData<zxjc_ryxx> _importservice;
        private IRyXx _ryxx;
        private int i = 1;
        public RYXXController(IDbOperate<zxjc_ryxx> ryxxservice,IRyXx ryxx,IImportData<zxjc_ryxx> importservice)
        {
            _ryxxservice = ryxxservice;
            _importservice = importservice;
            _ryxx = ryxx;
        }

        [HttpPost, SearchFilter, Route("list")]
        public IHttpActionResult List(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _ryxxservice.GetList(parm, out resultcount);
                return Json(new sys_search_result()
                {
                    code = 1,
                    msg = "ok",
                    resultcount = resultcount,
                    list = list
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost, Route("add")]
        public IHttpActionResult AddRyxx(List<zxjc_ryxx> entitys)
        {
            try
            {
                int no = 1;
                int uid = _ryxx.MaxUserCode();
                foreach (var item in entitys)
                {
                    item.usercode = CheckUserCode(uid + no);
                    no++;
                }
                int ret = _ryxxservice.Add(entitys);
                if (ret > 0)
                {
                    return Json(new sys_result()
                    {
                        code = 1,
                        msg = "数据保存成功"
                    });
                }
                else
                {
                    return Json(new sys_result()
                    {
                        code = 0,
                        msg = "数据保存失败"
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost, Route("edit")]
        public IHttpActionResult EditRyxx(List<zxjc_ryxx> entitys)
        {
            try
            {
                var ret = _ryxxservice.Modify(entitys);
                if (ret)
                {
                    return Json(new sys_result()
                    {
                        code = 1,
                        msg = "数据修改成功"
                    });
                }
                else
                {
                    return Json(new sys_result()
                    {
                        code = 0,
                        msg = "数据修改失败"
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost, Route("del")]
        public IHttpActionResult DelRyxx(List<zxjc_ryxx> entitys)
        {
            try
            {
                var ret = _ryxxservice.Del(entitys);
                if (ret)
                {
                    return Json(new sys_result()
                    {
                        code = 1,
                        msg = "数据删除成功"
                    });
                }
                else
                {
                    return Json(new sys_result()
                    {
                        code = 0,
                        msg = "数据删除失败"
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private string CheckUserCode(int id)
        {
            try
            {
                var code = id.ToString().PadLeft(6, '0');
                var sfcz = _ryxx.IsExistUserCode(code);
                if (!sfcz)
                {
                    return code;
                }
                else
                {
                    i++;
                    return CheckUserCode(_ryxx.MaxUserCode() + i);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet, Route("readxls_by_zh")]
        public IHttpActionResult ReadTempFile_By_Zh(string fileid)
        {
            string filepath = HttpContext.Current.Server.MapPath($"~/Upload/Excel/{fileid}");
            FileInfo finfo = new FileInfo(filepath);
            try
            {
                List<zxjc_ryxx> list = new List<zxjc_ryxx>();
                if (!string.IsNullOrEmpty(fileid))
                {
                    list = ReadData(fileid);
                    var ret = _importservice.ZhImportData(list);
                    if (ret.oklist.Count == list.Count)
                    {
                        return Json(new sys_result()
                        {
                            code = 1,
                            msg = $"成功导入数据{list.Count()}条"
                        });
                    }
                    else if (ret.dellist.Count > 0)
                    {
                        return Json(new sys_result()
                        {
                            code = 2,
                            msg = $"文件数据{list.Count()}条，替换{ret.dellist.Count}条"
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
        [HttpGet, Route("readxls_by_replace")]
        public IHttpActionResult ReadTempFile_By_Replace(string fileid)
        {
            string filepath = HttpContext.Current.Server.MapPath($"~/Upload/Excel/{fileid}");
            FileInfo finfo = new FileInfo(filepath);
            try
            {
                List<zxjc_ryxx> list = new List<zxjc_ryxx>();
                if (!string.IsNullOrEmpty(fileid))
                {
                    list = ReadData(fileid);
                    var ret = _importservice.ReaplaceImportData(list);
                    if (ret.oklist.Count == list.Count)
                    {
                        return Json(new sys_result()
                        {
                            code = 1,
                            msg = $"成功导入数据{list.Count()}条"
                        });
                    }
                    else if (ret.dellist.Count > 0)
                    {
                        return Json(new sys_result()
                        {
                            code = 2,
                            msg = $"文件数据{list.Count()}条，替换{ret.dellist.Count}条"
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
        [HttpGet,Route("readxls")]
        public IHttpActionResult ReadTempFile(string fileid)
        {
            string filepath = HttpContext.Current.Server.MapPath($"~/Upload/Excel/{fileid}"); 
            FileInfo finfo = new FileInfo(filepath);
            try
            {
                int no = 1;
                int uid = _ryxx.MaxUserCode();
                List<zxjc_ryxx> list = new List<zxjc_ryxx>();
                if (!string.IsNullOrEmpty(fileid))
                {
                    Workbook wk = new Workbook(filepath);
                    Cells cells = wk.Worksheets[0].Cells;
                    DataTable dataTable = cells.ExportDataTableAsString(1, 0, cells.MaxDataRow, cells.MaxColumn+1);
                    finfo.Delete();
                    foreach (DataRow item in dataTable.Rows)
                    {
                        DateTime rsrq = DateTime.Today;
                        DateTime csrq = DateTime.Today.AddYears(-18);
                        DateTime.TryParse(item[11].ToString(), out rsrq);
                        DateTime.TryParse(item[10].ToString(), out csrq);
                        list.Add(new zxjc_ryxx()
                        {
                            gcdm = item[0].ToString(),
                            scx = item[1].ToString(),
                            gwh = item[6].ToString(),
                            usercode = CheckUserCode(uid + no),
                            username = item[3].ToString(),
                            ryxb = item[4].ToString(),
                            password = "123456",
                            rylx = item[5].ToString(),
                            bzxx = item[7].ToString(),
                            hgsg = "Y",
                            rsrq = rsrq,
                            csrq = csrq,
                            jmh = Guid.NewGuid().ToString().Replace("-", "")
                        }); ;
                        no++;
                    }
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

        private List<zxjc_ryxx> ReadData(string fileid)
        {
            string filepath = HttpContext.Current.Server.MapPath($"~/Upload/Excel/{fileid}");
            FileInfo finfo = new FileInfo(filepath);
            List<zxjc_ryxx> list = new List<zxjc_ryxx>();
            try
            {
                Workbook wk = new Workbook(filepath);
                Cells cells = wk.Worksheets[0].Cells;
                DataTable dataTable = cells.ExportDataTableAsString(1, 0, cells.MaxDataRow, cells.MaxColumn + 1);
                foreach (DataRow item in dataTable.Rows)
                {
                    DateTime rsrq = DateTime.Today;
                    DateTime csrq = DateTime.Today.AddYears(-18);
                    DateTime.TryParse(item[11].ToString(), out rsrq);
                    DateTime.TryParse(item[10].ToString(), out csrq);
                    list.Add(new zxjc_ryxx()
                    {
                        gcdm = item[0].ToString(),
                        scx = item[1].ToString(),
                        usercode = item[2].ToString(),
                        username = item[3].ToString(),
                        ryxb = item[4].ToString(),
                        rylx = item[5].ToString(),
                        gwh = item[6].ToString(),
                        bzxx = item[7].ToString(),
                        hgsg = "Y",
                        rsrq = rsrq,
                        csrq = csrq
                    }); 
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
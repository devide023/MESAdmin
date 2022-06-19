using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesModels.LBJ;
using ZDMesInterfaces.Common;
using MesAdmin.Filters;
using ZDMesModels;
using System.IO;
using System.Web;
using Aspose.Cells;
using System.Data;

namespace MesAdmin.Controllers.LBJ.RYGL
{
    [RoutePrefix("api/lbj/jcgl")]
    public class RYJCController : ApiController
    {
        private IDbOperate<zxjc_jcgl> _jcglservice;
        public RYJCController(IDbOperate<zxjc_jcgl> jcglservice)
        {
            _jcglservice = jcglservice;
        }

        [HttpPost, SearchFilter, Route("list")]
        public IHttpActionResult List(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _jcglservice.GetList(parm, out resultcount);
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
        public IHttpActionResult Add(List<zxjc_jcgl> entitys)
        {
            try
            {
                var ret = _jcglservice.Add(entitys);
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
        [HttpPost, Route("del")]
        public IHttpActionResult Del(List<zxjc_jcgl> entitys)
        {
            try
            {
                var ret = _jcglservice.Del(entitys);
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
        [HttpPost, Route("edit")]
        public IHttpActionResult Edit(List<zxjc_jcgl> entitys)
        {
            try
            {
                var ret = _jcglservice.Modify(entitys);
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
        [HttpGet, Route("readxls")]
        public IHttpActionResult ReadTempFile(string fileid)
        {
            string filepath = HttpContext.Current.Server.MapPath($"~/Upload/Excel/{fileid}");
            try
            {
                List<zxjc_jcgl> list = new List<zxjc_jcgl>();
                if (!string.IsNullOrEmpty(fileid))
                {
                    Workbook wk = new Workbook(filepath);
                    Cells cells = wk.Worksheets[0].Cells;
                    DataTable dataTable = cells.ExportDataTableAsString(1, 0, cells.MaxDataRow, cells.MaxColumn + 1);
                    foreach (DataRow item in dataTable.Rows)
                    {
                        list.Add(new zxjc_jcgl()
                        {
                            gcdm = item[0].ToString(),
                            scx = item[1].ToString(),
                            usercode = item[2].ToString(),
                            bzxx = item[3].ToString(),
                            gwh = item[4].ToString(),
                            lx = item[5].ToString(),
                            sl = item[6].ToString(),
                            jcje = item[7].ToString(),
                            jcxx = item[8].ToString(),
                            jcly = item[9].ToString(),
                            fsrq = Convert.ToDateTime(item[10].ToString()),
                            khr = item[11].ToString(),
                            khbm = item[12].ToString(),
                            bz = item[13].ToString(),
                        });
                    }
                    FileInfo finfo = new FileInfo(filepath);
                    finfo.Delete();
                    return Json(new { code = 1, msg = "ok", list = list });
                }
                else
                {
                    return Json(new { code = 0, msg = "读取文件失败,请确认文件是否上传成功" });
                }
            }
            catch (Exception)
            {
                FileInfo finfo = new FileInfo(filepath);
                finfo.Delete();
                throw;
            }
        }
        }
}
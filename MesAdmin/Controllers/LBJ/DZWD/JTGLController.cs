using Aspose.Cells;
using MesAdmin.Filters;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ;
using ZDMesModels;
using ZDMesModels.LBJ;
namespace MesAdmin.Controllers.LBJ.DZWD
{
    [RoutePrefix("api/lbj/jtgl")]
    public class JTGLController : ApiController
    {
        private IDbOperate<zxjc_t_jstc> _jtglservice;
        public JTGLController(IDbOperate<zxjc_t_jstc> jtglservice)
        {
            _jtglservice = jtglservice;
        }

        [HttpPost, SearchFilter, Route("list")]
        public IHttpActionResult GetList(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _jtglservice.GetList(parm, out resultcount);
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
        public IHttpActionResult Add(List<zxjc_t_jstc> entitys)
        {
            try
            {
                entitys.ForEach(i => i.jtid = Guid.NewGuid().ToString());
                int ret = _jtglservice.Add(entitys);
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
        public IHttpActionResult Edit(List<zxjc_t_jstc> entitys)
        {
            try
            {
                var ret = _jtglservice.Modify(entitys);
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
        public IHttpActionResult Del(List<zxjc_t_jstc> entitys)
        {
            try
            {
                var ret = _jtglservice.Del(entitys);
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
        [HttpGet, Route("readxls")]
        public IHttpActionResult ReadTempFile(string fileid)
        {
            string filepath = HttpContext.Current.Server.MapPath($"~/Upload/Excel/{fileid}");
            try
            {
                List<zxjc_t_jstc> list = new List<zxjc_t_jstc>();
                if (!string.IsNullOrEmpty(fileid))
                {
                    Workbook wk = new Workbook(filepath);
                    Cells cells = wk.Worksheets[0].Cells;
                    DataTable dataTable = cells.ExportDataTable(1, 0, cells.MaxDataRow, cells.MaxColumn + 1);
                    foreach (DataRow item in dataTable.Rows)
                    {
                        list.Add(new zxjc_t_jstc()
                        {
                            jtid = Guid.NewGuid().ToString(),
                            gcdm = item[0].ToString(),
                            scx = item[1].ToString(),
                            jcbh = item[2].ToString(),
                            wjfl = item[3].ToString(),
                            jcmc = item[4].ToString(),
                            jcms = item[5].ToString(),
                            yxqx1 = Convert.ToDateTime(item[6].ToString()),
                            yxqx2 = Convert.ToDateTime(item[7].ToString()),
                        });
                    }
                    var ret = _jtglservice.Add(list);
                    if (ret > 0)
                    {
                        FileInfo finfo = new FileInfo(filepath);
                        finfo.Delete();
                        return Json(new sys_result()
                        {
                            code = 1,
                            msg = "数据导入成功"
                        });
                    }
                    else
                    {
                        return Json(new sys_result()
                        {
                            code = 0,
                            msg = "数据导入失败"
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
                FileInfo finfo = new FileInfo(filepath);
                finfo.Delete();
                throw;
            }
        }
    }
}
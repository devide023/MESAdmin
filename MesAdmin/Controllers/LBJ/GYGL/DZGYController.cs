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
using ZDMesModels;
using ZDMesModels.LBJ;
namespace MesAdmin.Controllers.LBJ.GYGL
{
    [RoutePrefix("api/lbj/dzgy")]
    public class DZGYController : ApiController
    {
        private IDbOperate<zxjc_t_dzgy> _dzgyservice;
        public DZGYController(IDbOperate<zxjc_t_dzgy> dzgyservice)
        {
            _dzgyservice = dzgyservice;
        }

        [HttpPost, SearchFilter, Route("list")]
        public IHttpActionResult GetList(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                parm.default_order_colname = "scx asc,scsj ";
                var list = _dzgyservice.GetList(parm, out resultcount);
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
        public IHttpActionResult Add(List<zxjc_t_dzgy> entitys)
        {
            try
            {
                var q = entitys.Where(t => string.IsNullOrEmpty(t.statusno) || string.IsNullOrEmpty(t.scx) || string.IsNullOrEmpty(t.gybh));
                if (q.Count() > 0)
                {
                    return Json(new sys_result()
                    {
                        code = 0,
                        msg = "生产线、工艺编号、产品不能为空"
                    });
                }
                else
                {
                    entitys.ForEach(i => i.gyid = Guid.NewGuid().ToString());
                    int ret = _dzgyservice.Add(entitys);
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
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost, Route("edit")]
        public IHttpActionResult EditRyxx(List<zxjc_t_dzgy> entitys)
        {
            try
            {
                var q = entitys.Where(t => string.IsNullOrEmpty(t.statusno) || string.IsNullOrEmpty(t.scx) || string.IsNullOrEmpty(t.gybh));
                if (q.Count() > 0)
                {
                    return Json(new sys_result()
                    {
                        code = 0,
                        msg = "生产线、工艺编号、产品不能为空"
                    });
                }
                else
                {
                    var ret = _dzgyservice.Modify(entitys);
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
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost, Route("del")]
        public IHttpActionResult DelRyxx(List<zxjc_t_dzgy> entitys)
        {
            try
            {
                var ret = _dzgyservice.Del(entitys);
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
                List<zxjc_t_dzgy> list = new List<zxjc_t_dzgy>();
                if (!string.IsNullOrEmpty(fileid))
                {
                    Workbook wk = new Workbook(filepath);
                    Cells cells = wk.Worksheets[0].Cells;
                    DataTable dataTable = cells.ExportDataTableAsString(1, 0, cells.MaxDataRow, cells.MaxColumn + 1);
                    foreach (DataRow item in dataTable.Rows)
                    {
                        list.Add(new zxjc_t_dzgy()
                        {
                            gyid = Guid.NewGuid().ToString(),
                            gcdm = item[0].ToString(),
                            scx = item[1].ToString(),
                            gybh = item[2].ToString(),
                            statusno = item[3].ToString(),
                            gymc = item[4].ToString(),
                            gyms = item[5].ToString(),
                            wjfl = item[6].ToString(),
                        });
                    }
                    var ret = _dzgyservice.Add(list);
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
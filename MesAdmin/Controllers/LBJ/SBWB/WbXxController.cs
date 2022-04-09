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
namespace MesAdmin.Controllers.LBJ.SBWB
{
    [RoutePrefix("api/lbj/wbxx")]
    public class WbXxController : ApiController
    {
        private IDbOperate<base_sbwb> _sbwbservice;
        private IUser _user;
        public WbXxController(IDbOperate<base_sbwb> sbwbservice, IUser user)
        {
            _sbwbservice = sbwbservice;
            _user = user;
        }
        [HttpPost, SearchFilter, Route("list")]
        public IHttpActionResult GetList(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _sbwbservice.GetList(parm, out resultcount);
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
        public IHttpActionResult Add(List<base_sbwb> entitys)
        {
            try
            {
                int ret = _sbwbservice.Add(entitys);
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
        public IHttpActionResult Edit(List<base_sbwb> entitys)
        {
            try
            {
                var ret = _sbwbservice.Modify(entitys);
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
        public IHttpActionResult Del(List<base_sbwb> entitys)
        {
            try
            {
                var ret = _sbwbservice.Del(entitys);
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
        [HttpGet,Route("readxls")]
        public IHttpActionResult ReadTempFile(string fileid)
        {
            string filepath = HttpContext.Current.Server.MapPath($"~/Upload/Excel/{fileid}");
            try
            {
                List<base_sbwb> list = new List<base_sbwb>();
                if (!string.IsNullOrEmpty(fileid))
                {
                    string token = ZDToolHelper.TokenHelper.GetToken;
                    var userinfo = _user.GetUserByToken(token);
                    Workbook wk = new Workbook(filepath);
                    Cells cells = wk.Worksheets[0].Cells;
                    DataTable dataTable = cells.ExportDataTable(1, 0, cells.MaxDataRow, cells.MaxColumn + 1);
                    foreach (DataRow item in dataTable.Rows)
                    {
                        list.Add(new base_sbwb()
                        {
                            autoid = Guid.NewGuid().ToString(),
                            gcdm = item[0].ToString(),
                            scx = item[1].ToString(),
                            gwh = item[2].ToString(),
                            wbsh = Convert.ToInt32( item[3].ToString()),
                            wbxx = item[4].ToString(),
                            bz=item[5].ToString(),
                            scbz="N",
                            lrr = userinfo.name,
                            lrsj = DateTime.Now,
                        });
                    }
                    var ret = _sbwbservice.Add(list);
                    FileInfo finfo = new FileInfo(filepath);
                    finfo.Delete();
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
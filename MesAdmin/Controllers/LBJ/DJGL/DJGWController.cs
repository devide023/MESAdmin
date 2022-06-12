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
using System.Web;
using Aspose.Cells;
using System.IO;
using System.Data;
using ZDMesInterfaces.LBJ.DJGW;
using ZDMesInterfaces.LBJ.ImportData;
namespace MesAdmin.Controllers.LBJ.DJGL
{
    [RoutePrefix("api/lbj/djgw")]
    public class DJGWController : ApiController
    {
        private IDbOperate<zxjc_djgw> _djgwservice;
        IImportData<zxjc_djgw> _importservice;
        private IUser _user;
        private IDjGw _djgw;
        private int i = 1;
        public DJGWController(IDbOperate<zxjc_djgw> djgwservice, IUser user, IDjGw djgw,IImportData<zxjc_djgw> importservice)
        {
            _djgwservice = djgwservice;
            _importservice = importservice;
            _user = user;
            _djgw = djgw;
        }
        [HttpPost, SearchFilter, Route("list")]
        public IHttpActionResult GetList(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _djgwservice.GetList(parm, out resultcount);
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
        public IHttpActionResult Add(List<zxjc_djgw> entitys)
        {
            try
            {
                int ret = _djgwservice.Add(entitys);
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
        public IHttpActionResult Edit(List<zxjc_djgw> entitys)
        {
            try
            {
                var ret = _djgwservice.Modify(entitys);
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
        public IHttpActionResult Del(List<zxjc_djgw> entitys)
        {
            try
            {
                var ret = _djgwservice.Del(entitys);
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
        [HttpGet, Route("readxls_by_replace")]
        public IHttpActionResult ReadTempFile_Replace(string fileid) {
            try
            {
                List<zxjc_djgw> list = new List<zxjc_djgw>();
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
                            msg = $"文件数据{list.Count()}条，导入{ret.oklist.Count}条,替换{ret.dellist.Count}条"
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

                throw;
            }
        }
        [HttpGet, Route("readxls_by_zh")]
        public IHttpActionResult ReadTempFile_Zh(string fileid) {
            try
            {
                List<zxjc_djgw> list = new List<zxjc_djgw>();
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
                    else if (ret.orginallist.Count > 0)
                    {
                        return Json(new sys_result()
                        {
                            code = 2,
                            msg = $"文件数据{list.Count()}条，导入{ret.oklist.Count}条,更新{ret.orginallist.Count}条"
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

                throw;
            }
        }
        [HttpGet, Route("readxls")]
        public IHttpActionResult ReadTempFile(string fileid)
        {
            string filepath = HttpContext.Current.Server.MapPath($"~/Upload/Excel/{fileid}");
            FileInfo finfo = new FileInfo(filepath);
            int index = 1;
            try
            {
                List<zxjc_djgw> list = new List<zxjc_djgw>();
                if (!string.IsNullOrEmpty(fileid))
                {
                    Workbook wk = new Workbook(filepath);
                    Cells cells = wk.Worksheets[0].Cells;
                    DataTable dataTable = cells.ExportDataTable(1, 0, cells.MaxDataRow, cells.MaxColumn + 1);
                    string token = ZDToolHelper.TokenHelper.GetToken;
                    int maxno = _djgw.MaxDjNo();
                    foreach (DataRow item in dataTable.Rows)
                    {
                        list.Add(new zxjc_djgw()
                        {
                            gcdm = item[0].ToString(),
                            scx  = item[1].ToString(),
                            gwh = item[2].ToString(),
                            statusno = item[3].ToString(),
                            djxx = item[4].ToString(),
                            scbz="N",
                            djno = CheckDjNo(maxno + index),
                            lrr = _user.GetUserByToken(token).name,
                            lrsj = DateTime.Now
                        });
                        index++;
                    }
                    var ret = _importservice.NewImportData(list);
                    finfo.Delete();
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
                            msg = $"文件数据{list.Count()}条，导入{ret.oklist.Count}条,重复{ret.repeatlist.Count}条"
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

        private string CheckDjNo(int no)
        {
            try
            {
                string djno = "DJ" + no.ToString().PadLeft(4, '0');
                if (!_djgw.IsExistDjNo(djno))
                {
                    return djno;
                }
                else
                {
                    var res = CheckDjNo(no + i);
                    i++;
                    return res;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private List<zxjc_djgw> ReadData(string fileid)
        {
            string filepath = HttpContext.Current.Server.MapPath($"~/Upload/Excel/{fileid}");
            FileInfo finfo = new FileInfo(filepath);
            try
            {
                List<zxjc_djgw> list = new List<zxjc_djgw>();
                if (!string.IsNullOrEmpty(fileid))
                {
                    Workbook wk = new Workbook(filepath);
                    Cells cells = wk.Worksheets[0].Cells;
                    DataTable dataTable = cells.ExportDataTable(1, 0, cells.MaxDataRow, cells.MaxColumn + 1);
                    foreach (DataRow item in dataTable.Rows)
                    {
                        list.Add(new zxjc_djgw()
                        {
                            gcdm = item[0].ToString(),
                            scx = item[1].ToString(),
                            gwh = item[2].ToString(),
                            statusno = item[3].ToString(),
                            djxx = item[4].ToString(),
                            djno = item[4].ToString()
                        });
                    }
                }
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
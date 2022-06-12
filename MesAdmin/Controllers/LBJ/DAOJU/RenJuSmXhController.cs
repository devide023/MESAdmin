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
using ZDMesInterfaces.LBJ.ImportData;
using ZDMesModels;
using ZDMesModels.LBJ;

namespace MesAdmin.Controllers.LBJ.DAOJU
{
    [RoutePrefix("api/lbj/rjsmxh")]
    public class RenJuSmXhController : ApiController
    {
        private IDbOperate<base_rjsmxh> _rjsmxhservice;
        private IImportData<base_rjsmxh> _importservice;
        public RenJuSmXhController(IDbOperate<base_rjsmxh> rjsmxhservice, IImportData<base_rjsmxh> importservice)
        {
            _rjsmxhservice = rjsmxhservice;
            _importservice = importservice;
        }
        [HttpPost, SearchFilter, Route("list")]
        public IHttpActionResult GetList(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _rjsmxhservice.GetList(parm, out resultcount);
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
        public IHttpActionResult Add(List<base_rjsmxh> entitys)
        {
            try
            {
                int ret = _rjsmxhservice.Add(entitys);
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
        public IHttpActionResult Edit(List<base_rjsmxh> entitys)
        {
            try
            {
                var ret = _rjsmxhservice.Modify(entitys);
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
        public IHttpActionResult Del(List<base_rjsmxh> entitys)
        {
            try
            {
                var ret = _rjsmxhservice.Del(entitys);
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
        public IHttpActionResult ReadTempFile_By_Replace(string fileid)
        {
            try
            {
                List<base_rjsmxh> list = new List<base_rjsmxh>();
                if (!string.IsNullOrEmpty(fileid))
                {
                    list = Read_Data(fileid);
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
        public IHttpActionResult ReadTempFile_By_Zh(string fileid)
        {
            try
            {
                List<base_rjsmxh> list = new List<base_rjsmxh>();
                if (!string.IsNullOrEmpty(fileid))
                {
                    list = Read_Data(fileid);
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
            try
            {
                List<base_rjsmxh> list = new List<base_rjsmxh>();
                if (!string.IsNullOrEmpty(fileid))
                {
                    list = Read_Data(fileid);
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
                
                throw;
            }
        }

        private List<base_rjsmxh> Read_Data(string fileid)
        {
            string filepath = HttpContext.Current.Server.MapPath($"~/Upload/Excel/{fileid}");
            FileInfo finfo = new FileInfo(filepath);
            try
            {
                List<base_rjsmxh> list = new List<base_rjsmxh>();
                if (!string.IsNullOrEmpty(fileid))
                {
                    Workbook wk = new Workbook(filepath);
                    Cells cells = wk.Worksheets[0].Cells;
                    DataTable dataTable = cells.ExportDataTableAsString(1, 0, cells.MaxDataRow, cells.MaxColumn + 1);
                    string token = ZDToolHelper.TokenHelper.GetToken;
                    foreach (DataRow item in dataTable.Rows)
                    {
                        list.Add(new base_rjsmxh()
                        {
                            gcdm = item[0].ToString(),
                            scx = item[1].ToString(),
                            rjlx = item[2].ToString(),
                            cpzt = item[3].ToString(),
                            sbbh = item[4].ToString(),
                            mjxhsm = Convert.ToInt32(item[5].ToString())

                        });
                    }
                    finfo.Delete();
                }
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
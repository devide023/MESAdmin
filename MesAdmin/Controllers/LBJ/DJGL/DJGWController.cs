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
using ZDMesInterfaces.LBJ;

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
        private IDbSeq _seqservice;
        private IBaseInfo _baseinfo;
        public DJGWController(IDbOperate<zxjc_djgw> djgwservice, IUser user, IDjGw djgw,IImportData<zxjc_djgw> importservice, IDbSeq seqservice, IBaseInfo baseinfo)
        {
            _djgwservice = djgwservice;
            _importservice = importservice;
            _user = user;
            _djgw = djgw;
            _seqservice = seqservice;
            _baseinfo = baseinfo;
        }
        [HttpPost, SearchFilter, Route("list")]
        public IHttpActionResult GetList(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var gwzdlist = _baseinfo.GetGwZd();
                var scxzxlist = _baseinfo.Get_ALL_ScxXX_JJ();
                var list = _djgwservice.GetList(parm, out resultcount);
                foreach (var item in list)
                {
                    var options = new List<sys_column_options>();
                    var l = gwzdlist.Where(t => t.scx == item.scx);
                    var scxzxs = scxzxlist.Where(t=>t.scx == item.scx).Select(t=>new option_list() {label=t.scxzxmc,value=t.scxzx}).OrderBy(t=>t.value);
                    foreach (var o in l)
                    {
                        var q = options.Where(t => t.value == o.gwh);
                        if (q.Count() == 0)
                        {
                            options.Add(new sys_column_options { label = o.gwmc, value = o.gwh });
                        }
                    }
                    item.gwhoptions = options;
                    item.scxzxs = scxzxs.ToList();
                }
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
                var q = entitys.Where(t => string.IsNullOrEmpty(t.scx) || string.IsNullOrEmpty(t.gwh) || string.IsNullOrEmpty(t.djxx));
                if (q.Count() > 0)
                {
                    return Json(new sys_result()
                    {
                        code = 0,
                        msg = "生产线、岗位、点检内容不能为空"
                    });
                }
                else
                {
                foreach (var item in entitys)
                {
                    var djno = _seqservice.Get_Seq_Number("SEQ_MES_DJNO");
                    item.djno = "DJ"+ djno.ToString().PadLeft(4, '0');
                }
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
                var q = entitys.Where(t => string.IsNullOrEmpty(t.scx) || string.IsNullOrEmpty(t.gwh) || string.IsNullOrEmpty(t.djxx));
                if (q.Count() > 0)
                {
                    return Json(new sys_result()
                    {
                        code = 0,
                        msg = "生产线、岗位、点检内容不能为空"
                    });
                }
                else
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
                    DataTable dataTable = cells.ExportDataTableAsString(1, 0, cells.MaxDataRow, cells.MaxColumn + 1);
                    string token = ZDToolHelper.TokenHelper.GetToken;
                    int maxno = _djgw.MaxDjNo();
                    foreach (DataRow item in dataTable.Rows)
                    {
                        var djno = _seqservice.Get_Seq_Number("SEQ_MES_DJNO");
                        list.Add(new zxjc_djgw()
                        {
                            gcdm = item[0].ToString(),
                            scx = item[1].ToString(),
                            scxzx= item[2].ToString(),
                            gwh = item[3].ToString(),
                            statusno = item[4].ToString(),
                            djxx = item[5].ToString(),
                            scbz = "N",
                            djno = "DJ" + djno.ToString().PadLeft(4, '0'),
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
                    DataTable dataTable = cells.ExportDataTableAsString(1, 0, cells.MaxDataRow, cells.MaxColumn + 1);
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
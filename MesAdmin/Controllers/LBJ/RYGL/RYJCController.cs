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
using ZDMesInterfaces.LBJ.ImportData;
using ZDMesInterfaces.LBJ;

namespace MesAdmin.Controllers.LBJ.RYGL
{
    [RoutePrefix("api/lbj/jcgl")]
    public class RYJCController : ApiController
    {
        private IDbOperate<zxjc_jcgl> _jcglservice;
        private IImportData<zxjc_jcgl> _importservice;
        private ICheckData _cks;
        private IUser _userservice;
        private IBaseInfo _baseinfo;
        public RYJCController(IDbOperate<zxjc_jcgl> jcglservice, IImportData<zxjc_jcgl> importservice, ICheckData cks, IUser userservice, IBaseInfo baseinfo)
        {
            _jcglservice = jcglservice;
            _importservice = importservice;
            _cks = cks;
            _userservice = userservice;
            _baseinfo = baseinfo;
        }

        [HttpPost, SearchFilter, Route("list")]
        public IHttpActionResult List(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var gwzdlist = _baseinfo.GetGwZd();
                var list = _jcglservice.GetList(parm, out resultcount);
                foreach (var item in list)
                {
                    var options = new List<sys_column_options>();
                    var l = gwzdlist.Where(t => t.scx == item.scx);
                    foreach (var o in l)
                    {
                        var q = options.Where(t => t.value == o.gwh);
                        if (q.Count() == 0)
                        {
                            options.Add(new sys_column_options { label = o.gwmc, value = o.gwh });
                        }
                    }
                    item.gwhoptions = options;
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
        public IHttpActionResult Add(List<zxjc_jcgl> entitys)
        {
            try
            {
                var checkquery = entitys.Where(t => string.IsNullOrEmpty(t.usercode) || string.IsNullOrEmpty(t.gwh) || string.IsNullOrEmpty(t.jcxx));
                if (checkquery.Count() > 0)
                {
                    return Json(new sys_result()
                    {
                        code = 0,
                        msg = "账号、岗位、明细字段不能为空"
                    });
                }
                else
                {
                    var q = entitys.Where(t => !_cks.Valid<zxjc_jcgl>("user_code", t.usercode)).ToList();
                    if (q.Count > 0)
                    {
                        string codes = string.Empty;
                        q.ForEach(t => codes = codes + t.usercode + ",");
                        return Json(new sys_result()
                        {
                            code = 0,
                            msg = $"账号{codes}非法请检查"
                        });
                    }
                    else
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
                var checkquery = entitys.Where(t => string.IsNullOrEmpty(t.usercode) || string.IsNullOrEmpty(t.gwh) || string.IsNullOrEmpty(t.jcxx));
                if (checkquery.Count() > 0)
                {
                    return Json(new sys_result()
                    {
                        code = 0,
                        msg = "账号、岗位、明细字段不能为空"
                    });
                }
                else
                {
                    var q = entitys.Where(t => !_cks.Valid<zxjc_jcgl>("user_code", t.usercode)).ToList();
                    if (q.Count > 0)
                    {
                        string codes = string.Empty;
                        q.ForEach(t => codes = codes + t.usercode + ",");
                        return Json(new sys_result()
                        {
                            code = 0,
                            msg = $"账号{codes}非法请检查"
                        });
                    }
                    else
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
                    var token = ZDToolHelper.TokenHelper.GetToken;
                    var uifno = _userservice.GetUserByToken(token);
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
                            lrr = uifno.name,
                            lrsj=DateTime.Now
                        });
                    }
                    FileInfo finfo = new FileInfo(filepath);
                    finfo.Delete();
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
                FileInfo finfo = new FileInfo(filepath);
                finfo.Delete();
                throw;
            }
        }
        }
}
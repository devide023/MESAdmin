﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesModels.LBJ;
using ZDMesInterfaces.Common;
using MesAdmin.Filters;
using ZDMesModels;
using ZDMesInterfaces.LBJ.RyMgr;
using System.IO;
using System.Web;
using Aspose.Cells;
using System.Data;
using ZDToolHelper;
using ZDMesInterfaces.LBJ.ImportData;
using ZDMesInterfaces.LBJ;

namespace MesAdmin.Controllers.LBJ.RYGL
{
    [RoutePrefix("api/lbj/ryjn")]
    public class RYJNController : ApiController
    {
        private int i = 0;
        private IDbOperate<zxjc_ryxx_jn> _ryjnservice;
        private IImportData<zxjc_ryxx_jn> _importservice;
        private IBaseInfo _baseinfo;
        private IRyJn _ryjn;
        private IDbSeq _seqservice;
        private IUser _user;
        public RYJNController(IDbOperate<zxjc_ryxx_jn> ryjnservice, IRyJn ryjn,IImportData<zxjc_ryxx_jn> importservice, IDbSeq seqservice,IUser user,IBaseInfo baseinfo)
        {
            _ryjnservice = ryjnservice;
            _importservice = importservice;
            _ryjn = ryjn;
            _seqservice = seqservice;
            _user = user;
            _baseinfo = baseinfo;
        }

        [HttpPost, SearchFilter, Route("list")]
        public IHttpActionResult List(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var gwzdlist = _baseinfo.GetGwZd();
                var ryxxlist = _baseinfo.RyxxList();
                var list = _ryjnservice.GetList(parm, out resultcount);
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
                    var useroptions = new List<sys_column_options>();
                    var ryq = ryxxlist.Where(t => t.scx == item.scx);
                    foreach (var o in ryq)
                    {
                        useroptions.Add(new sys_column_options { label = o.username, value = o.usercode });
                    }
                    item.useroptions = useroptions;
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
        private string CheckJnNo(int maxno)
        {
            try
            {
                var jnno = "JN" + maxno.ToString().PadLeft(4, '0');
                var sfcz = _ryjn.IsExistJnNo(jnno);
                if (!sfcz) {
                    return jnno;
                }
                else
                {
                    i++;
                    return CheckJnNo(_ryjn.MaxJnNo() + i);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost, Route("add")]
        public IHttpActionResult Add(List<zxjc_ryxx_jn> entitys)
        {
            try
            {
                var checkquery = entitys.Where(t => string.IsNullOrEmpty(t.usercode) || string.IsNullOrEmpty(t.gwh));
                if (checkquery.Count() > 0)
                {
                    return Json(new sys_result()
                    {
                        code = 0,
                        msg = "账号、岗位信息不能为空"
                    }) ;
                }
                else
                {
                    foreach (var item in entitys)
                    {
                        var jnbh = _seqservice.Get_Seq_Number("seq_mes_jnbh");
                        item.jnbh = "JN" + jnbh.ToString().PadLeft(4, '0');
                    }
                    var ret = _ryjnservice.Add(entitys);
                    if (ret == entitys.Count)
                    {
                        return Json(new sys_result()
                        {
                            code = 1,
                            msg = "数据保存成功"
                        });
                    }
                    else if (ret < entitys.Count && ret != 0)
                    {
                        return Json(new sys_result()
                        {
                            code = 0,
                            msg = "部分数据保存成功"
                        });
                    }
                    else
                    {
                        return Json(new sys_result()
                        {
                            code = 0,
                            msg = "数据保存失败,账号信息不存在"
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
        public IHttpActionResult Del(List<zxjc_ryxx_jn> entitys)
        {
            try
            {
                var ret = _ryjnservice.Del(entitys);
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
        public IHttpActionResult Edit(List<zxjc_ryxx_jn> entitys)
        {
            try
            {
                var checkquery = entitys.Where(t => string.IsNullOrEmpty(t.usercode) || string.IsNullOrEmpty(t.gwh));
                if (checkquery.Count() > 0)
                {
                    return Json(new sys_result()
                    {
                        code = 0,
                        msg = "账号、岗位信息不能为空"
                    });
                }
                else
                {
                    var ret = _ryjnservice.Modify(entitys);
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
        [HttpGet, Route("readxls_by_replace")]
        public IHttpActionResult ReadTempFile_Replace(string fileid)
        {
            try
            {
                if (!string.IsNullOrEmpty(fileid))
                {
                    var list = ReadData(fileid);
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
        public IHttpActionResult ReadTempFile_ZH(string fileid)
        {
            try
            {
                if (!string.IsNullOrEmpty(fileid))
                {
                    var list = ReadData(fileid);
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
            try
            {
                List<zxjc_ryxx_jn> list = new List<zxjc_ryxx_jn>();
                if (!string.IsNullOrEmpty(fileid))
                {
                    var token = TokenHelper.GetToken;
                    var userinfo = _user.GetUserByToken(token);
                    Workbook wk = new Workbook(filepath);
                    Cells cells = wk.Worksheets[0].Cells;
                    DataTable dataTable = cells.ExportDataTableAsString(1, 0, cells.MaxDataRow, cells.MaxColumn + 1);
                    int maxno = _ryjn.MaxJnNo();
                    int no = 1;
                    foreach (DataRow item in dataTable.Rows)
                    {
                        list.Add(new zxjc_ryxx_jn()
                        {
                            jnbh = CheckJnNo(maxno + no),
                            gcdm = item[0].ToString(),
                            scx = item[1].ToString(),
                            usercode = item[2].ToString(),
                            gwh = item[3].ToString(),
                            jnfl = item[4].ToString(),
                            jnsj = Convert.ToDateTime(item[5].ToString()),
                            jnsld = Convert.ToInt32(item[6].ToString()),
                            jnxx = item[7].ToString(),
                            sfhg = "Y",
                            lrr = userinfo.name,
                            lrsj=DateTime.Now
                        });
                        no++;
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
        private List<zxjc_ryxx_jn> ReadData(string fileid)
        {
            string filepath = HttpContext.Current.Server.MapPath($"~/Upload/Excel/{fileid}");
            FileInfo finfo = new FileInfo(filepath);
            try
            {
                List<zxjc_ryxx_jn> list = new List<zxjc_ryxx_jn>();
                if (!string.IsNullOrEmpty(fileid))
                {
                    var token = TokenHelper.GetToken;
                    var userinfo = _user.GetUserByToken(token);
                    Workbook wk = new Workbook(filepath);
                    Cells cells = wk.Worksheets[0].Cells;
                    DataTable dataTable = cells.ExportDataTableAsString(1, 0, cells.MaxDataRow, cells.MaxColumn + 1);
                    foreach (DataRow item in dataTable.Rows)
                    {
                        list.Add(new zxjc_ryxx_jn()
                        {
                            gcdm = item[0].ToString(),
                            scx = item[1].ToString(),
                            usercode = item[2].ToString(),
                            gwh = item[3].ToString(),
                            jnfl = item[4].ToString(),
                            jnsj = Convert.ToDateTime(item[5].ToString()),
                            jnsld = Convert.ToInt32(item[6].ToString()),
                            jnxx = item[7].ToString(),
                            lrr = userinfo.name,
                            lrsj = DateTime.Now
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
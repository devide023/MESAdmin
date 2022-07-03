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
using ZDMesInterfaces.LBJ.DaoJu;
using System.Web;
using Aspose.Cells;
using System.IO;
using System.Data;
using ZDMesInterfaces.LBJ.ImportData;

namespace MesAdmin.Controllers.LBJ.DAOJU
{
    [RoutePrefix("api/lbj/dbrjly")]
    public class DbRjLyController : ApiController
    {
        private IDbOperate<base_dbrjzx> _dbrjzxservice;
        private IDaoJu _gxservice;
        private IImportData<base_dbrjzx> _impservice;
        public DbRjLyController(IDbOperate<base_dbrjzx> dbrjzxservice, IDaoJu gxservice, IImportData<base_dbrjzx> impservice)
        {
            _dbrjzxservice = dbrjzxservice;
            _gxservice = gxservice;
            _impservice = impservice;
        }

        [HttpPost, SearchFilter, Route("list")]
        public IHttpActionResult GetList(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _dbrjzxservice.GetList(parm, out resultcount);
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
            finally
            {
            }
        }
        [HttpPost,Route("search_dbrjzx")]
        public IHttpActionResult Search_DbRjZx(sys_dbrjzx_form form)
        {
            try
            {
                var list = _gxservice.Search_DbRjZx(form);
                return Json(new sys_search_result()
                {
                    code = 1,
                    msg = "ok",
                    list = list
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost,Route("dbrjgx")]
        public IHttpActionResult Get_DbrjgxList(List<string> dbh)
        {
            try
            {
                var list = _gxservice.DbRjGxList(dbh);
                return Json(new { code = 1, msg = "ok", list = list });
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 刀柄以旧换新，以坏换新
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        [HttpPost,Route("old2new")]
        public IHttpActionResult OldToNew(List<base_dbrjzx> entitys)
        {
            try
            {
               var ret = _gxservice.OldToNew(entitys);
                if (ret)
                {
                    return Json(new sys_result()
                    {
                        code = 1,
                        msg = "操作成功"
                    });
                }
                else
                {
                    return Json(new sys_result()
                    {
                        code = 0,
                        msg = "操作失败"
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 刀柄刃具首次领用
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost,Route("scly")]
        public IHttpActionResult First_Use(dbrjlyform form)
        {
            try
            {
               var ret = _gxservice.DaoBinRenJuLy(form);
                if (ret)
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
        
        [HttpPost,Route("zxrjrm")]
        public IHttpActionResult SetZxRjRm(List<int> id)
        {
            try
            {
                var ret = _gxservice.SetRjSm(id);
                return Json(new
                {
                    code = 1,
                    msg = "数据操作成功"
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost,Route("rjlx_by_dbh")]
        public IHttpActionResult Get_RjLx_By(List<string> dbh)
        {
            try
            {
                var list = _gxservice.GetRjxxByDbBh(dbh);
                return Json(new { code = 1, msg = "ok", list = list });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost,Route("uninstall")]
        public IHttpActionResult UnInstall(List<int> id)
        {
            try
            {
               var ret = _gxservice.UnInstallRjXx(id);
                if (ret)
                {
                    return Json(new { code = 1, msg = "卸载成功"});
                }
                else
                {
                    return Json(new { code = 0, msg = "卸载失败" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet, Route("zxbydbh")]
        public IHttpActionResult GetRjZxByDbh(string dbh,string scx,string sbbh)
        {
            try
            {
                var list = _gxservice.GetRjZxByDbh(dbh,scx,sbbh);
                var rlist = list.Select(t => new { t.dbh, t.sbbh, t.scx }).Distinct().OrderBy(t=>t.scx).ThenBy(t=>t.sbbh).ThenBy(t=>t.dbh);
                return Json(new { code = 1, msg = "ok", list = rlist });
            }
            catch (Exception)
            {

                throw;
            }
        }
        //刃具使用情况，选择刃具更换
        [HttpGet, Route("rjgh")]
        public IHttpActionResult Choose_RjList(string dbh)
        {
            try
            {
                var list = _gxservice.ChooseRjlxByDbh(dbh);
                var scx = list.Select(t => t.scx).FirstOrDefault();
                var sbbh = list.Select(t=>t.sbbh).FirstOrDefault();
                return Json(new { code = 1, msg = "ok", list = list,scx = scx,sbbh = sbbh });
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 在线刃具安装
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [HttpPost,Route("zxrjaz")]
        public IHttpActionResult ZxRjInstall(List<base_dbrjzx> list)
        {
            try
            {
                var ret = _gxservice.ZxRjInstall(list);
                if (ret)
                {
                    return Json(new { code = 1, msg = "ok" });
                }
                else
                {
                    return Json(new { code = 1, msg = "数据保存失败" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 在线刃具变更
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        [HttpPost, Route("zxrjbg")]
        public IHttpActionResult ZxRjChange(List<base_dbrjzx> list)
        {
            try
            {
                var ret = _gxservice.ZxRjChange(list);
                if (ret)
                {
                    return Json(new { code = 1, msg = "ok" });
                }
                else
                {
                    return Json(new { code = 1, msg = "数据保存失败" });
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                
            }
        }
        [HttpPost, Route("save_dbrjzx")]
        public IHttpActionResult Save_DbRjZx_Change(sys_dbrj_bgly_form form) 
        {
            try
            {
                var ret = _gxservice.Save_DbRjZx_Change(form);
                if (ret)
                {
                    return Json(new { code = 1, msg = "ok" });
                }
                else
                {
                    return Json(new { code = 1, msg = "数据保存失败" });
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
            FileInfo finfo = new FileInfo(filepath);
            try
            {
                List<base_dbrjzx> list = new List<base_dbrjzx>();
                if (!string.IsNullOrEmpty(fileid))
                {
                    Workbook wk = new Workbook(filepath);
                    Cells cells = wk.Worksheets[0].Cells;
                    DataTable dataTable = cells.ExportDataTableAsString(1, 0, cells.MaxDataRow, cells.MaxColumn + 1);
                    string token = ZDToolHelper.TokenHelper.GetToken;
                    foreach (DataRow item in dataTable.Rows)
                    {
                        list.Add(new base_dbrjzx()
                        {
                            gcdm = item[0].ToString(),
                            scx = item[1].ToString(),
                            sbbh = item[2].ToString(),
                            dbh = item[3].ToString(),
                            rjlx=item[4].ToString(),
                            dblyr = item[5].ToString(),
                            rjlyr = item[5].ToString(),
                            rjrmcs = 0,
                            dblysj = DateTime.Now,
                            rjlysj = DateTime.Now,                            
                        });
                    }
                    sys_import_result<base_dbrjzx> ret = _impservice.NewImportData(list);
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
            finally
            {
                
            }
        }
    }
}
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
using ZDMesInterfaces.LBJ.DZWD;
using ZDMesModels;
using ZDMesModels.LBJ;
namespace MesAdmin.Controllers.LBJ.DZWD
{
    [RoutePrefix("api/lbj/jtgl")]
    public class JTGLController : ApiController
    {
        private IDbOperate<zxjc_t_jstc> _jtglservice;
        private IAudit<zxjc_t_jstc> _auditservice;
        private IJsTc _jts;
        public JTGLController(IDbOperate<zxjc_t_jstc> jtglservice, IAudit<zxjc_t_jstc> auditservice, IJsTc jts)
        {
            _jtglservice = jtglservice;
            _auditservice = auditservice;
            _jts = jts;
        }

        [HttpPost, SearchFilter, Route("list")]
        public IHttpActionResult GetList(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _jtglservice.GetList(parm, out resultcount);
                foreach (var item in list)
                {
                    item.fpmx = _jts.Fp_Detail(item.jtid);
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
                List<zxjc_t_jstc> deldata = new List<zxjc_t_jstc>();
                List<zxjc_t_jstc> nodeldata = new List<zxjc_t_jstc>();
                foreach (var item in entitys)
                {
                    var candel = _jts.CanDel(item.jtid);
                    if (candel)
                    {
                        deldata.Add(item);
                    }
                    else
                    {
                        nodeldata.Add(item);
                    }
                }
                if (nodeldata.Count > 0)
                {
                    return Json(new sys_result()
                    {
                        code = 2,
                        msg = "已审核或已分配的文档不能删除"
                    });
                }
                var ret = _jtglservice.Del(deldata);
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
                    DataTable dataTable = cells.ExportDataTableAsString(1, 0, cells.MaxDataRow, cells.MaxColumn + 1);
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
        [HttpPost, SearchFilter, Route("auditlist")]
        public IHttpActionResult AuditList(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                //if (!string.IsNullOrEmpty(parm.sqlexp))
                //{
                //    parm.sqlexp += " and shbz = 'N' ";
                //}
                //else
                //{
                //    parm.sqlexp = " shbz = 'N' ";
                //}                
                var list = _jtglservice.GetList(parm, out resultcount);
                foreach (var item in list)
                {
                    item.fpmx = _jts.Fp_Detail(item.jtid);
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
        [HttpPost, SearchFilter, Route("mydoclist")]
        public IHttpActionResult MyDocList(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _jts.My_Doc_List(parm, out resultcount);
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
        [HttpPost,Route("audit")]
        public IHttpActionResult AuditJSTC(List<zxjc_t_jstc> entitys)
        {
            try
            {
                var ids = entitys.Select(t => t.jtid).ToList();
               var ret = _auditservice.AuditBill(ids);
                if (ret)
                {
                    return Json(new sys_result()
                    {
                        code = 1,
                        msg = "审核成功"
                    });
                }
                else
                {
                    return Json(new sys_result()
                    {
                        code = 0,
                        msg = "审核失败"
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
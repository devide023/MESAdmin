using MesAdmin.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ.ImportData;
using ZDMesModels;
using Newtonsoft.Json;
namespace MesAdmin.Controllers
{
    public class BaseApiController<T> : ApiController where T : class, new()
    {
        protected IDbOperate<T> _baseservice;
        protected IRequireVerify _requireverfify;
        protected IImportData<T> _importservice;
        public BaseApiController(IDbOperate<T> baseservice)
        {
            _baseservice = baseservice;
        }
        [HttpPost, SearchFilter, Route("list")]
        public virtual IHttpActionResult GetList(sys_page parm)
        {
            try
            {
                int resultcount = 0;
                var list = _baseservice.GetList(parm, out resultcount);
                return Json(new 
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
        [HttpPost, RequireVerify, Route("add")]
        public virtual IHttpActionResult Add(List<T> entitys)
        {
            try
            {
                int ret = _baseservice.Add(entitys);
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

        [HttpPost, RequireVerify, Route("edit")]
        public virtual IHttpActionResult Edit(List<T> entitys)
        {
            try
            {
                var ret = _baseservice.Modify(entitys);
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
        public virtual IHttpActionResult Del(List<T> entitys)
        {
            try
            {
                var routepath = RequestContext.RouteData.Route.RouteTemplate;
                var filename = HttpContext.Current.Server.MapPath($"~/sqlconfig/{ routepath.Replace("/", "-")}.json");
                FileInfo fi = new FileInfo(filename);
                if (fi.Exists)
                {
                   var config = JsonConvert.DeserializeObject<ZDMesModels.sys_search_config>(File.ReadAllText(filename));
                    if (config != null) {
                     
                    }
                }
                var ret = _baseservice.Del(entitys);
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

        [HttpGet, RequireVerify, Route("readxls")]
        public virtual IHttpActionResult ReadTempFile(string fileid)
        {
            try
            {
                List<T> list = new List<T>();
                object template_data = null;
                var isok = Request.Properties.TryGetValue("template_datalist", out template_data);
                if (isok)
                {
                    list = (template_data as IEnumerable<object>).ToList().ConvertAll(t => (T)t);
                }
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
                        msg = $"文件数据{list.Count()}条，重复{ret.repeatlist.Count}条"
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
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet, RequireVerify, Route("readxls_by_replace")]
        public virtual IHttpActionResult ReadTempFile_By_Replace(string fileid)
        {
            try
            {
                List<T> list = new List<T>();
                object template_data = null;
                var isok = Request.Properties.TryGetValue("template_datalist", out template_data);
                if (isok)
                {
                    list = (template_data as IEnumerable<object>).ToList().ConvertAll(t => (T)t);
                }
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
                        msg = $"文件数据{list.Count()}条，替换{ret.dellist.Count}条"
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
            catch (Exception)
            {

                throw;
            }
        }
        [HttpGet,RequireVerify, Route("readxls_by_zh")]
        public virtual IHttpActionResult ReadTempFile_By_Zh(string fileid)
        {
            try
            {
                List<T> list = new List<T>();
                object template_data = null;
                var isok = Request.Properties.TryGetValue("template_datalist", out template_data);
                if (isok)
                {
                    list = (template_data as IEnumerable<object>).ToList().ConvertAll(t => (T)t);
                }
                var ret = _importservice.ZhImportData(list);
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
                        msg = $"文件数据{list.Count()}条，替换{ret.dellist.Count}条"
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
            catch (Exception)
            {

                throw;
            }
        }
    }
}
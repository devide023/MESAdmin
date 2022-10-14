using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesModels.CDGC;
using System.Text;
using ZDMesModels;
using System.Web;
using ZDMesInterfaces.LBJ;
using ZDMesInterfaces.LBJ.ImportData;
using MesAdmin.Filters;

namespace MesAdmin.Controllers.CDGC.RYGL
{
    [RoutePrefix("api/cdgc/ryxx")]
    public class RYGLController : BaseApiController<zxjc_ryxx>
    {
        private IDbOperate<zxjc_ryxx> _ryxxservice;
        private IRequireVerify _requireverify;
        public RYGLController(IDbOperate<zxjc_ryxx> ryxxservice,IImportData<zxjc_ryxx> importservice, IRequireVerify requireverify) : base(ryxxservice)
        {
            _ryxxservice = ryxxservice;
            _importservice = importservice;
            _requireverify = requireverify;
        }
        [HttpPost,RequireVerify,Route("add_bycode")]
        public override IHttpActionResult Add(List<zxjc_ryxx> entitys)
        {
            try
            {
                var ret = _ryxxservice.Add(entitys);
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

        [TemplateVerify("ZDMesModels.CDGC.zxjc_ryxx,ZDMesModels")]
        public override IHttpActionResult ReadTempFile(string fileid)
        {
            try
            {
                string error = string.Empty;
                sys_result msg = new sys_result();
                //string filepath = HttpContext.Current.Server.MapPath($"~/Upload/Excel/{fileid}");
                //var list = _templateservice.ReadData<zxjc_ryxx>(filepath).ToList();
                object template_data = null;
                List<zxjc_ryxx> list = new List<zxjc_ryxx>();
                var isok = Request.Properties.TryGetValue("template_datalist", out template_data);
                if (isok)
                {
                    list = (template_data as List<object>).ConvertAll(t => (zxjc_ryxx)t);
                }
                if (list.Count > 0)
                {
                    _requireverify.VerifyRequire<zxjc_ryxx>(list);

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
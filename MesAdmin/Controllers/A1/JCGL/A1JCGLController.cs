using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesModels.TJ.A1;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ.ImportData;
using MesAdmin.Filters;
namespace MesAdmin.Controllers.A1.JCGL
{
    /// <summary>
    /// 人员奖惩
    /// </summary>
    [RoutePrefix("api/a1/jcgl")]
    public class A1JCGLController : BaseApiController<zxjc_jcgl>
    {
        public A1JCGLController(IDbOperate<zxjc_jcgl> jcglservice, IRequireVerify requireverfify, IImportData<zxjc_jcgl> importservice) :base(jcglservice)
        {
            this._requireverfify = requireverfify;
            this._importservice = importservice;
        }
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_jcgl,ZDMesModels")]
        public override IHttpActionResult ReadTempFile(string fileid)
        {
            try
            {
                List<zxjc_jcgl> list = new List<zxjc_jcgl>();
                object template_data = null;
                var isok = Request.Properties.TryGetValue("template_datalist", out template_data);
                if (isok)
                {
                    list = (template_data as List<object>).ConvertAll(t => (zxjc_jcgl)t);
                    foreach (var item in list)
                    {
                        if (item.lx == "奖励")
                        {
                            item.jcje = Math.Abs(item.jcje);
                        }
                        else if (item.lx == "惩罚")
                        {
                            item.jcje = -1 * Math.Abs(item.jcje);
                        }
                    }
                    _requireverfify.VerifyRequire<zxjc_jcgl>(list);
                }
                var ret = _importservice.NewImportData(list);
                if (ret.oklist.Count == list.Count)
                {
                    return Json(new 
                    {
                        code = 1,
                        msg = $"成功导入数据{list.Count()}条"
                    });
                }
                else if (ret.repeatlist.Count > 0)
                {
                    return Json(new 
                    {
                        code = 2,
                        msg = $"文件数据{list.Count()}条，重复{ret.repeatlist.Count}条"
                    });
                }
                else
                {
                    return Json(new 
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
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_jcgl,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Replace(string fileid)
        {
            return base.ReadTempFile_By_Replace(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_jcgl,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Zh(string fileid)
        {
            return base.ReadTempFile_By_Zh(fileid);
        }
    }
}
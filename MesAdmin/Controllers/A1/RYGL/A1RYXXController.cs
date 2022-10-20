using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesModels.TJ.A1;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.TJ;
using MesAdmin.Filters;
using ZDMesInterfaces.LBJ.ImportData;
using ZDMesModels;

namespace MesAdmin.Controllers.A1.RYGL
{
    [RoutePrefix("api/a1/rygl")]
    public class A1RYXXController : BaseApiController<zxjc_ryxx>
    {
        private IDbOperate<zxjc_ryxx> _zxjc_ryxx;
        private IRYGL _rygl;
        public A1RYXXController(IDbOperate<zxjc_ryxx> zxjc_ryxx,IRYGL rygl, IRequireVerify requireverfify, IImportData<zxjc_ryxx> importservice) :base(zxjc_ryxx)
        {
            _zxjc_ryxx = zxjc_ryxx;
            _rygl = rygl;
            _requireverfify = requireverfify;
            _importservice = importservice;
        }
        [HttpGet,Route("usercode")]
        public IHttpActionResult CreateUserCode()
        {
            try
            {
                return Json(new { code = 1, msg = "ok", usercode = _rygl.CreateUserCode() });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_ryxx,ZDMesModels")]
        public override IHttpActionResult ReadTempFile(string fileid)
        {
            try
            {
                List<zxjc_ryxx> list = new List<zxjc_ryxx>();
                object template_data = null;
                var isok = Request.Properties.TryGetValue("template_datalist", out template_data);
                if (isok)
                {
                    list = (template_data as List<object>).ConvertAll(t => (zxjc_ryxx)t);
                    list.ForEach(t => t.usercode = _rygl.CreateUserCode());
                    _requireverfify.VerifyRequire<zxjc_ryxx>(list);
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
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_ryxx,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Replace(string fileid)
        {
            return base.ReadTempFile_By_Replace(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_ryxx,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Zh(string fileid)
        {
            return base.ReadTempFile_By_Zh(fileid);
        }


    }
}
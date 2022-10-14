using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesModels.TJ.A1;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ.ImportData;
using ZDMesInterfaces.TJ;
using MesAdmin.Filters;
using ZDMesModels;

namespace MesAdmin.Controllers.A1.RYGL
{
    [RoutePrefix("api/a1/ryjn")]
    public class A1RYJNController : BaseApiController<zxjc_ryxx_jn>
    {
        private IRYJN _ryjn;
        public A1RYJNController(IDbOperate<zxjc_ryxx_jn> ryjnservice, IRYJN ryjn, IRequireVerify requireverfify, IImportData<zxjc_ryxx_jn> importservice) :base(ryjnservice)
        {
            this._requireverfify = requireverfify;
            this._importservice = importservice;
            _ryjn = ryjn;
        }
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_ryxx_jn,ZDMesModels")]
        public override IHttpActionResult ReadTempFile(string fileid)
        {
            try
            {
                List<zxjc_ryxx_jn> list = new List<zxjc_ryxx_jn>();
                object template_data = null;
                var isok = Request.Properties.TryGetValue("template_datalist", out template_data);
                if (isok)
                {
                    list = (template_data as List<object>).ConvertAll(t => (zxjc_ryxx_jn)t);
                    list.ForEach(t =>  t.jnbh = _ryjn.CreateJnCode());
                    _requireverfify.VerifyRequire<zxjc_ryxx_jn>(list);
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
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_ryxx_jn,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Replace(string fileid)
        {
            try
            {
                List<zxjc_ryxx_jn> list = new List<zxjc_ryxx_jn>();
                object template_data = null;
                var isok = Request.Properties.TryGetValue("template_datalist", out template_data);
                if (isok)
                {
                    list = (template_data as List<object>).ConvertAll(t => (zxjc_ryxx_jn)t);
                    list.ForEach(t =>  t.jnbh = _ryjn.CreateJnCode());
                    _requireverfify.VerifyRequire<zxjc_ryxx_jn>(list);
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
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_ryxx_jn,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Zh(string fileid)
        {
            try
            {
                List<zxjc_ryxx_jn> list = new List<zxjc_ryxx_jn>();
                object template_data = null;
                var isok = Request.Properties.TryGetValue("template_datalist", out template_data);
                if (isok)
                {
                    list = (template_data as List<object>).ConvertAll(t => (zxjc_ryxx_jn)t);
                    list.ForEach(t => t.jnbh = _ryjn.CreateJnCode());
                    _requireverfify.VerifyRequire<zxjc_ryxx_jn>(list);
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
    }
}
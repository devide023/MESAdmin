using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MesAdmin.Filters;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ.ImportData;
using ZDMesModels;
using ZDMesModels.TJ.A1;
namespace MesAdmin.Controllers.A1.JTGL
{
    [RoutePrefix("api/a1/jtgl")]
    public class A1JTGLController : BaseApiController<zxjc_t_jstc>
    {
        public A1JTGLController(IDbOperate<zxjc_t_jstc> jstzservice, IRequireVerify requireverfify, IImportData<zxjc_t_jstc> importservice):base(jstzservice)
        {
            this._requireverfify = requireverfify;
            this._importservice = importservice;
        }
        [HttpPost,SearchFilter,Route("toscxlist")]
        public IHttpActionResult JstzToScxList(sys_page parm)
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
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_t_jstc,ZDMesModels")]
        public override IHttpActionResult ReadTempFile(string fileid)
        {
            return base.ReadTempFile(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_t_jstc,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Replace(string fileid)
        {
            return base.ReadTempFile_By_Replace(fileid);
        }
        [TemplateVerify("ZDMesModels.TJ.A1.zxjc_t_jstc,ZDMesModels")]
        public override IHttpActionResult ReadTempFile_By_Zh(string fileid)
        {
            return base.ReadTempFile_By_Zh(fileid);
        }
    }
}
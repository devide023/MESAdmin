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
using ZDMesServices.TJ.A1.RYGL;

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
        [AtachValue(typeof(IBatAtachValue<zxjc_ryxx>), "BatSetValue")]
        public override IHttpActionResult Add(List<zxjc_ryxx> entitys)
        {
            return base.Add(entitys);
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
        [AtachValue(typeof(IBatAtachValue<zxjc_ryxx>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile(string fileid)
        {
           return base.ReadTempFile(fileid);
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
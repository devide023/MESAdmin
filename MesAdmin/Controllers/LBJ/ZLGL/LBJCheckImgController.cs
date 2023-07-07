using Autofac.Core;
using MesAdmin.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ.ImportData;
using ZDMesInterfaces.LBJ.ZLGL;
using ZDMesInterfaces.TJ;
using ZDMesModels.LBJ;

namespace MesAdmin.Controllers.LBJ.ZLGL
{
    [RoutePrefix("api/lbj/checkimg")]
    public class LBJCheckImgController : BaseApiController<zxjc_check_image>
    {
        private ILBJCheckImage _checkimgservice;
        public LBJCheckImgController(IDbOperate<zxjc_check_image> baseservice, ILBJCheckImage checkimgservice, IRequireVerify requireverfify, IImportData<zxjc_check_image> importservice) : base(baseservice)
        {
            _checkimgservice = checkimgservice;
             _requireverfify = requireverfify;
            _importservice = importservice;
        }

        [HttpPost,Route("images")]
        public IHttpActionResult GetCheckImages(zxjc_check_image parm)
        {
            try
            {
                var list = _checkimgservice.GetCheckImages(parm);
                return Json(new
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

        [TemplateVerify("ZDMesModels.LBJ.zxjc_check_image,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<zxjc_check_image>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile(string fileid)
        {
            return base.ReadTempFile(fileid);
        }
        [TemplateVerify("ZDMesModels.LBJ.zxjc_check_image,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<zxjc_check_image>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile_By_Replace(string fileid)
        {
            return base.ReadTempFile_By_Replace(fileid);
        }
        [TemplateVerify("ZDMesModels.LBJ.zxjc_check_image,ZDMesModels")]
        [AtachValue(typeof(IBatAtachValue<zxjc_check_image>), "BatSetValue")]
        public override IHttpActionResult ReadTempFile_By_Zh(string fileid)
        {
            return base.ReadTempFile_By_Zh(fileid);
        }
    }
}
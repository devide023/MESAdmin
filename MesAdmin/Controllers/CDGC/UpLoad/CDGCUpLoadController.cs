using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesModels;
using ZDMesServices.Common;

namespace MesAdmin.Controllers.CDGC.UpLoad
{
    /// <summary>
    /// 成都工厂PDF文件上传
    /// </summary>
    /// 
    [RoutePrefix("api/cdgc/upload")]
    public class CDGCUpLoadController : ApiController
    {
        private IUpLoad _uploadservice;
        public CDGCUpLoadController(IUpLoad uploadservice)
        {
            _uploadservice = uploadservice;
        }
        [HttpPost, Route("dzgy_pdf")]
        public IHttpActionResult Upload_DzGy_Pdf()
        {
            try
            {
                Dictionary<string, object> kv = new Dictionary<string, object>();
                _uploadservice.IsClientFileName = true;
                var list = _uploadservice.File2Ftp("电子工艺", UploadWjLx.pdf, out kv);
                if (list.Count() > 0)
                {
                    return Json(new { code = 1, msg = "上传成功", files = list, extdata = kv });
                }
                else
                {
                    return Json(new { code = 0, msg = "上传失败", files = list, extdata = kv });
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
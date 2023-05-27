using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ZDMesInterfaces.Common;
using ZDMesModels;
using ZDMesServices.Common;

namespace MesAdmin.Controllers.DuCar.UpLoad
{
    [RoutePrefix("api/ducar/upload")]
    public class DucarUpLoadController : ApiController
    {
        private IUpLoad _uploadservice;
        public DucarUpLoadController(IUpLoad uploadservice)
        {
            _uploadservice = uploadservice;
        }
        [HttpPost, Route("jstc_pdf")]
        public IHttpActionResult Upload_Jstz_Pdf()
        {
            try
            {
                Dictionary<string, object> kv = new Dictionary<string, object>();
                _uploadservice.IsClientFileName = true;
                var list = _uploadservice.File2Ftp("技术通知", UploadWjLx.pdf, out kv);
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
        [HttpPost, Route("gysp")]
        public IHttpActionResult Upload_Dzgy_Mp4()
        {
            try
            {
                Dictionary<string, object> kv = new Dictionary<string, object>();
                _uploadservice.IsClientFileName = true;
                var list = _uploadservice.File2Ftp("视频", UploadWjLx.video, out kv);
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
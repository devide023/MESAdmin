using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace MesAdmin.Controllers.Common
{
    [RoutePrefix("api/upload")]
    public class UpLoadController : ApiController
    {
        [HttpPost, Route("pdf")]
        public IHttpActionResult UploadPdf()
        {
            try
            {
                string serverpath = HttpContext.Current.Server.MapPath("~/Upload/PDF/");
                var extdata = HttpContext.Current.Request.Form;
                Dictionary<string, object> kv = new Dictionary<string, object>();
                if (extdata != null)
                {
                    if (extdata != null)
                    {
                        foreach (var item in extdata.AllKeys)
                        {
                            kv.Add(item, extdata.Get(item));
                        }
                    }
                }
                HttpFileCollection files = HttpContext.Current.Request.Files;
                List<dynamic> list = new List<dynamic>();
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFile file = HttpContext.Current.Request.Files[i];
                    string client_filename = file.FileName;
                    int fileszie = file.ContentLength;
                    int pos = client_filename.LastIndexOf(".");
                    string filetype = client_filename.Substring(pos, client_filename.Length - pos);
                    string guid = Guid.NewGuid().ToString() + filetype;
                    file.SaveAs(serverpath + guid);
                    list.Add(new { fileid = guid, filename = client_filename, filesize = fileszie });
                }
                return Json(new { code = 1, msg = "上传成功", files = list, extdata = kv });
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost,Route("image")]
        public IHttpActionResult UpLoadImage()
        {
            try
            {
                var extdata = HttpContext.Current.Request.Form;
                Dictionary<string, object> kv = new Dictionary<string, object>();
                string rowindex = string.Empty;
                object rowkey;
                if (extdata != null)
                {
                    foreach (var item in extdata.AllKeys)
                    {
                        kv.Add(item, extdata.Get(item));
                    }
                    if (kv.TryGetValue("rowkey", out rowkey))
                    {
                        rowindex = rowkey.ToString();
                    }
                }
                string serverpath = HttpContext.Current.Server.MapPath("~/Upload/Image/");
                HttpFileCollection files = HttpContext.Current.Request.Files;
                List<dynamic> list = new List<dynamic>();
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFile file = HttpContext.Current.Request.Files[i];
                    string client_filename = file.FileName;
                    int pos = client_filename.LastIndexOf(".");
                    string filetype = client_filename.Substring(pos, client_filename.Length - pos);
                    string guid = Guid.NewGuid().ToString() + filetype;
                    file.SaveAs(serverpath + guid);
                    int fileszie = file.ContentLength;
                    string imgurl = HttpContext.Current.Request.Url.ToString().Replace(HttpContext.Current.Request.Path, "") + "/upload/image/" + guid;
                    list.Add(new { imgurl = imgurl, serverfilename = guid, rowindex = rowindex });
                }
                return Json(new { code = 1, msg = "上传成功", files = list });
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost, Route("xls")]
        public IHttpActionResult upload_xls_file()
        {
            try
            {
                HttpFileCollection files = HttpContext.Current.Request.Files;
                string savepath = HttpContext.Current.Server.MapPath("~/Upload/Excel/");
                List<dynamic> list = new List<dynamic>();
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFile file = HttpContext.Current.Request.Files[i];
                    string client_filename = file.FileName;
                    int fileszie = file.ContentLength;
                    int pos = client_filename.LastIndexOf(".");
                    string filetype = client_filename.Substring(pos, client_filename.Length - pos);
                    string newfilename = Guid.NewGuid().ToString() + filetype;
                    string fullfilename = savepath + newfilename;
                    file.SaveAs(fullfilename);
                    list.Add(new { fileid = newfilename, filename = client_filename, filesize = fileszie });
                }
                return Json(new { code = 1, msg = "上传成功", files = list });
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet, AllowAnonymous, Route("downloadpdf")]
        public HttpResponseMessage DownLoadPdf(string filename)
        {
            try
            {
                var strPath = HttpContext.Current.Server.MapPath("~/Upload/PDF/" + filename);
                var stream = new FileStream(strPath, FileMode.Open);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StreamContent(stream);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = filename
                };
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
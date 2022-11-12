using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ;
using ZDMesModels;
using ZDToolHelper;

namespace MesAdmin.Controllers.Common
{
    [RoutePrefix("api/upload")]
    public class UpLoadController : ApiController
    {
        private IUpLoad _uploadservice;
        public UpLoadController(IUpLoad uploadservice)
        {
            _uploadservice = uploadservice;
        }
        private IBaseInfo _baseinfo;
        public UpLoadController(IBaseInfo baseinfo)
        {
            _baseinfo = baseinfo;
        }
        private List<dynamic> File2Ftp(string wjlx, UploadWjLx lx, out Dictionary<string, object> kv)
        {
            try
            {
                var qftpconfig = _baseinfo.FtpConfig().Where(t=>t.filetype == wjlx);
                var extdata = HttpContext.Current.Request.Form;
                kv = new Dictionary<string, object>();
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
                if (qftpconfig.Count() > 0)
                {
                  base_ftpfilepath ftpconfig =  qftpconfig.First();
                    string spath = string.Empty;
                    switch (lx)
                    {
                        case UploadWjLx.pdf:
                            spath = "~/Upload/PDF/";
                            break;
                        case UploadWjLx.image:
                            spath = "~/Upload/Image/";
                            break;
                        case UploadWjLx.excel:
                            spath = "~/Upload/Excel/";
                            break;
                        case UploadWjLx.video:
                            spath = "~/Upload/Video/";
                            break;
                        default:
                            spath = "~/Upload/";
                            break;
                    }
                    string serverpath = HttpContext.Current.Server.MapPath(spath);
                    HttpFileCollection files = HttpContext.Current.Request.Files;
                    List<dynamic> list = new List<dynamic>();
                    FtpHelper ftphelper = new FtpHelper();
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFile file = HttpContext.Current.Request.Files[i];
                        string client_filename = file.FileName;
                        int fileszie = file.ContentLength;
                        int pos = client_filename.LastIndexOf(".");
                        string filetype = client_filename.Substring(pos, client_filename.Length - pos);
                        string guid = Guid.NewGuid().ToString() + filetype;
                        file.SaveAs(serverpath + guid);
                        ftphelper.UploadFile(file.InputStream, ftpconfig.ftpurl + ":" + ftpconfig.ftpport + ftpconfig.filepath, guid, ftpconfig.ftpuser, ftpconfig.ftppassword);
                        list.Add(new { fileid = guid, filename = client_filename, filesize = fileszie });
                    }
                    return list;
                }
                else
                {
                    return new List<dynamic>();
                }                
            }
            catch (Exception)
            {

        //        throw;
        //    }
        //}
        [HttpPost, Route("video")]
        public IHttpActionResult Uplad_DzGy_Mp4()
        {
            try
            {
                Dictionary<string, object> kv = new Dictionary<string, object>();
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
        /// <summary>
        /// 电子工艺PDF
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("dzgy_pdf")]
        public IHttpActionResult Uplad_DzGy_Pdf()
        {
            try
            {
                Dictionary<string, object> kv = new Dictionary<string, object>();
                var list = _uploadservice.File2Ftp("电子工艺", UploadWjLx.pdf,out kv);
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
        /// <summary>
        /// 技术通知PDF
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("jstc_pdf")]
        public IHttpActionResult Uplad_Jstc_Pdf()
        {
            try
            {
                Dictionary<string, object> kv = new Dictionary<string, object>();
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
                List<dynamic> result_list = new List<dynamic>();
                Dictionary<string, object> kv = new Dictionary<string, object>();
                var list = _uploadservice.File2Ftp("头像", UploadWjLx.image, out kv);
                if (list.Count() > 0)
                {
                    foreach (var item in list)
                    {
                        string imgurl = HttpContext.Current.Request.Url.ToString().Replace(HttpContext.Current.Request.Path, "") + "/upload/image/" + item.fileid;
                        result_list.Add(new
                        {
                            fileid = item.fileid,
                            imgurl = imgurl,
                            filename = item.filename,
                            filesize = item.filesize
                        });
                    }
                    return Json(new { code = 1, msg = "上传成功", files = result_list, extdata = kv });
                }
                else
                {
                    return Json(new { code = 0, msg = "上传失败", files = result_list, extdata = kv });
                }
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
                var list = _uploadservice.UpLoadFile("~/Upload/Excel/");
                //HttpFileCollection files = HttpContext.Current.Request.Files;
                //string savepath = HttpContext.Current.Server.MapPath("~/Upload/Excel/");
                //List<dynamic> list = new List<dynamic>();
                //for (int i = 0; i < files.Count; i++)
                //{
                //    HttpPostedFile file = HttpContext.Current.Request.Files[i];
                //    string client_filename = file.FileName;
                //    int fileszie = file.ContentLength;
                //    int pos = client_filename.LastIndexOf(".");
                //    string filetype = client_filename.Substring(pos, client_filename.Length - pos);
                //    string newfilename = Guid.NewGuid().ToString() + filetype;
                //    string fullfilename = savepath + newfilename;
                //    file.SaveAs(fullfilename);
                //    list.Add(new { fileid = newfilename, filename = client_filename, filesize = fileszie });
                //}
                return Json(new { code = 1, msg = "上传成功", files = list });
            }
            catch (Exception)
            {
                throw;
            }
        }
        
    }
}
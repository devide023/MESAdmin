using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using ZDMesInterfaces.LBJ;

namespace MesAdmin.Controllers.Common
{
    [RoutePrefix("api/download")]
    public class DownLoadController : ApiController
    {
        private IBaseInfo _baseinfo;
        public DownLoadController(IBaseInfo baseinfo)
        {
            _baseinfo = baseinfo;
        }
        /// <summary>
        /// 从ftp下载文件暂存到web服务器
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAnonymous, Route("ftp2web")]
        public IHttpActionResult DownLoadFilefromFtp(string wjlx, string wjlj)
        {
            try
            {
                var q = _baseinfo.FtpConfig().Where(t => t.filetype == wjlx);
                if (q.Count() > 0)
                {
                    var ftpconf = q.First();
                    string path = $"ftp://{ftpconf.ftpuser}:{ftpconf.ftppassword}@{ftpconf.ftpurl}:{ftpconf.ftpport}{ftpconf.filepath}{wjlj}";
                    string downpath = HttpContext.Current.Server.MapPath("~/Upload/PDF/");
                    string localfile = downpath + wjlj;
                    FtpWebRequest reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(path));
                    using (FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse())
                    {
                        using (Stream responseStream = response.GetResponseStream())
                        {
                            using (FileStream fs = new FileStream(localfile, FileMode.OpenOrCreate))
                            {
                                try
                                {
                                    byte[] buffer = new byte[20480];
                                    int read = 0;
                                    do
                                    {
                                        read = responseStream.Read(buffer, 0, buffer.Length);
                                        fs.Write(buffer, 0, read);
                                    } while (!(read == 0));
                                    responseStream.Close();
                                    fs.Flush();
                                    fs.Close();
                                }
                                catch (Exception)
                                {
                                    fs.Close();
                                    throw;
                                }
                            }
                            responseStream.Close();
                        }
                        response.Close();
                    }
                    return Json(new { code = 1, msg = "文件从ftp下载成功", downloadid = wjlj });
                }
                else
                {
                    return Json(new { code = 0, msg = "文件从ftp下载失败" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet, AllowAnonymous, Route("downloadpdf")]
        public HttpResponseMessage DownLoadPdf(string wjlj)
        {
            try
            {
                var strPath = HttpContext.Current.Server.MapPath("~/Upload/PDF/" + wjlj);
                var stream = new FileStream(strPath, FileMode.Open);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StreamContent(stream);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = wjlj
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
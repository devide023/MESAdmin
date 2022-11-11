using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ZDMesInterfaces.Common;
using ZDMesModels;
using ZDToolHelper;

namespace ZDMesServices.Common
{
    public class UpLoadService : IUpLoad
    {
        private IFtpConfig _ftpservice;
        public UpLoadService(IFtpConfig ftpservice)
        {
            _ftpservice = ftpservice;
        }

        public bool IsClientFileName { get; set; } = false;

        public List<sys_upload_file_info> File2Ftp(string wjlx, UploadWjLx lx, out Dictionary<string, object> kv)
        {
            try
            {
                var qftpconfig = _ftpservice.FtpConfig().Where(t => t.filetype == wjlx);
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
                    base_ftpfilepath ftpconfig = qftpconfig.First();
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
                    List<sys_upload_file_info> list = new List<sys_upload_file_info>();
                    FtpHelper ftphelper = new FtpHelper();
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFile file = HttpContext.Current.Request.Files[i];
                        string client_filename = file.FileName;
                        int fileszie = file.ContentLength;
                        int pos = client_filename.LastIndexOf(".");
                        string filetype = client_filename.Substring(pos, client_filename.Length - pos);
                        string guid = Guid.NewGuid().ToString() + filetype;
                        if (IsClientFileName)
                        {
                            guid = file.FileName;
                        }
                        file.SaveAs(serverpath + guid);
                        ftphelper.UploadFile(file.InputStream, ftpconfig.ftpurl + ":" + ftpconfig.ftpport + ftpconfig.filepath, guid, ftpconfig.ftpuser, ftpconfig.ftppassword);
                        list.Add(new sys_upload_file_info(){ fileid = guid, filename = client_filename, filesize = fileszie });
                    }
                    return list;
                }
                else
                {
                    return new List<sys_upload_file_info>();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<dynamic> UpLoadFile(string path)
        {
            try
            {
                HttpFileCollection files = HttpContext.Current.Request.Files;
                string savepath = HttpContext.Current.Server.MapPath(path);
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
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

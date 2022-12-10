using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;

namespace ZDMesInterfaces.Common
{
    public interface IUpLoad
    {
        /// <summary>
        /// 是否按上传文件名命名服务器文件名
        /// </summary>
        bool IsClientFileName { get; set; }
        /// <summary>
        /// 文件上传到Ftp
        /// </summary>
        /// <param name="wjlx"></param>
        /// <param name="lx"></param>
        /// <param name="kv"></param>
        /// <returns></returns>
        List<sys_upload_file_info> File2Ftp(string wjlx, UploadWjLx lx, out Dictionary<string, object> kv);
        /// <summary>
        /// 按路径存储文件
        /// </summary>
        /// <param name="wjlx"></param>
        /// <param name="lx"></param>
        /// <param name="kv"></param>
        /// <returns></returns>
        List<sys_upload_file_info> File2FtpByPath(string wjlx, UploadWjLx lx,string path, out Dictionary<string, object> kv);
        /// <summary>
        /// 上传文件到web服务器
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        List<dynamic> UpLoadFile(string path);
    }
}

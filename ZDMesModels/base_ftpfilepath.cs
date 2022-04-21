using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels
{
    public class base_ftpfilepath
    {
        /// <summary>
        /// 文件类型
        /// </summary>
        public string filetype { get; set; }
        /// <summary>
        /// ftp地址
        /// </summary>
        public string ftpurl { get; set; }
        /// <summary>
        /// ftp端口号
        /// </summary>
        public string ftpport { get; set; }
        /// <summary>
        /// ftp用户名
        /// </summary>
        public string ftpuser { get; set; }
        /// <summary>
        /// ftp用户密码
        /// </summary>
        public string ftppassword { get; set; }
        /// <summary>
        /// ftp文件夹
        /// </summary>
        public string filepath { get; set; }
    }

    public class base_ftpfilepath_mapper : ClassMapper<base_ftpfilepath>
    {
        public base_ftpfilepath_mapper()
        {
            AutoMap();
        }
    }
}

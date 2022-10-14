using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.TJ.A1
{
    public class base_ftpfilepath
    {
        /// <summary>
        /// 文件类别（工艺 计通 质量警示 AI照片 相似件 物料 人员照片）
        /// </summary>
        public string filetype { get; set; }
        /// <summary>
        /// FTP地址
        /// </summary>
        public string ftpurl { get; set; }
        /// <summary>
        /// 端口号
        /// </summary>
        public string ftpport { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string ftpuser { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string ftppassword { get; set; }
        /// <summary>
        /// 文件路径
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

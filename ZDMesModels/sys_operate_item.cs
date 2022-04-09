using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels
{
    public class sys_operate_item
    {
        /// <summary>
        /// 操作列下拉菜单名称
        /// </summary>
        public string label { get; set; }
        /// <summary>
        /// 操作列下拉菜单类型
        /// </summary>
        public string btntype { get; set; }
        /// <summary>
        /// 操作列下拉菜单执行的函数名称
        /// </summary>
        public string fnname { get; set; }
        /// <summary>
        /// 操作列下拉菜单上传文件url
        /// </summary>
        public string action { get; set; }
        /// <summary>
        /// 操作列下拉菜单回调函数
        /// </summary>
        public string callback { get; set; }
    }
}

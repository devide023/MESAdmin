using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels
{
    public class sys_page_api
    {
        /// <summary>
        /// 接口名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// api地址
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// api请求方式
        /// </summary>
        public string method { get; set; }
        /// <summary>
        /// 回调函数
        /// </summary>
        public string callback { get; set; }

    }
}

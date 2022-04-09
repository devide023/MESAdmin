using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels
{
    public class sys_page_field
    {
        public string coltype { get; set; }
        public string label { get; set; }
        public string prop { get; set; }
        public string dbprop { get; set; }
        public string width { get; set; }
        public bool overflowtooltip { get; set; }
        public string headeralign { get; set; }
        public string align { get; set; }
        /// <summary>
        /// 接口url
        /// </summary>
        public string url { get; set; } = string.Empty;
        public string method { get; set; } = string.Empty;
        /// <summary>
        /// 函数体
        /// </summary>
        public string callback { get; set; } = string.Empty;
        /// <summary>
        /// 执行的函数名称
        /// </summary>
        public string function_name { get; set; } = string.Empty;
    }
}

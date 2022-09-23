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
        /// <summary>
        /// 关联对象属性
        /// </summary>
        public string subprop { get; set; }
        public string width { get; set; }
        /// <summary>
        /// 是否启用建议输入
        /// </summary>
        public string suggest { get; set; }
        public bool overflowtooltip { get; set; }
        /// <summary>
        /// 是否在下拉搜索中列出
        /// </summary>
        public bool searchable { get; set; }
        /// <summary>
        /// 是否排序
        /// </summary>
        public bool sortable { get; set; }
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

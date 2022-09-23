using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels
{
    public class sys_field_info
    {
        /// <summary>
        /// 列名称
        /// </summary>
        public string label { get; set; }
        /// <summary>
        /// 表字段名称
        /// </summary>
        public string prop { get; set; }
        /// <summary>
        /// 多表查询时，表字段名称 ta.id、tb.id
        /// </summary>
        public string dbprop { get; set; }
        /// <summary>
        /// 列类型
        /// </summary>
        public string coltype { get; set; } 
        /// <summary>
        /// 列头对齐方式
        /// </summary>
        public string headeralign { get; set; } = "center";
        /// <summary>
        /// 单元格对齐方式
        /// </summary>
        public string align { get; set; } = "center";
        /// <summary>
        /// 是否排序
        /// </summary>
        public bool sortable { get; set; } = true;
        /// <summary>
        /// 是否溢出显示提示
        /// </summary>
        public bool overflowtooltip { get; set; } = true;
    }
}

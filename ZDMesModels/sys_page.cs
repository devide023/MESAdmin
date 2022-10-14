using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels
{
    public class sys_page
    {
        /// <summary>
        /// 默认排序列名
        /// </summary>
        public string default_order_colname { get; set; } = string.Empty;
        /// <summary>
        /// sql查询语句配置
        /// </summary>
        public sys_search_config sqlconfig { get; set; }
        /// <summary>
        /// sql表达式
        /// </summary>
        public string sqlexp { get; set; } = string.Empty;
        public string orderbyexp { get; set; } = string.Empty;
        /// <summary>
        /// sql表达式参数
        /// </summary>
        public DynamicParameters sqlparam { get; set; } = new DynamicParameters();
        public List<sys_search> search_condition { get; set; } = new List<sys_search>();
        /// <summary>
        /// 排序条件
        /// </summary>
        public List<sys_sort> px_condition { get; set; } = new List<sys_sort>();
        public int pageindex { get; set; } = 1;
        /// <summary>
        /// 每页记录数
        /// </summary>
        public int pagesize { get; set; } = 20;
        /// <summary>
        /// 记录总条数
        /// </summary>
        public int resultcount { get; set; }
    }
}

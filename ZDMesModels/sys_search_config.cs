using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels
{
    public class sys_search_config
    {
        /// <summary>
        /// 主查询语句
        /// </summary>
        public string sql { get; set; } = string.Empty;
        /// <summary>
        /// 记录集条数统计
        /// </summary>
        public string sql_cnt { get; set; } = string.Empty;
        /// <summary>
        /// 排序条件
        /// </summary>
        public string sql_orderby { get; set; } = string.Empty;
    }
}

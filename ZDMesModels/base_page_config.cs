using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels
{
    public class base_page_config
    {
        /// <summary>
        /// 是否高级查询
        /// </summary>
        public bool isgradequery { get; set; }
        /// <summary>
        /// 是否批量操作
        /// </summary>
        public bool isbatoperate { get; set; }
        /// <summary>
        /// 是否操作列
        /// </summary>
        public bool isoperate { get; set; }
        /// <summary>
        /// 是否刷新
        /// </summary>
        public bool isfresh { get; set; }
        /// <summary>
        /// 是否选择列
        /// </summary>
        public bool isselect { get; set; }
    }
}

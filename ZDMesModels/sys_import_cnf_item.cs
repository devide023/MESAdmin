using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels
{
    /// <summary>
    /// 批量导入配置项
    /// </summary>
    public class sys_import_cnf_item
    {
        /// <summary>
        /// //新增导入url
        /// </summary>
        public string addurl { get; set; } = string.Empty;
        /// <summary>
        /// 替换导入url
        /// </summary>
        public string replaceurl { get; set; } = string.Empty;
        /// <summary>
        /// 综合导入url
        /// </summary>
        public string zongheurl { get; set; } = string.Empty;
    }
}

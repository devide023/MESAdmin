using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.LBJ
{
    public class sys_import_log_config
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string tablename { get; set; }
        /// <summary>
        /// 新增导入规则，查询字段名称
        /// </summary>
        public List<string> xinzeng { get; set; }
        /// <summary>
        /// 提换导入规则，查询字段名称
        /// </summary>
        public List<string> replace { get; set; }
        /// <summary>
        /// 综合导入规则，查询字段名称
        /// </summary>
        public List<string> zhonghe { get; set; }
        /// <summary>
        /// 综合导入规则，更新字段
        /// </summary>
        public List<string> updatecol { get; set; }
    }

}

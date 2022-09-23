using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels
{
    /// <summary>
    /// 模板数据列对应数据配置
    /// </summary>
    public class sys_template_config
    {
        public string tablename { get; set; }
        public List<config_item> conf { get; set; }
    }

    public class config_item
    {
        /// <summary>
        /// 数据库字段名
        /// </summary>
        public string dbcolname { get; set; }
        /// <summary>
        /// 数据库字段类型
        /// </summary>
        public string dbcoltype { get; set; }
        /// <summary>
        /// 模板数据列索引
        /// </summary>
        public int fieldindex { get; set; }
        /// <summary>
        /// 模板数据列名称
        /// </summary>
        public string fieldname { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels
{
    /// <summary>
    /// 数据校验配置
    /// </summary>
    public class sys_verify_config
    {
        /// <summary>
        /// 实体名称
        /// </summary>
        public string model { get; set; }
        /// <summary>
        /// 必填字段配置
        /// </summary>
        public List<config_item> requireconf { get; set; }
        /// <summary>
        /// 模板字段配置
        /// </summary>
        public List<config_item> templateconf { get; set; }
    }

    public class config_item
    {
        /// <summary>
        /// 数据库字段名
        /// </summary>
        public string colname { get; set; }
        /// 模板数据列索引
        /// </summary>
        public int colindex { get; set; }
        /// <summary>
        /// 模板数据列名称
        /// </summary>
        public string coltxt { get; set; }
    }
}

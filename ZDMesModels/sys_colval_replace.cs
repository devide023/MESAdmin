using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels
{
    /// <summary>
    /// 列批量替换实体
    /// </summary>
    public class sys_colval_replace
    {
        /// <summary>
        /// vue路由路径
        /// </summary>
        public string routepath { get; set; }
        /// <summary>
        /// 查询条件
        /// </summary>
        public List<sys_com_search> queryexp { get; set; }
        /// <summary>
        /// 列替换值
        /// </summary>
        public List<sys_col_replace> replaceexp { get; set; }
    }

    public class sys_com_search
    {
        public string coltype { get; set; } = "string";
        public string left { get; set; }
        public string right { get; set; }
        public string colname { get; set; }
        public string oper { get; set; } = "like";
        public string value { get; set; } = string.Empty;
        public List<string> values { get; set; } = new List<string>();
        public string logic { get; set; } = string.Empty;
    }

    public class sys_col_replace
    {
        public string colname { get; set; }
        public string replacevalue { get; set; }
    }
    /// <summary>
    /// 路由路径与表名对应关系
    /// </summary>
    public class route_tablename_config
    {
        public List<colreplace_config_item> colvalreplace_config { get; set; }
    }

    public class colreplace_config_item
    {
        /// <summary>
        /// vue路由
        /// </summary>
        public string routepath { get; set; }
        /// <summary>
        /// 表名
        /// </summary>
        public string tablename { get; set; }
    }

    public class colreplace_sql_expression
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string tablename { get; set; }
        /// <summary>
        /// 所有列sql
        /// </summary>
        public string sql_all_cols { get; set; }
        /// <summary>
        /// 查询ids语句
        /// </summary>
        public string sql { get; set; }
        /// <summary>
        /// 查询参数
        /// </summary>
        public DynamicParameters  select_param { get; set; }
        /// <summary>
        /// 更新语句
        /// </summary>
        public string updatesql { get; set; }
        /// <summary>
        /// 更新参数
        /// </summary>
        public DynamicParameters update_param { get; set; }
    }
}

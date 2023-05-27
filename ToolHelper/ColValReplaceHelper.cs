using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ZDMesModels;
namespace ZDToolHelper
{
    public static class ColValReplaceHelper
    {
        /// <summary>
        /// 获取列值批量替换sql表达式
        /// </summary>
        /// <param name="parm"></param>
        public static colreplace_sql_expression Get_Replace_Exp(sys_colval_replace parm)
        {
            colreplace_sql_expression ret = new colreplace_sql_expression();
            try
            {
                string configpath = HttpContext.Current.Server.MapPath("~/ColumnValReplace_Config.json");
                string config_json = File.ReadAllText(configpath, Encoding.UTF8);
                //路由与表名对应关系
                var route_tablename = JsonConvert.DeserializeObject<route_tablename_config>(config_json);
                var q = route_tablename.colvalreplace_config.Where(t => t.routepath == parm.routepath);
                if(q.Count() > 0 )
                {
                    int index = 0;
                    string tablename = q.First().tablename;
                    ret.tablename = tablename;
                    Dapper.DynamicParameters p = new Dapper.DynamicParameters();
                    StringBuilder sql = new StringBuilder();
                    StringBuilder sql_allcols = new StringBuilder();
                    sql.Append($"select rowid as rid FROM {tablename} where 1=1 ");
                    sql_allcols.Append($"select * FROM {tablename} where 1=1 ");
                    StringBuilder sqlwhere = new StringBuilder();
                    foreach (var item in parm.queryexp)
                    {
                        sqlwhere.Append($" {item.left} {item.colname}  {item.oper}  :{item.colname}{index} {item.right} {item.logic} ");
                        if (item.oper == "like")
                        {
                            p.Add($":{item.colname}{index}", "%" + item.value + "%");
                        }
                        else
                        {
                           p.Add($":{item.colname}{index}", item.value);
                        }
                        index++;
                    }
                    if (sqlwhere.Length > 0)
                    {
                        sql.Append(" and ");
                        sql.Append(sqlwhere);
                        //
                        sql_allcols.Append(" and ");
                        sql_allcols.Append(sqlwhere);
                    }
                    ret.sql = sql.ToString();
                    ret.select_param = p;
                    //
                    ret.sql_all_cols = sql_allcols.ToString();
                    StringBuilder updatesql = new StringBuilder();
                    Dapper.DynamicParameters p1 = new Dapper.DynamicParameters();
                    updatesql.Append($"update {tablename} set ");
                    index = 0;
                    foreach (var item in parm.replaceexp)
                    {
                        updatesql.Append($"{item.colname} = :{item.colname}{index},");
                        p1.Add($":{item.colname}{index}", item.replacevalue);
                        index++;
                    }
                    if (updatesql.Length > 0)
                    {
                        updatesql.Remove(updatesql.Length - 1, 1);
                    }
                    updatesql.Append(" where rowid in :rid");

                    ret.updatesql= updatesql.ToString();
                    ret.update_param = p1;

                }
                else
                {
                    throw new Exception("未在ColumnValReplace_Config.json文件配置列替换参数");
                }

                return ret;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

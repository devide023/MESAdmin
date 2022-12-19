using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using ZDMesModels;
using System.IO;
using Newtonsoft.Json;
namespace MesAdmin.Filters
{
    public class SearchFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            try
            {
                int index = 0;
                string expression = string.Empty;
                string orderexp = string.Empty;
                DynamicParameters p = new DynamicParameters();
                object obj;
                var isok = actionContext.ActionArguments.TryGetValue("parm", out obj);
                if (!isok || obj == null)
                {
                    return;
                }
                sys_page condition = obj as sys_page;
                string routetemplate = actionContext.ControllerContext.RouteData.Route.RouteTemplate;
                var sqlconfigpath = HttpContext.Current.Server.MapPath($"~/sqlconfig/{routetemplate.Replace("/", "-")}.json");
                FileInfo fi = new FileInfo(sqlconfigpath);
                if (fi.Exists)
                {
                    condition.sqlconfig = JsonConvert.DeserializeObject<sys_search_config>(File.ReadAllText(sqlconfigpath));
                }
                foreach (var item in condition.search_condition)
                {
                    switch (item.coltype.ToLower())
                    {
                        case "list":
                            if (item.values.Count > 0)
                            {
                                var templist = new List<string>();
                                item.values.ForEach(t => templist.Add(t.ToLower()));
                                expression = expression + $" {item.left} lower({item.colname}) {item.oper} :{item.colname.Replace(".", "_")}{index} {item.right} {item.logic} ";
                                p.Add($":{item.colname.Replace(".", "_")}{index}", templist);
                            }
                            break;
                        case "date":
                            if (!string.IsNullOrEmpty(item.value))
                            {
                                expression = expression + $" {item.left} trunc({item.colname}) {item.oper} :{item.colname.Replace(".", "_")}{index} {item.right} {item.logic} ";
                                p.Add($":{item.colname.Replace(".", "_")}{index}", Convert.ToDateTime(item.value), System.Data.DbType.Date, System.Data.ParameterDirection.Input);
                            }
                            else if (item.values.Count > 0 && item.oper == "between")
                            {
                                expression = expression + $" {item.left} trunc({item.colname}) between :{item.colname.Replace(".", "_")}{index}1 and :{item.colname.Replace(".", "_")}{index}2 {item.right} {item.logic} ";
                                p.Add($":{item.colname.Replace(".", "_")}{index}1", Convert.ToDateTime(item.values[0]), System.Data.DbType.Date, System.Data.ParameterDirection.Input);
                                p.Add($":{item.colname.Replace(".", "_")}{index}2", Convert.ToDateTime(item.values[1]), System.Data.DbType.Date, System.Data.ParameterDirection.Input);
                            }
                            break;
                        case "datetime":
                            if (!string.IsNullOrEmpty(item.value))
                            {
                                expression = expression + $" {item.left} {item.colname} {item.oper} :{item.colname.Replace(".", "_")}{index} {item.right} {item.logic} ";
                                p.Add($":{item.colname.Replace(".", "_")}{index}", Convert.ToDateTime(item.value), System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
                            }
                            else if (item.values!=null && item.values.Count > 0 && item.oper == "between")
                            {
                                expression = expression + $" {item.left} {item.colname} between :{item.colname.Replace(".", "_")}{index}3 and :{item.colname.Replace(".", "_")}{index}4 {item.right} {item.logic} ";
                                p.Add($":{item.colname.Replace(".", "_")}{index}3", Convert.ToDateTime(item.values[0]), System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
                                p.Add($":{item.colname.Replace(".", "_")}{index}4", Convert.ToDateTime(item.values[1]), System.Data.DbType.DateTime, System.Data.ParameterDirection.Input);
                            }
                            break;
                        case "bool":
                            expression = expression + $" {item.left} {item.colname} {item.oper} :{item.colname.Replace(".", "_")}{index} {item.right} {item.logic} ";
                            p.Add($":{item.colname.Replace(".", "_")}{index}", item.value);
                            break;
                        case "string":
                            expression = expression + $" {item.left} lower({item.colname}) {item.oper} :{item.colname.Replace(".", "_")}{index} {item.right} {item.logic} ";
                            if (item.oper == "like")
                            {
                                p.Add($":{item.colname.Replace(".", "_")}{index}", "%" + item.value.ToLower() + "%");
                            }
                            else
                            {
                                p.Add($":{item.colname.Replace(".", "_")}{index}", item.value.ToLower());
                            }
                            break;
                        case "int":
                            expression = expression + $" {item.left} {item.colname} {item.oper} :{item.colname.Replace(".", "_")}{index} {item.right} {item.logic} ";
                            p.Add($":{item.colname.Replace(".", "_")}{index}", Convert.ToInt32(item.value), System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                            break;
                        default:
                            expression = expression + $" {item.left} lower({item.colname}) {item.oper} :{item.colname.Replace(".", "_")}{index} {item.right} {item.logic} ";
                            p.Add($":{item.colname.Replace(".", "_")}{index}", item.value);
                            break;
                    }
                    index++;
                }
                if (condition.px_condition != null)
                {
                    foreach (var item in condition.px_condition)
                    {
                        orderexp = orderexp + $"{item.fieldname} {item.pxfs} ,";
                    }
                }
                if (!string.IsNullOrEmpty(orderexp))
                {
                    orderexp = " order by " + orderexp.Remove(orderexp.Length - 1, 1);
                }
                p.Add(":pageindex", condition.pageindex, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                p.Add(":pagesize", condition.pagesize, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                condition.orderbyexp = orderexp;
                condition.sqlexp = expression;
                condition.sqlparam = p;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
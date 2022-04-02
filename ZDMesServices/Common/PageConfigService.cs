using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.Common;
using ZDToolHelper;
using ZDMesModels;
using System.Web;
using Dapper;
using DapperExtensions;
using DapperExtensions.Predicate;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
namespace ZDMesServices.Common
{
    public class PageConfigService : OracleBaseFixture, IPageConfig
    {
        public PageConfigService(string constr):base(constr)
        {

        }

        public sys_menu_permis GetPagePermis(string route,string token)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select t1.permis ");
                sql.Append(" FROM   mes_role_menu t1, mes_menu_entity t2, (select ta.roleid ");
                sql.Append("          from mes_user_role ta, mes_user_entity tb ");
                sql.Append("          where  ta.userid = tb.id ");
                sql.Append("          and    tb.token = :token ) t3 ");
                sql.Append(" where  t1.menuid = t2.id ");
                sql.Append(" and t2.routepath = :path ");
                sql.Append(" and t1.roleid = t3.roleid ");
                var json = Db.Connection.ExecuteScalar<string>(sql.ToString(), new { path = route,token = token });
                return Newtonsoft.Json.JsonConvert.DeserializeObject<sys_menu_permis>(json);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GetPageConf(string route)
        {
            try
            {
                var configroot = HttpContext.Current.Server.MapPath("~/Config/");
                StringBuilder sql = new StringBuilder();
                sql.Append("select configpath from mes_menu_entity where routepath = :path ");
                var q = DB.Connection.Query<string>(sql.ToString(), new { path = route.Trim() });
                if (q.Count() > 0)
                {
                    var conf = q.First();
                    var configfullpath = configroot + conf;
                    return Tool.ReadFile(configfullpath);
                }
                else
                {
                    return "{}";
                }
            }
            catch (Exception)
            {
                return "{}";
                throw;
            }
        }

        public List<sys_field_info> GetPageFields(int menuid)
        {
            try
            {
                var configroot = HttpContext.Current.Server.MapPath("~/Config/");
                StringBuilder sql = new StringBuilder();
                sql.Append("select configpath from mes_menu_entity where id = :id ");
                var q = DB.Connection.Query<string>(sql.ToString(), new { id = menuid });
                if (q.Count() > 0)
                {
                    var conf = q.First();
                    var configfullpath = configroot + conf;
                    var jsstr = Tool.ReadFile(configfullpath);
                    Regex reg = new Regex(@"(?<fields>fields:[\w\W]*])");
                    var fieldsinfo = reg.Match(jsstr).Groups["fields"].Value;
                    fieldsinfo = fieldsinfo.Replace("fields:", "").Replace(" ","");
                    return JsonConvert.DeserializeObject<List<sys_field_info>>(fieldsinfo);
                }
                else
                {
                    return new List<sys_field_info>();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<sys_pagefn_info> GetPageFnList(string route, string token)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select t1.fnname,t1.btntxt,t1.btntype,t1.icon ");
                sql.Append("  FROM   mes_menu_entity t1, mes_role_menu t2, mes_user_role t3, mes_user_entity t4 ");
                sql.Append("  where  t1.id = t2.menuid ");
                sql.Append("  and    t2.roleid = t2.roleid ");
                sql.Append("  and    t3.userid = t4.id ");
                sql.Append("  and    t1.menutype = '03' ");
                sql.Append("  and    t1.pid = ");
                sql.Append("         (select id from mes_menu_entity where routepath = :route ) ");
                sql.Append("  and t4.token = :token order by t1.seq asc,t1.id asc");
                var q = DB.Connection.Query<sys_pagefn_info>(sql.ToString(), new { route = route, token = token });
                return q.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<sys_route_component> GetRouteComponent()
        {
            try
            {
                List<sys_route_component> list = new List<sys_route_component>();
                list.Add(new sys_route_component() { routepath="/home",componentname= "HomeComponent" });
                StringBuilder sql = new StringBuilder();
                sql.Append("select routepath, componentname ");
                sql.Append(" from mes_menu_entity ");
                sql.Append(" where menutype = '02' ");
                sql.Append(" order  by pid, id");
                list.AddRange(DB.Connection.Query<sys_route_component>(sql.ToString()));
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

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
    }
}

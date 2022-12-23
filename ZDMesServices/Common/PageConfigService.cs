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
using System.IO;
using DapperExtensions;
using System.Reflection;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Data;
using Oracle.ManagedDataAccess.Client;

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
                using (var db = new OracleConnection(ConString))
                {
                    var json = db.ExecuteScalar<string>(sql.ToString(), new { path = route, token = token });
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<sys_menu_permis>(json);
                }
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
                using (var db = new OracleConnection(ConString))
                {
                    var q = db.Query<string>(sql.ToString(), new { path = route.Trim() });
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
                using (var db = new OracleConnection(ConString))
                {
                    var q = db.Query<string>(sql.ToString(), new { id = menuid });
                    if (q.Count() > 0)
                    {
                        var conf = q.First();
                        var configfullpath = configroot + conf;
                        var jsstr = Tool.ReadFile(configfullpath);
                        Regex reg = new Regex(@"(?<fields>fields:.*\[\{.+\}\])");
                        var fieldsinfo = reg.Match(jsstr).Groups["fields"].Value;
                        fieldsinfo = fieldsinfo.Replace("fields:", "").Replace(" ", "");
                        Regex regfn = new Regex(@":function[\w\W]*?},");
                        fieldsinfo = regfn.Replace(fieldsinfo, ":##");
                        fieldsinfo = fieldsinfo.Replace("##{", "'',},{").Replace("##","'',");
                        if(fieldsinfo.LastIndexOf(",") == fieldsinfo.Length - 1)
                        {
                            fieldsinfo = fieldsinfo.Remove(fieldsinfo.Length - 1);
                        }
                        return JsonConvert.DeserializeObject<List<sys_field_info>>(fieldsinfo);
                    }
                    else
                    {
                        return new List<sys_field_info>();
                    }
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
                List<string> permis_list = new List<string>();
                StringBuilder menusql = new StringBuilder();
                menusql.Append("select id from mes_menu_entity where routepath = :route");
                StringBuilder roleidsql = new StringBuilder();
                roleidsql.Append("select distinct roleid FROM mes_user_role ta,mes_user_entity tb where ta.userid = tb.id and tb.token = :token ");
                using (var db = new OracleConnection(ConString))
                {
                    var menuids = db.Query<int>(menusql.ToString(), new { route = route });
                    var roleids = db.Query<int>(roleidsql.ToString(), new { token = token });
                    var permis = db.Query<string>("select permis FROM mes_role_menu where menuid in :menuids and roleid in :roleids ", new { menuids = menuids, roleids = roleids });
                    foreach (var item in permis)
                    {
                        var p = JsonConvert.DeserializeObject<sys_menu_permis>(item);
                        if (p.funs.Count > 0)
                        {
                            permis_list.AddRange(p.funs);
                        }
                    }
                    if (permis_list.Distinct().Count() > 0)
                    {
                        var q = db.Query<sys_pagefn_info>("select * FROM mes_menu_entity where pid in :menuid and menutype = '03' and name in :permis ", new { menuid = menuids, permis = permis_list.Distinct() });
                        return q.ToList();
                    }
                    else
                    {
                        //var q = db.Query<sys_pagefn_info>("select * FROM mes_menu_entity where pid in :menuid and menutype = '03' ", new { menuid = menuids });
                        //return q.ToList();
                        return new List<sys_pagefn_info>();
                    }                    
                }
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
                using (var db = new OracleConnection(ConString))
                {
                    list.AddRange(db.Query<sys_route_component>(sql.ToString()));
                    return list;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private string LoadJsScript(string path, params string[] par)
        {
            try
            {
                string fullpath = HttpContext.Current.Server.MapPath(path);
                var script = File.ReadAllText(fullpath);
                foreach (var item in par)
                {
                    script = script.Replace("@@",item);
                }
                return script;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Save_Page_Config(sys_page_config config)
        {
            try
            {
                StringBuilder contents = new StringBuilder();
                StringBuilder fieldcontent = new StringBuilder();
                var js_filename = config.menu.configpath;
                if (!string.IsNullOrEmpty(js_filename))
                {
                    var configroot = HttpContext.Current.Server.MapPath("~/Config/");
                    string configpath = configroot + js_filename;
                    FileInfo fi = new FileInfo(configpath);
                    contents.Append("{");
                    foreach (PropertyInfo p in config.baseconfig.GetType().GetProperties())
                    {
                        var attrs = p.GetCustomAttributes().Where(t => t.TypeId.ToString().Contains("NoCnf"));
                        if (attrs.Count() == 0)
                        {
                            contents.Append($"{p.Name}:{p.GetValue(config.baseconfig).ToString().ToLower()},");
                        }
                    }
                    if (config.baseconfig.isbatoperate)
                    {
                        contents.Append("batoperate:{");
                        if (!string.IsNullOrEmpty(config.baseconfig.import_cnf.addurl))
                        {
                            contents.Append("import_by_add:function(_this, res){" + LoadJsScript("~/Scripts/import_by_add.js", config.baseconfig.import_cnf.addurl) + "},");
                        }
                        if (!string.IsNullOrEmpty(config.baseconfig.import_cnf.replaceurl))
                        {
                            contents.Append("import_by_replace:function(_this, res){" + LoadJsScript("~/Scripts/import_by_replace.js", config.baseconfig.import_cnf.replaceurl) + "},");
                        }
                        if (!string.IsNullOrEmpty(config.baseconfig.import_cnf.zongheurl))
                        {
                            contents.Append("import_by_zh:function(_this, res){" + LoadJsScript("~/Scripts/import_by_zh.js", config.baseconfig.import_cnf.zongheurl) + "},");
                        }
                        contents.Append("export_excel:function(_this){" + LoadJsScript("~/Scripts/export_excel.js") + "},");
                        contents.Append("},");
                    }
                    if (config.baseconfig.isoperate)
                    { 
                        contents.Append("operate_fnlist:[");
                        foreach (var item in config.operate_fnlist)
                        {
                            contents.Append("{");
                            foreach (PropertyInfo p in item.GetType().GetProperties())
                            {
                                var val = p.GetValue(item)?.ToString();
                                if (string.IsNullOrWhiteSpace(val))
                                {
                                    continue;
                                }
                                if (p.Name == "callback")
                                {
                                    
                                    contents.Append($"{p.Name}:{val},");
                                }
                                else
                                {
                                    contents.Append($"{p.Name}:'{val}',");
                                }
                            }
                            contents.Append("},");
                        }
                        contents.Append("],");
                    }
                    contents.Append("pagefuns:{");
                    foreach (var item in config.pagefn)
                    {
                        contents.Append($"{item.fnname}:{item.fnbody},");
                    }
                    contents.Append("},");
                    contents.Append("fields:[");
                    foreach (var item in config.fields)
                    {
                        fieldcontent.Append("{");
                        foreach (PropertyInfo p in item.GetType().GetProperties())
                        {
                            var val = p.GetValue(item)?.ToString().ToLower();
                            if (!string.IsNullOrWhiteSpace(val))
                            {
                                if (p.Name == "url" || p.Name == "method")
                                {
                                    continue;
                                }
                                else if(p.Name == "overflowtooltip" && val == "false")
                                {
                                    continue;
                                }
                                else
                                {
                                    if (p.PropertyType == typeof(bool))
                                    {
                                        fieldcontent.Append($"{p.Name}:{val},");
                                    }
                                    else
                                    {
                                        fieldcontent.Append($"{p.Name}:'{val}',");
                                    }
                                }
                            }
                        }
                        if(!string.IsNullOrWhiteSpace(item.suggest))
                        {
                            if (item.suggest == "true")
                            {
                                fieldcontent.Append($"suggest:true,");
                                fieldcontent.Append($"suggest_fn_name:'suggest_fn',");
                                fieldcontent.Append($"select_fn_name:'select_fn',");
                            }
                        }
                        else if (item.coltype == "list" && !string.IsNullOrWhiteSpace(item.url)) {
                            fieldcontent.Append("inioptionapi:{");
                            fieldcontent.Append($"method:'{item.method}',");
                            fieldcontent.Append($"url:'{item.url}',");
                            fieldcontent.Append("},");
                            fieldcontent.Append($"options:[],");
                        }
                        else if (item.coltype == "list" && string.IsNullOrWhiteSpace(item.url))
                        {
                            fieldcontent.Append($"options:[],");
                        }
                        else if(item.coltype == "bool")
                        {
                            fieldcontent.Append($"activevalue:'Y',");
                            fieldcontent.Append($"inactivevalue:'N',");
                        }
                        fieldcontent.Append("},");
                    }
                    if (fieldcontent.Length > 0)
                    {
                        fieldcontent = fieldcontent.Remove(fieldcontent.Length - 1, 1);
                        contents.Append(fieldcontent);
                    }
                    contents.Append("],");
                    contents.Append("form:{");
                    foreach (var item in config.pageform)
                    {
                        contents.Append($"{item.fieldname}:'{(item.fieldvalue == null ? "" : item.fieldvalue)}',");
                    }
                    contents.Append("isdb:false,");
                    contents.Append("isedit:true");
                    contents.Append("},");
                    foreach (var item in config.pageapis)
                    {
                        contents.Append(item.name + ":" +"{");
                        contents.Append($"url:'{item.url}',");
                        contents.Append($"method:'{item.method}',");
                        contents.Append($"callback:{item.callback},");
                        contents.Append("},");
                    }
                    contents.Append("}");
                    if (!fi.Exists)
                    {
                        var pos = configpath.LastIndexOf("/");
                        if (pos != -1)
                        {
                            var dirpath = configpath;
                            dirpath = configpath.Substring(0, pos);
                            DirectoryInfo dirinfo = new DirectoryInfo(dirpath);
                            if (!dirinfo.Exists)
                            {
                                dirinfo.Create();
                            }
                        }
                        File.WriteAllText(configpath, contents.ToString());
                    }
                    else
                    {
                        fi.CopyTo(configroot + js_filename+".bak");
                        File.WriteAllText(configpath, contents.ToString());
                    }
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<sys_pagefn_info> GetPageBatList(string route, string token)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select id from mes_menu_entity where routepath = :route");
            StringBuilder roleidsql = new StringBuilder();
            roleidsql.Append("select distinct roleid FROM mes_user_role ta,mes_user_entity tb where ta.userid = tb.id and tb.token = :token ");
            try
            {
                List<string> permis_list = new List<string>();
                using (var db = new OracleConnection(ConString))
                {
                    var menuids = db.Query<int>(sql.ToString(), new { route = route });
                    var roleids = db.Query<int>(roleidsql.ToString(), new { token = token });
                    var permis = db.Query<string>("select permis FROM mes_role_menu where menuid in :menuids and roleid in :roleids ", new { menuids = menuids, roleids = roleids });
                    foreach (var item in permis)
                    {
                        var p = JsonConvert.DeserializeObject<sys_menu_permis>(item);
                        if (p.batbtns.Count > 0)
                        {
                            permis_list.AddRange(p.batbtns);
                        }
                    }
                    if (permis_list.Distinct().Count() > 0)
                    {
                        var q = db.Query<sys_pagefn_info>("select * FROM mes_menu_entity where pid in :menuid and menutype = '05' and name in :permis ", new { menuid = menuids, permis = permis_list.Distinct() });
                        return q.ToList();
                    }
                    else
                    {
                        //var q = db.Query<sys_pagefn_info>("select * FROM mes_menu_entity where pid in :menuid and menutype = '03' ", new { menuid = menuids });
                        //return q.ToList();
                        return new List<sys_pagefn_info>();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

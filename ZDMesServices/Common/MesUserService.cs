using Dapper;
using DapperExtensions;
using DapperExtensions.Predicate;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ZDMesInterfaces.Common;
using ZDMesModels;

namespace ZDMesServices.Common
{
    public class MesUserService : BaseDao<mes_user_entity>, IUser
    {
        public MesUserService(string constr):base(constr)
        {

        }

        public bool ChangePwd(string token,string newpwd)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("select count(id) from MES_USER_ENTITY where token = :token");
                    var qty = db.ExecuteScalar<int>(sql.ToString(), new { token = token });
                    if (qty > 0)
                    {
                        var pwd = ZDToolHelper.Tool.Str2MD5(newpwd);
                        var ret = db.Execute("update MES_USER_ENTITY set pwd = :pwd where token = :token", new { token = token, pwd = pwd });
                        return ret > 0;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<mes_menu_entity> Get_User_Menus(string token)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        InitDB(db);
                        var pre = Predicates.Field<mes_user_entity>(t => t.token, Operator.Eq, token);
                        var q = Db.GetList<mes_user_entity>(pre);
                        if (q.Count() > 0)
                        {
                            var user = q.First();
                            var user_role_q = Predicates.Field<mes_user_role>(t => t.userid, Operator.Eq, user.id);
                            var roleids = Db.GetList<mes_user_role>(user_role_q).Select(t => t.roleid);
                            var role_q = Predicates.Field<mes_role_menu>(t => t.roleid, Operator.Eq, roleids);
                            var menuids = Db.GetList<mes_role_menu>(role_q);
                            PredicateGroup pg = new PredicateGroup();
                            pg.Operator = GroupOperator.And;
                            pg.Predicates = new List<IPredicate>();
                            pg.Predicates.Add(Predicates.Field<mes_menu_entity>(t => t.id, Operator.Eq, menuids.Select(t => t.menuid)));
                            pg.Predicates.Add(Predicates.Field<mes_menu_entity>(t => t.status, Operator.Eq, 1));
                            pg.Predicates.Add(Predicates.Field<mes_menu_entity>(t => t.menutype, Operator.Eq, new List<string> { "01", "02" }));
                            var list = Db.GetList<mes_menu_entity>(pg).OrderBy(t => t.seq);
                            var rootlist = list.Where(t => t.pid == 0);
                            foreach (var item in rootlist)
                            {
                                item.menupermission = JsonConvert.DeserializeObject<sys_menu_permis>(menuids.Where(t => t.menuid == item.id).Select(t => t.permis).First());
                                item.children = Get_User_SubMenus(list, menuids, item).ToList();
                                //if (!string.IsNullOrEmpty(item.configpath))
                                //{
                                //    var configfullpath = configroot + item.configpath;
                                //    item.viewconf = ZDToolHelper.Tool.ReadFile(configfullpath);
                                //}
                            }
                            return rootlist;
                        }
                        else
                        {
                            return new List<mes_menu_entity>();
                        }
                    }
                    finally
                    {
                        db.Close();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Db.Dispose();
            }
        }

        private IEnumerable<mes_menu_entity> Get_User_SubMenus(IEnumerable<mes_menu_entity> list,IEnumerable<mes_role_menu> role_menus, mes_menu_entity item)
        {
            try
            {
                //var configroot = HttpContext.Current.Server.MapPath("~/Config/");
                var sublist = list.Where(t => t.pid == item.id);
                foreach (var sitem in sublist)
                {
                    sitem.menupermission = JsonConvert.DeserializeObject<sys_menu_permis>(role_menus.Where(t => t.menuid == sitem.id).Select(t => t.permis).First());
                    sitem.children = Get_User_SubMenus(list, role_menus, sitem).ToList();
                    //if (!string.IsNullOrEmpty(sitem.configpath))
                    //{
                    //    var configfullpath = configroot + sitem.configpath;
                    //    sitem.viewconf = ZDToolHelper.Tool.ReadFile(configfullpath);
                    //}
                }
                return sublist;
            }
            catch (Exception)
            {
                return new List<mes_menu_entity>();
            }
        }

        public IEnumerable<mes_role_entity> Get_User_Roles(string token)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        InitDB(db);
                        var pre = Predicates.Field<mes_user_entity>(t => t.token, Operator.Eq, token);
                        var q = Db.GetList<mes_user_entity>(pre);
                        if (q.Count() > 0)
                        {
                            var user = q.First();
                            var user_role_q = Predicates.Field<mes_user_role>(t => t.userid, Operator.Eq, user.id);
                            var roleids = Db.GetList<mes_user_role>(user_role_q).Select(t => t.roleid);
                            var role_q = Predicates.Field<mes_role_entity>(t => t.id, Operator.Eq, roleids);
                            return Db.GetList<mes_role_entity>(role_q);
                        }
                        else
                        {
                            return new List<mes_role_entity>();
                        }
                    }
                    finally
                    {
                        db.Close();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Db.Dispose();
            }
        }

        public bool ReSetToken(int id)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        InitDB(db);
                        var newtoken = new ZDToolHelper.JWTHelper().CreateToken();
                        var q = Db.GetList<mes_user_entity>(Predicates.Field<mes_user_entity>(t => t.id, Operator.Eq, id));
                        if (q.Count() > 0)
                        {
                            var user = q.First();
                            user.token = newtoken;
                            return Db.Update<mes_user_entity>(user);
                        }
                        else
                        {
                            return false;
                        }
                    }
                    finally
                    {
                        db.Close();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Db.Dispose();
            }
        }

        public bool Save_User_Roles(int userid, List<int> roleids)
        {
            try
            {
                List<dynamic> list = new List<dynamic>();
                foreach (var item in roleids)
                {
                    list.Add(new { userid = userid, roleid = item });
                }
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        db.Open();
                        using (var trans = db.BeginTransaction())
                        {
                            try
                            {
                                db.Execute("delete from mes_user_role where userid in :userid", new { userid = userid }, trans);
                                db.Execute("insert into mes_user_role(userid,roleid) values (:userid,:roleid)", list, trans);
                                trans.Commit();
                                return true;
                            }
                            catch (Exception)
                            {
                                trans.Rollback();
                                throw;
                            }
                        }
                    }
                    finally
                    {
                        db.Close();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public mes_user_entity GetUserByToken(string token)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        InitDB(db);
                        var q = Db.GetList<mes_user_entity>(Predicates.Field<mes_user_entity>(t => t.token, Operator.Eq, token));
                        return q.FirstOrDefault();
                    }
                    finally
                    {
                        db.Close();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Db?.Dispose();
            }
        }
        public sys_userinfo_result GetUserInfo(string token)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        InitDB(db);
                        var q = Db.GetList<mes_user_entity>(Predicates.Field<mes_user_entity>(t => t.token, Operator.Eq, token));
                        if (q.Count() > 0)
                        {
                            string server_path = "http://" + HttpContext.Current.Request.Url.Authority + "/Images/headimg/";
                            mes_user_entity user = q.First();
                            user.headimg = server_path + (string.IsNullOrEmpty(user.headimg) ? "default.jpg" : user.headimg);
                            var user_menus = this.Get_User_Menus(token).ToList();
                            return new sys_userinfo_result()
                            {
                                code = 1,
                                msg = "用户信息获取成功",
                                userinfo = user,
                                user_menus = user_menus
                            };
                        }
                        else
                        {
                            return new sys_userinfo_result()
                            {
                                code = 0,
                                msg = "获取用户信息失败",
                                userinfo = new mes_user_entity(),
                                user_menus = new List<mes_menu_entity>()
                            };
                        }
                    }
                    finally
                    {
                        db.Close();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Db?.Dispose();
            }
        }

        public sys_login_result Login(sys_user user)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    var pwd = ZDToolHelper.Tool.Str2MD5(user.password);
                    StringBuilder sql = new StringBuilder();
                    var isexsit = db.ExecuteScalar<int>("select count(id) from mes_user_entity where code= :code and pwd= :pwd", new { code = user.username, pwd = pwd });
                    if (isexsit > 0)
                    {
                        var token = db.ExecuteScalar<string>("select token from mes_user_entity where code= :code and pwd= :pwd ", new { code = user.username, pwd = pwd });
                        return new sys_login_result()
                        {
                            code = 1,
                            msg = "登录成功",
                            token = token
                        };
                    }
                    else
                    {
                        return new sys_login_result()
                        {
                            code = 0,
                            msg = "用户名或密码错误",
                            token = ""
                        };
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Logout()
        {
            try
            {
                var token = ZDToolHelper.TokenHelper.GetToken;
                ZDToolHelper.JWTHelper jwthelper = new ZDToolHelper.JWTHelper();
                var newtoken = jwthelper.CreateToken();
                using (var db = new OracleConnection(ConString))
                {
                    var ret = db.Execute("update mes_user_entity set token= :newtoken where token  = :token ", new { newtoken = newtoken, token = token });
                    return ret > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override int Add(IEnumerable<mes_user_entity> entitys,out IEnumerable<mes_user_entity> noklist)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        InitDB(db);
                        List<mes_user_entity> errorlist = new List<mes_user_entity>();
                        int okcnt = 0;
                        StringBuilder sql = new StringBuilder();
                        sql.Append("select count(id) FROM mes_user_entity where code = :code");
                        var jwt = new ZDToolHelper.JWTHelper();
                        foreach (var item in entitys)
                        {
                            int qty = db.ExecuteScalar<int>(sql.ToString(), new { code = item.code });
                            if (qty == 0)
                            {
                                item.pwd = ZDToolHelper.Tool.Str2MD5("123456");
                                item.token = jwt.CreateToken();
                                item.headimg = "default.jpg";
                                var ret = Db.Insert<mes_user_entity>(item);
                                if (ret > 0)
                                {
                                    List<mes_user_role> rolelist = new List<mes_user_role>();
                                    foreach (var ritem in item.role)
                                    {
                                        rolelist.Add(new mes_user_role()
                                        {
                                            userid = Convert.ToInt32(ret),
                                            roleid = Convert.ToInt32(ritem)
                                        });
                                    }
                                    Db.Insert<mes_user_role>(rolelist);
                                    okcnt++;
                                }
                            }
                            else
                            {
                                okcnt++;
                                errorlist.Add(item);
                            }
                        }
                        noklist = errorlist;
                        return okcnt;
                    }
                    finally
                    {
                        db.Close();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Db.Dispose();
            }
        }
        public override bool Del(IEnumerable<mes_user_entity> entitys)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        db.Open();
                        using (var transaction = db.BeginTransaction())
                        {
                            try
                            {
                                var p = new { id = entitys.Select(t => t.id) };
                                var usercode = entitys.Select(t => t.code).ToList();
                                var affectedRows1 = db.Execute("delete from mes_user_entity where id in :id", p, transaction: transaction);
                                var affectedRows2 = db.Execute("delete from mes_user_role where userid in :id", p, transaction: transaction);
                                var cnt = db.ExecuteScalar<int>("select count(*) from user_tables where table_name = upper('app_role_user')");
                                if (cnt > 0)
                                {
                                    var affectedRows3 = db.Execute("delete from app_role_user where usercode in :usercode", new { usercode = usercode }, transaction: transaction);
                                }
                                transaction.Commit();
                                return true;
                            }
                            catch (Exception)
                            {
                                transaction.Rollback();
                                throw;
                            }
                        }
                    }
                    finally
                    {
                        db.Close();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public override IEnumerable<mes_user_entity> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("select ta.id,ta.status, ta.code,ta.tel, ta.name, ta.pwd, ta.token, ta.headimg, ta.adduser,ta.addusername, ta.addtime, tb.id as roleid,tb.name as rolename");
                    sql.Append(" from MES_USER_ENTITY ta, (select t2.id,t2.name,t1.userid from mes_user_role t1,mes_role_entity t2 where t1.roleid = t2.id) tb");
                    sql.Append(" where  ta.id = tb.userid(+) ");
                    //sql.Append(" and tb.roleid = tc.id ");
                    StringBuilder sql_cnt = new StringBuilder();
                    sql_cnt.Append("select count(ta.id) ");
                    sql_cnt.Append(" from MES_USER_ENTITY ta, (select t2.id,t2.name,t1.userid from mes_user_role t1,mes_role_entity t2 where t1.roleid = t2.id) tb");
                    sql_cnt.Append(" where  ta.id = tb.userid(+) ");
                    //sql_cnt.Append(" and tb.roleid = tc.id ");
                    if (!string.IsNullOrEmpty(parm.sqlexp))
                    {
                        sql.Append(" and " + parm.sqlexp);
                        sql_cnt.Append(" and " + parm.sqlexp);
                    }
                    if (parm.orderbyexp != null && !string.IsNullOrWhiteSpace(parm.orderbyexp))
                    {
                        sql.Append(parm.orderbyexp);
                    }
                    else
                    {
                        if (parm.default_order_colname != null && !string.IsNullOrEmpty(parm.default_order_colname))
                        {
                            sql.Append($" order by {parm.default_order_colname} desc ");
                        }
                    }
                    var user_role_dic = new Dictionary<int, mes_user_entity>();
                    var list = db.Query<mes_user_entity, mes_role_entity, mes_user_entity>(OraPager(sql.ToString()), (ta, tb) =>
                     {
                         mes_user_entity user = new mes_user_entity();
                         if (!user_role_dic.TryGetValue(ta.id, out user))
                         {
                             user = ta;
                             user.role = new List<dynamic>();
                             user_role_dic.Add(ta.id, user);
                         }
                         if (tb != null)
                         {
                             user.role.Add(tb.roleid);
                         }
                         return user;
                     }, param: parm.sqlparam, splitOn: "roleid").Distinct();
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                    return list;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public IEnumerable<mes_user_entity> GetUserByKey(string key)
        {
            try
            {
                var exp = Predicates.Field<mes_user_entity>(t => t.name, Operator.Like, key);
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        InitDB(db);
                        return Db.GetList<mes_user_entity>(exp);
                    }
                    finally
                    {
                        db.Close();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Db.Dispose();
            }
        }

        public mes_user_entity CurrentUser()
        {
            try
            {
                var token = ZDToolHelper.TokenHelper.GetToken;
                return GetUserByToken(token);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ResetPwd(int id, string pwd)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                var newpwd = ZDToolHelper.Tool.Str2MD5(pwd);
                sql.Append("update MES_USER_ENTITY set pwd = :pwd where id = :id ");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Execute(sql.ToString(), new { id = id, pwd = newpwd }) > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }        
    }
}

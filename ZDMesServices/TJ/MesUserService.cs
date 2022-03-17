using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.Common;
using ZDMesModels;
using Dapper;
using DapperExtensions;
using DapperExtensions.Predicate;
using Newtonsoft.Json;
using System.Web;
namespace ZDMesServices.TJ
{
    public class MesUserService : TJDAO<mes_user_entity>, IUser
    {
        public bool ChangePwd(string token,string newpwd)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<mes_menu_entity> Get_User_Menus(string token)
        {
            try
            {
                var configroot = HttpContext.Current.Server.MapPath("~/Config/");
                var pre = Predicates.Field<mes_user_entity>(t => t.token, Operator.Eq, token);
                var q = Db.GetList<mes_user_entity>(pre);
                if (q.Count() > 0)
                {
                    var user = q.First();
                    var user_role_q = Predicates.Field<mes_user_role>(t => t.userid, Operator.Eq, user.id);
                    var roleids = Db.GetList<mes_user_role>(user_role_q).Select(t => t.roleid);
                    var role_q = Predicates.Field<mes_role_menu>(t => t.roleid, Operator.Eq, roleids);
                    var menuids = DB.GetList<mes_role_menu>(role_q);
                    PredicateGroup pg = new PredicateGroup();
                    pg.Operator = GroupOperator.And;
                    pg.Predicates = new List<IPredicate>();
                    pg.Predicates.Add(Predicates.Field<mes_menu_entity>(t => t.id, Operator.Eq, menuids.Select(t=>t.menuid)));
                    pg.Predicates.Add(Predicates.Field<mes_menu_entity>(t => t.status, Operator.Eq, 1));
                    var list = DB.GetList<mes_menu_entity>(pg).OrderBy(t=>t.seq);
                    var rootlist = list.Where(t => t.pid == 0);
                    foreach (var item in rootlist)
                    {
                        item.menu_permission = JsonConvert.DeserializeObject<sys_menu_permis>(menuids.Where(t => t.menuid == item.id).Select(t => t.permis).First());
                        item.children = Get_User_SubMenus(list, menuids, item).ToList();
                        if (!string.IsNullOrEmpty(item.configpath))
                        {
                            var configfullpath = configroot + item.configpath;
                            item.viewconf = ZDToolHelper.Tool.ReadFile(configfullpath);
                        }
                    }
                    return rootlist;
                }
                else
                {
                    return new List<mes_menu_entity>();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private IEnumerable<mes_menu_entity> Get_User_SubMenus(IEnumerable<mes_menu_entity> list,IEnumerable<mes_role_menu> role_menus, mes_menu_entity item)
        {
            try
            {
                var configroot = HttpContext.Current.Server.MapPath("~/Config/");
                var sublist = list.Where(t => t.pid == item.id);
                foreach (var sitem in sublist)
                {
                    sitem.menu_permission = JsonConvert.DeserializeObject<sys_menu_permis>(role_menus.Where(t => t.menuid == item.id).Select(t => t.permis).First());
                    sitem.children = Get_User_SubMenus(list, role_menus, sitem).ToList();
                    if (!string.IsNullOrEmpty(sitem.configpath))
                    {
                        var configfullpath = configroot + sitem.configpath;
                        sitem.viewconf = ZDToolHelper.Tool.ReadFile(configfullpath);
                    }
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
                var pre = Predicates.Field<mes_user_entity>(t => t.token, Operator.Eq, token);
                var q = Db.GetList<mes_user_entity>(pre);
                if (q.Count() > 0)
                {
                    var user = q.First();
                    var user_role_q = Predicates.Field<mes_user_role>(t => t.userid, Operator.Eq, user.id);
                    var roleids = Db.GetList<mes_user_role>(user_role_q).Select(t => t.roleid);
                    var role_q = Predicates.Field<mes_role_entity>(t => t.id, Operator.Eq, roleids);
                    return DB.GetList<mes_role_entity>(role_q);
                }
                else
                {
                    return new List<mes_role_entity>();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ReSetToken(string token)
        {
            throw new NotImplementedException();
        }

        public bool Save_User_Roles(int userid, List<mes_role_entity> roles)
        {
            try
            {

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public sys_userinfo_result GetUserInfo(string token)
        {
            try
            {
                var q = DB.GetList<mes_user_entity>(Predicates.Field<mes_user_entity>(t => t.token, Operator.Eq, token));
                if (q.Count() > 0)
                {
                    mes_user_entity user = q.First();
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
            catch (Exception)
            {

                throw;
            }
        }

        public sys_login_result Login(sys_user user)
        {
            try
            {

                var pwd = ZDToolHelper.Tool.Str2MD5(user.password);
                StringBuilder sql = new StringBuilder();
                var isexsit = Db.Connection.ExecuteScalar<int>("select count(id) from sys_user where code= :code and pwd= :pwd", new { code = user.username, pwd = pwd });
                if (isexsit > 0)
                {
                    var token = Db.Connection.ExecuteScalar<string>("select token from sys_user where code= :code and pwd= :pwd ", new { code = user.username, pwd = pwd });
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
            catch (Exception)
            {

                throw;
            }
        }

        public sys_result Logout(string token)
        {
            try
            {
                return new sys_result()
                {
                    code = 1,
                    msg = "成功退出系统"
                };
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

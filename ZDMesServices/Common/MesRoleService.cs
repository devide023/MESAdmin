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
namespace ZDMesServices.Common
{
    public class MesRoleService: BaseDao<mes_role_entity>,IRole
    {
        public MesRoleService(string constr):base(constr)
        {

        }

        public IEnumerable<mes_menu_entity> Get_Role_Menus(int roleid)
        {
            try
            {
                var pre = Predicates.Field<mes_role_menu>(t => t.roleid, Operator.Eq, roleid);
                var q = DB.GetList<mes_role_menu>(pre);
                if (q.Count() > 0)
                {
                    var menuids = q.Select(t => t.menuid).Distinct();
                    var pre1 = Predicates.Field<mes_menu_entity>(t => t.id, Operator.Eq, menuids);
                    return DB.GetList<mes_menu_entity>(pre1);
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

        public IEnumerable<mes_user_entity> Get_Role_Users(int roleid)
        {
            try
            {
                var pre = Predicates.Field<mes_user_role>(t => t.roleid, Operator.Eq, roleid);
                var q = DB.GetList<mes_user_role>(pre);
                if (q.Count() > 0)
                {
                    var userids = q.Select(t => t.userid).Distinct();
                    var pre1 = Predicates.Field<mes_user_entity>(t => t.id, Operator.Eq, userids);
                    return DB.GetList<mes_user_entity>(pre1);
                }
                else
                {
                    return new List<mes_user_entity>();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Save_Role_Menus(int roleid, List<mes_menu_entity> menus)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" declare \n");
                sql.Append(" begin \n");
                sql.Append(" delete from mes_role_menu where roleid in :roleid; \n");
                sql.Append(" insert into mes_role_menu(roleid,menuid,permis) values (:roleid,:menuid,:permis); \n");
                sql.Append(" commit; \n");
                sql.Append(" end;\n");
                List<dynamic> list = new List<dynamic>();
                foreach (var item in menus)
                {
                    list.Add(new { roleid = roleid, menuid = item.id, permis = JsonConvert.SerializeObject(item.menupermission) });
                }
                return DB.Connection.Execute(sql.ToString(), list) > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Save_Role_Users(int roleid, List<int> userids)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" declare \n");
                sql.Append(" begin \n");
                sql.Append(" delete from mes_user_role where roleid in :roleid; \n");
                sql.Append(" insert into mes_user_role(userid,roleid) values (:userid,:roleid); \n");
                sql.Append(" commit; \n");
                sql.Append(" end;\n");
                List<dynamic> list = new List<dynamic>();
                foreach (var item in userids)
                {
                    list.Add(new { roleid = roleid, userid = item });
                }
                return DB.Connection.Execute(sql.ToString(), list) > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

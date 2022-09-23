using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.LBJ.App;
using ZDMesModels.LBJ;
using Oracle.ManagedDataAccess.Client;
using Dapper;
namespace ZDMesServices.LBJ.App
{
    public class AppRoleService: BaseDao<app_role>,IApp
    {
        public AppRoleService(string constr) : base(constr)
        {

        }

        public override bool Del(IEnumerable<app_role> entitys)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        db.Open();
                        using (var trans = db.BeginTransaction())
                        {
                            try
                            {
                                var roleids = entitys.Select(t => t.id);
                                db.Execute("delete from app_role where id in :roleid", new { roleid = roleids }, trans);
                                db.Execute("delete FROM app_role_menu where roleid in :roleid", new { roleid = roleids }, trans);
                                db.Execute("delete FROM app_role_user where roleid in :roleid", new { roleid = roleids }, trans);
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

        public IEnumerable<app_user> Get_All_Users()
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select code, name, tel ");
                sql.Append(" FROM   mes_user_entity ");
                sql.Append(" where  status = 1 ");
                sql.Append(" union all ");
                sql.Append(" select user_code, user_name, tel ");
                sql.Append(" FROM   zxjc_ryxx ");
                sql.Append(" where  scbz = 'N'");
                using (var db = new  OracleConnection(ConString))
                {
                   return db.Query<app_user>(sql.ToString());
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<App_Menu> Get_App_Menus()
        {
            try
            {
                List<App_Menu> result = new List<App_Menu>();
                StringBuilder sql = new StringBuilder();
                sql.Append("select * FROM app_menu where status = 1 order by pid asc,seq asc");
                using (var db = new OracleConnection(ConString))
                {
                    var list = db.Query<App_Menu>(sql.ToString()).ToList();
                    foreach (var item in list.Where(t=>t.pid == 0))
                    {
                        item.children = Get_SubMenu(list, item.id).ToList();
                        result.Add(item);
                    }
                    return result;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private IEnumerable<App_Menu> Get_SubMenu(List<App_Menu> list,int pid)
        {
            List<App_Menu> result = new List<App_Menu>();
            foreach (var item in list.Where(t => t.pid == pid))
            {
                item.children = Get_SubMenu(list, item.id).ToList();
                result.Add(item);
            }
            return result;
        }

        public IEnumerable<App_Menu> Get_Role_Menus(int roleid)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select ta.* ");
                sql.Append(" from app_menu ta, app_role_menu tb");
                sql.Append(" where  ta.id = tb.menuid");
                sql.Append(" and    tb.roleid = :roleid ");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<App_Menu>(sql.ToString(), new { roleid = roleid });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<app_user> Get_Role_Users(int roleid)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select distinct t1.* ");
                sql.Append(" FROM(select code, name, tel ");
                sql.Append("          FROM   mes_user_entity ");
                sql.Append("          union all ");
                sql.Append("          select user_code, user_name, tel ");
                sql.Append("          FROM   zxjc_ryxx) t1, (select tel ");
                sql.Append("          FROM   app_role_user ");
                sql.Append("          where  roleid = :roleid ) t2 ");
                sql.Append(" where  t1.tel = t2.tel ");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<app_user>(sql.ToString(),new {roleid = roleid});
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Save_Role_Menus(form_app_role_menu form)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        db.Open();
                        using (var trans = db.BeginTransaction())
                        {
                            try
                            {
                                db.Execute("delete FROM app_role_menu where roleid = :roleid", new { roleid = form.roleid }, trans);
                                foreach (var item in form.menuids)
                                {
                                    db.Execute("insert into app_role_menu (roleid, menuid) values (:roleid, :menuid)", new { roleid = form.roleid, menuid = item }, trans);
                                }
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

        public bool Save_Role_Users(form_app_role_user form)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        db.Open();
                        using (var trans = db.BeginTransaction())
                        {
                            try
                            {
                                db.Execute("delete FROM app_role_user where roleid=:roleid",new { roleid = form.roleid}, trans);
                                foreach (var item in form.users)
                                {
                                    if (!string.IsNullOrEmpty(item.tel))
                                    {
                                        db.Execute("insert into app_role_user (roleid, tel,usercode) values (:roleid, :tel,:usercode)", new { roleid = form.roleid, tel = item.tel, usercode = item.code }, trans);
                                    }
                                }
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
    }
}

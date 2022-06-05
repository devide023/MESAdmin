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
using Oracle.ManagedDataAccess.Client;
namespace ZDMesServices.Common
{
    public class MesRoleService: BaseDao<mes_role_entity>,IRole
    {
        public MesRoleService(string constr):base(constr)
        {

        }

        public IEnumerable<mes_role_entity> All()
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        InitDB(db);
                        return Db.GetList<mes_role_entity>();
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

        public override bool Del(IEnumerable<mes_role_entity> entitys)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        db.Open();
                        InitDB(db);
                        using (var trans = db.BeginTransaction())
                        {
                            try
                            {
                                foreach (var item in entitys)
                                {
                                    Db.Delete<mes_role_entity>(Predicates.Field<mes_role_entity>(t => t.id, Operator.Eq, item.id), trans);
                                    Db.Delete<mes_role_menu>(Predicates.Field<mes_role_menu>(t => t.roleid, Operator.Eq, item.id), trans);
                                    Db.Delete<mes_user_role>(Predicates.Field<mes_user_role>(t => t.roleid, Operator.Eq, item.id), trans);
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
            finally
            {
                Db.Dispose();
            }
        }

        public bool Edit_Role_Menus(sys_role_form form)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        db.Open();
                        InitDB(db);
                        using (var trans = db.BeginTransaction())
                        {
                            try
                            {
                                if (form.mes_role_entity.id > 0)
                                {
                                    Db.Update<mes_role_entity>(form.mes_role_entity, trans);
                                    var q = Predicates.Field<mes_role_menu>(t => t.roleid, Operator.Eq, form.mes_role_entity.id);
                                    Db.Delete<mes_role_menu>(q, trans);
                                    foreach (var menu in form.permission)
                                    {
                                        mes_role_menu role_menu = new mes_role_menu();
                                        role_menu.menuid = menu.id;
                                        role_menu.roleid = form.mes_role_entity.id;
                                        if (menu.menutype == "02")
                                        {
                                            var ids = form.permission.Where(t => t.pid == menu.id).Select(t => t.id).ToList();
                                            var funs = new List<string>();
                                            if (ids.Count > 0)
                                            {
                                                funs = Db.GetList<mes_menu_entity>(Predicates.Field<mes_menu_entity>(t => t.id, Operator.Eq, ids)).Select(t => t.name).ToList();
                                            }
                                            var editids = form.editfields.Where(t => t.pid == menu.id).Select(t => t.id).ToList();
                                            var editfields = new List<string>();
                                            if (editids.Count > 0)
                                            {
                                                editfields = Db.GetList<mes_menu_entity>(Predicates.Field<mes_menu_entity>(t => t.id, Operator.Eq, editids)).Select(t => t.name).ToList();
                                            }
                                            var hideids = form.hidefields.Where(t => t.pid == menu.id).Select(t => t.id).ToList();
                                            var hidefields = new List<string>();
                                            if (hideids.Count > 0)
                                            {
                                                hidefields = Db.GetList<mes_menu_entity>(Predicates.Field<mes_menu_entity>(t => t.id, Operator.Eq, hideids)).Select(t => t.name).ToList();
                                            }
                                            var permis = JsonConvert.SerializeObject(new sys_menu_permis()
                                            {
                                                editfields = editfields,
                                                hidefields = hidefields,
                                                funs = funs
                                            });
                                            role_menu.permis = permis;
                                        }
                                        else
                                        {
                                            role_menu.permis = JsonConvert.SerializeObject(new sys_menu_permis());
                                        }
                                        Db.Insert<mes_role_menu>(role_menu, trans);
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
            finally
            {
                Db.Dispose();
            }
        }

        public IEnumerable<mes_menu_entity> Get_Role_Edit_Fields(int roleid)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        InitDB(db);
                        var p = Predicates.Field<mes_role_menu>(t => t.roleid, Operator.Eq, roleid);
                        var list = Db.GetList<mes_role_menu>(p);

                        List<mes_menu_entity> editlist = new List<mes_menu_entity>();
                        foreach (var item in list)
                        {
                            var menupermis = JsonConvert.DeserializeObject<sys_menu_permis>(item.permis);
                            if (menupermis.editfields.Count > 0)
                            {
                                PredicateGroup pg = new PredicateGroup()
                                {
                                    Operator = GroupOperator.And,
                                    Predicates = new List<IPredicate>()
                                };
                                pg.Predicates.Add(Predicates.Field<mes_menu_entity>(t => t.pid, Operator.Eq, item.menuid));
                                pg.Predicates.Add(Predicates.Field<mes_menu_entity>(t => t.menutype, Operator.Eq, "04"));
                                pg.Predicates.Add(Predicates.Field<mes_menu_entity>(t => t.name, Operator.Eq, menupermis.editfields));
                                editlist.AddRange(Db.GetList<mes_menu_entity>(pg));
                            }
                        }
                        return editlist;
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

        public IEnumerable<mes_menu_entity> Get_Role_Hide_Fields(int roleid)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        InitDB(db);
                        var p = Predicates.Field<mes_role_menu>(t => t.roleid, Operator.Eq, roleid);
                        var list = Db.GetList<mes_role_menu>(p);

                        List<mes_menu_entity> hidelist = new List<mes_menu_entity>();
                        foreach (var item in list)
                        {
                            var menupermis = JsonConvert.DeserializeObject<sys_menu_permis>(item.permis);
                            if (menupermis.hidefields.Count > 0)
                            {
                                PredicateGroup pg = new PredicateGroup()
                                {
                                    Operator = GroupOperator.And,
                                    Predicates = new List<IPredicate>()
                                };
                                pg.Predicates.Add(Predicates.Field<mes_menu_entity>(t => t.pid, Operator.Eq, item.menuid));
                                pg.Predicates.Add(Predicates.Field<mes_menu_entity>(t => t.menutype, Operator.Eq, "04"));
                                pg.Predicates.Add(Predicates.Field<mes_menu_entity>(t => t.name, Operator.Eq, menupermis.hidefields));
                                hidelist.AddRange(Db.GetList<mes_menu_entity>(pg));
                            }
                        }
                        return hidelist;
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

        public IEnumerable<mes_menu_entity> Get_Role_Menus(int roleid)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        InitDB(db);
                        var pre = Predicates.Field<mes_role_menu>(t => t.roleid, Operator.Eq, roleid);
                        var q = Db.GetList<mes_role_menu>(pre);
                        if (q.Count() > 0)
                        {
                            var menuids = q.Select(t => t.menuid).Distinct();
                            var pre1 = Predicates.Field<mes_menu_entity>(t => t.id, Operator.Eq, menuids);
                            return Db.GetList<mes_menu_entity>(pre1);
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

        public IEnumerable<mes_user_entity> Get_Role_Users(int roleid)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        InitDB(db);
                        var pre = Predicates.Field<mes_user_role>(t => t.roleid, Operator.Eq, roleid);
                        var q = Db.GetList<mes_user_role>(pre);
                        if (q.Count() > 0)
                        {
                            var userids = q.Select(t => t.userid).Distinct();
                            var pre1 = Predicates.Field<mes_user_entity>(t => t.id, Operator.Eq, userids);
                            return Db.GetList<mes_user_entity>(pre1);
                        }
                        else
                        {
                            return new List<mes_user_entity>();
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

        public bool Save_Role_Menus(sys_role_form form)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        db.Open();
                        InitDB(db);
                        using (var trans = db.BeginTransaction())
                        {
                            try
                            {
                                var roleid = Db.Insert<mes_role_entity>(form.mes_role_entity, trans);
                                var q = Predicates.Field<mes_role_menu>(t => t.roleid, Operator.Eq, (int)roleid);
                                Db.Delete<mes_role_menu>(q, trans);
                                foreach (var menu in form.permission)
                                {
                                    mes_role_menu role_menu = new mes_role_menu();
                                    role_menu.menuid = menu.id;
                                    role_menu.roleid = roleid;
                                    if (menu.menutype == "02")
                                    {
                                        var ids = form.permission.Where(t => t.pid == menu.id).Select(t => t.id).ToList();
                                        var funs = new List<string>();
                                        if (ids.Count > 0)
                                        {
                                            funs = Db.GetList<mes_menu_entity>(Predicates.Field<mes_menu_entity>(t => t.id, Operator.Eq, ids)).Select(t => t.name).ToList();
                                        }
                                        var editids = form.editfields.Where(t => t.pid == menu.id).Select(t => t.id).ToList();
                                        var editfields = new List<string>();
                                        if (editids.Count > 0)
                                        {
                                            editfields = Db.GetList<mes_menu_entity>(Predicates.Field<mes_menu_entity>(t => t.id, Operator.Eq, editids)).Select(t => t.name).ToList();
                                        }
                                        var hideids = form.hidefields.Where(t => t.pid == menu.id).Select(t => t.id).ToList();
                                        var hidefields = new List<string>();
                                        if (hideids.Count > 0)
                                        {
                                            hidefields = Db.GetList<mes_menu_entity>(Predicates.Field<mes_menu_entity>(t => t.id, Operator.Eq, hideids)).Select(t => t.name).ToList();
                                        }
                                        var permis = JsonConvert.SerializeObject(new sys_menu_permis()
                                        {
                                            editfields = editfields,
                                            hidefields = hidefields,
                                            funs = funs
                                        });
                                        role_menu.permis = permis;
                                    }
                                    else
                                    {
                                        role_menu.permis = JsonConvert.SerializeObject(new sys_menu_permis());
                                    }
                                    Db.Insert<mes_role_menu>(role_menu, trans);
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
            finally
            {
                Db.Dispose();
            }
        }

        public bool Save_Role_Users(sys_role_user_form form)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        db.Open();
                        InitDB(db);
                        using (var trans = db.BeginTransaction())
                        {
                            try
                            {
                                List<mes_user_role> data = new List<mes_user_role>();
                                foreach (var item in form.userid)
                                {
                                    data.Add(new mes_user_role()
                                    {
                                        roleid = form.roleid,
                                        userid = item
                                    });
                                }
                                var exp1 = Predicates.Field<mes_user_role>(t => t.roleid, Operator.Eq, form.roleid);
                                Db.Delete<mes_user_role>(exp1, trans);
                                Db.Insert<mes_user_role>(data, trans);
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
            finally
            {
                Db.Dispose();
            }
        }
    }
}

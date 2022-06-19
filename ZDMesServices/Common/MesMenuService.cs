using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.Common;
using ZDMesModels;
using DapperExtensions;
using DapperExtensions.Predicate;
using Dapper;
using Oracle.ManagedDataAccess.Client;

namespace ZDMesServices.Common
{
    public class MesMenuService : BaseDao<mes_menu_entity>, IMenu
    {
        public MesMenuService(string constr) : base(constr)
        {

        }

        public override int Add(IEnumerable<mes_menu_entity> entitys)
        {
            try
            {
                List<mes_menu_entity> oklist = new List<mes_menu_entity>();
                StringBuilder sql = new StringBuilder();
                sql.Append("select count(id) FROM mes_menu_entity where menutype = '02' and routepath = :routepath");
                StringBuilder sqlfun = new StringBuilder();
                sqlfun.Append("select count(id) FROM mes_menu_entity where pid = :pid and menutype = '03' and name = :name and fnname = :fnname");
                StringBuilder sqlcol = new StringBuilder();
                sqlcol.Append("select count(id) FROM mes_menu_entity where pid=:pid and menutype = '04' and name = :name");
                var pagelist = entitys.Where(t => t.menutype == "02");
                var funlist = entitys.Where(t => t.menutype == "03");
                var collist = entitys.Where(t => t.menutype == "04");
                using (var db = new  OracleConnection(ConString))
                {
                    try
                    {
                        InitDB(db);
                        foreach (var item in pagelist)
                        {
                            var cnt = db.ExecuteScalar<int>(sql.ToString(), new { routepath = item.routepath });
                            if (cnt == 0)
                            {
                                Db.Insert<mes_menu_entity>(item);
                                oklist.Add(item);
                            }
                        }
                        foreach (var item in funlist)
                        {
                            var cnt = db.ExecuteScalar<int>(sqlfun.ToString(), new { pid = item.pid , name = item.name, fnname = item.fnname });
                            if (cnt == 0)
                            {
                                Db.Insert<mes_menu_entity>(item);
                                oklist.Add(item);
                            }
                        }
                        foreach (var item in collist)
                        {
                            var cnt = db.ExecuteScalar<int>(sqlcol.ToString(), new { pid = item.pid, name = item.name });
                            if (cnt == 0)
                            {
                                Db.Insert<mes_menu_entity>(item);
                                oklist.Add(item);
                            }
                        }
                        return oklist.Count;
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

        //public override bool Modify(IEnumerable<mes_menu_entity> entitys)
        //{
        //    List<mes_menu_entity> oklist = new List<mes_menu_entity>();
        //    StringBuilder sql = new StringBuilder();
        //    sql.Append("update mes_menu_entity set code = :code,name=:name,btntxt=:btntxt,menutype=:menutype,fnname=:fnname,btntype=:btntype,icon=:icon,routepath=:routepath,viewpath=:viewpath,seq = :seq,configpath=:configpath,componentname=:componentname where id = :id");
        //    using (var db = new OracleConnection(ConString))
        //    {
        //        foreach (var item in entitys)
        //        {

        //           int ret = db.Execute(sql.ToString(), item);
        //            if (ret > 0)
        //            {
        //                oklist.Add(item);
        //            }
        //        }
        //        return oklist.Count == entitys.Count();
        //    }
        //}

        public override IEnumerable<mes_menu_entity> GetList(sys_page parm, out int resultcount)
        {
            using (var db = new OracleConnection(ConString))
            {
                InitDB(db);
                try
                {
                    var rootlist = new List<mes_menu_entity>();
                    var list = Db.GetList<mes_menu_entity>(Predicates.Field<mes_menu_entity>(t => t.pid, Operator.Eq, 0));
                    resultcount = list.Count();
                    foreach (var item in list.Where(t => t.pid == 0).OrderBy(t => t.id).ThenBy(t => t.seq))
                    {
                        item.children = GetSubList(item.id).ToList();
                        //item.hasChildren = list.Where(t => t.pid == item.id).Count() > 0;
                        rootlist.Add(item);
                    }
                    return rootlist;
                }
                finally
                {
                    db.Close();
                    Db.Dispose();
                } 
            }
        }

        private IEnumerable<mes_menu_entity> GetSubList(int pid)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        InitDB(db);
                        var subs = new List<mes_menu_entity>();
                        var q = Predicates.Field<mes_menu_entity>(t => t.pid, Operator.Eq, pid);
                        var sublist = Db.GetList<mes_menu_entity>(q).OrderBy(t => t.pid).ThenBy(t => t.seq);
                        foreach (var item in sublist)
                        {
                            item.children = GetSubList(item.id).ToList();
                            //item.hasChildren = list.Where(t => t.pid == item.id).Count() > 0;
                            subs.Add(item);
                        }
                        return subs;
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

        public IEnumerable<mes_role_entity> Get_Menu_Roles(int menuid)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        InitDB(db);
                        var pre = Predicates.Field<mes_role_menu>(t => t.menuid, Operator.Eq, menuid);
                        var q = Db.GetList<mes_role_menu>(pre);
                        if (q.Count() > 0)
                        {
                            var pre1 = Predicates.Field<mes_role_entity>(t => t.id, Operator.Eq, q.Select(t => t.roleid));
                            var list = Db.GetList<mes_role_entity>(pre1);
                            return list;
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

        public IEnumerable<mes_user_entity> Get_Menu_Users(int menuid)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        InitDB(db);
                        var pre = Predicates.Field<mes_role_menu>(t => t.menuid, Operator.Eq, menuid);
                        var q = Db.GetList<mes_role_menu>(pre);
                        if (q.Count() > 0)
                        {
                            var pre1 = Predicates.Field<mes_user_role>(t => t.roleid, Operator.Eq, q.Select(t => t.roleid));
                            var userids = Db.GetList<mes_user_role>(pre1).Select(t => t.userid).Distinct();
                            var pre2 = Predicates.Field<mes_user_entity>(t => t.id, Operator.Eq, userids);
                            return Db.GetList<mes_user_entity>(pre2);
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

        public int Get_MenuMaxCode(int pid)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        InitDB(db);
                        var sub = Db.GetList<mes_menu_entity>(Predicates.Field<mes_menu_entity>(t => t.pid, Operator.Eq, pid));
                        return sub.Count();
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

        public IEnumerable<mes_menu_entity> Get_MenuTree()
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        InitDB(db);
                        var root = Db.GetList<mes_menu_entity>(Predicates.Field<mes_menu_entity>(t => t.pid, Operator.Eq, 0)).OrderBy(t => t.id).ThenBy(t => t.seq);
                        foreach (var item in root)
                        {
                            item.children = Get_SubMenu(item.id).ToList();
                        }
                        return root;
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

        private IEnumerable<mes_menu_entity> Get_SubMenu(int pid)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        InitDB(db);
                        PredicateGroup pg = new PredicateGroup()
                        {
                            Operator = GroupOperator.And,
                            Predicates = new List<IPredicate>()
                        };
                        pg.Predicates.Add(Predicates.Field<mes_menu_entity>(t => t.pid, Operator.Eq, pid));
                        pg.Predicates.Add(Predicates.Field<mes_menu_entity>(t => t.menutype, Operator.Eq, new List<string>() { "01", "02", "03" }));
                        var sub = Db.GetList<mes_menu_entity>(pg).OrderBy(t => t.seq);
                        foreach (var item in sub)
                        {
                            if (item.menutype == "03")
                            {
                                item.name = item.btntxt;
                            }
                            item.children = Get_SubMenu(item.id).ToList();
                        }
                        return sub;
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

        public IEnumerable<mes_menu_entity> Get_ColsTree()
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        InitDB(db);
                        var root = Db.GetList<mes_menu_entity>(Predicates.Field<mes_menu_entity>(t => t.pid, Operator.Eq, 0)).OrderBy(t => t.id).ThenBy(t => t.seq);
                        foreach (var item in root)
                        {
                            item.children = Get_SubTree(item.id).ToList();
                        }
                        return root;
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

        private IEnumerable<mes_menu_entity> Get_SubTree(int pid)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        InitDB(db);
                        PredicateGroup pg = new PredicateGroup()
                        {
                            Operator = GroupOperator.And,
                            Predicates = new List<IPredicate>()
                        };
                        pg.Predicates.Add(Predicates.Field<mes_menu_entity>(t => t.pid, Operator.Eq, pid));
                        pg.Predicates.Add(Predicates.Field<mes_menu_entity>(t => t.menutype, Operator.Eq, new List<string>() { "01", "02", "04" }));
                        var sub = Db.GetList<mes_menu_entity>(pg).OrderBy(t => t.seq);
                        foreach (var item in sub)
                        {
                            if (item.menutype == "04")
                            {
                                item.name = item.btntxt;
                            }
                            item.children = Get_SubTree(item.id).ToList();
                        }
                        return sub;
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

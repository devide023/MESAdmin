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
namespace ZDMesServices.Common
{
    public class MesMenuService : BaseDao<mes_menu_entity>,IMenu
    {
        public MesMenuService(string constr):base(constr)
        {

        }

        public override IEnumerable<mes_menu_entity> GetList(sys_page parm, out int resultcount)
        {
            int totalcnt = 0;
            var rootlist = new List<mes_menu_entity>();
            var list = base.GetList(parm, out totalcnt);
            resultcount = list.Where(t => t.pid == 0).Count();
            foreach (var item in list.Where(t=>t.pid == 0).OrderBy(t=>t.id).ThenBy(t=>t.seq))
            {
                item.children = GetSubList(item.id).ToList();
                //item.hasChildren = list.Where(t => t.pid == item.id).Count() > 0;
                rootlist.Add(item);
            }
            return rootlist;
        }

        private IEnumerable<mes_menu_entity> GetSubList(int pid)
        {
            try
            {
                var subs = new List<mes_menu_entity>();
                var q = Predicates.Field<mes_menu_entity>(t => t.pid, Operator.Eq, pid);
                var sublist = Db.GetList<mes_menu_entity>(q).OrderBy(t => t.pid).ThenBy(t=>t.seq);
                foreach (var item in sublist)
                {
                    item.children = GetSubList(item.id).ToList();
                    //item.hasChildren = list.Where(t => t.pid == item.id).Count() > 0;
                    subs.Add(item);
                }
                return subs;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<mes_role_entity> Get_Menu_Roles(int menuid)
        {
            try
            {
                var pre = Predicates.Field<mes_role_menu>(t => t.menuid, Operator.Eq, menuid);
                var q = DB.GetList<mes_role_menu>(pre);
                if (q.Count() > 0)
                {
                    var pre1 = Predicates.Field<mes_role_entity>(t => t.id, Operator.Eq, q.Select(t => t.roleid));
                    var list = DB.GetList<mes_role_entity>(pre1);
                    return list;
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

        public IEnumerable<mes_user_entity> Get_Menu_Users(int menuid)
        {
            try
            {
                var pre = Predicates.Field<mes_role_menu>(t => t.menuid, Operator.Eq, menuid);
                var q = DB.GetList<mes_role_menu>(pre);
                if (q.Count() > 0)
                {
                    var pre1 = Predicates.Field<mes_user_role>(t => t.roleid, Operator.Eq, q.Select(t=>t.roleid));
                    var userids = DB.GetList<mes_user_role>(pre1).Select(t => t.userid).Distinct();
                    var pre2 = Predicates.Field<mes_user_entity>(t => t.id, Operator.Eq, userids);
                    return Db.GetList<mes_user_entity>(pre2);
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

        public int Get_MenuMaxCode(int pid)
        {
            try
            {
                var sub = DB.GetList<mes_menu_entity>(Predicates.Field<mes_menu_entity>(t => t.pid, Operator.Eq, pid));
                return sub.Count();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<mes_menu_entity> Get_MenuTree()
        {
            try
            {
               var root = Db.GetList<mes_menu_entity>(Predicates.Field<mes_menu_entity>(t => t.pid, Operator.Eq, 0)).OrderBy(t=>t.id).ThenBy(t=>t.seq);
                foreach (var item in root)
                {
                    item.children = Get_SubMenu(item.id).ToList();
                }
                return root;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private IEnumerable<mes_menu_entity> Get_SubMenu(int pid)
        {
            try
            {
                PredicateGroup pg = new PredicateGroup()
                {
                    Operator = GroupOperator.And,
                    Predicates = new List<IPredicate>()
                };
                pg.Predicates.Add(Predicates.Field<mes_menu_entity>(t => t.pid, Operator.Eq, pid));
                pg.Predicates.Add(Predicates.Field<mes_menu_entity>(t => t.menutype, Operator.Eq, new List<string>() {"01","02","03"}));
                var sub = DB.GetList<mes_menu_entity>(pg).OrderBy(t=>t.seq);
                foreach (var item in sub)
                {
                    if (item.menutype == "03")
                    {
                        item.name = item.btntxt;
                    }
                    item.children= Get_SubMenu(item.id).ToList();
                }
                return sub;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<mes_menu_entity> Get_ColsTree()
        {
            try
            {
                var root = Db.GetList<mes_menu_entity>(Predicates.Field<mes_menu_entity>(t => t.pid, Operator.Eq, 0)).OrderBy(t => t.id).ThenBy(t => t.seq);
                foreach (var item in root)
                {
                    item.children = Get_SubTree(item.id).ToList();
                }
                return root;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private IEnumerable<mes_menu_entity> Get_SubTree(int pid)
        {
            try
            {
                PredicateGroup pg = new PredicateGroup()
                {
                    Operator = GroupOperator.And,
                    Predicates = new List<IPredicate>()
                };
                pg.Predicates.Add(Predicates.Field<mes_menu_entity>(t => t.pid, Operator.Eq, pid));
                pg.Predicates.Add(Predicates.Field<mes_menu_entity>(t => t.menutype, Operator.Eq, new List<string>() { "01", "02", "04" }));
                var sub = DB.GetList<mes_menu_entity>(pg).OrderBy(t => t.seq);
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
            catch (Exception)
            {

                throw;
            }
        }
    }
}

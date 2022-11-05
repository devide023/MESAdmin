using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ;
using ZDMesModels.LBJ;
using DapperExtensions;
using DapperExtensions.Predicate;
using ZDMesModels;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Dapper;
using Autofac.Extras.DynamicProxy;
using ZDMesInterceptor.LBJ;

namespace ZDMesServices.LBJ.BaseInfo
{
    public class BaseInfoService : OracleBaseFixture, IBaseInfo
    {
        public BaseInfoService(string constr):base(constr)
        {

        }

        public IEnumerable<base_dbxx> GetDbInfo()
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        InitDB(db);
                        return Db.GetList<base_dbxx>();
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

        public IEnumerable<base_dbxx> Get_UnUseDbList()
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select ta.dbmc,ta.dbh,ta.dblx,ta.dbzt,ta.bz from ( ");
                sql.Append(" select t.*, (select count(*) from BASE_DBRJZX where dbh = t.dbh) qty FROM base_dbxx t ");
                sql.Append(" ) ta where ta.qty = 0");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<base_dbxx>(sql.ToString());
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<base_dbxx> UnUse_DbRj_Tree()
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                
                //sql.Append("select t1.*, t2.id, t2.rjid, t2.djlx ");
                //sql.Append(" FROM(select ta.dbmc, ta.dbh, ta.dblx, ta.dbzt, ta.bz");
                //sql.Append("          from(select t.*, (select count(*) from BASE_DBRJZX where dbh = t.dbh) qty");
                //sql.Append("                   FROM   base_dbxx t) ta");
                //sql.Append("          where  ta.qty = 0) t1, base_dbrjgx t2");
                //sql.Append(" where  t1.dbh = t2.dbh");
                
                
                sql.Append("select bb.dbmc, bb.dbh, bb.dblx, aa.id, aa.rjid, aa.djlx ");
                sql.Append(" FROM(select t1.id, t1.djlx, t1.rjid, t1.dbh FROM base_dbrjgx t1) aa, base_dbxx bb ");
                sql.Append(" where aa.dbh = bb.dbh ");

                //sql.Append("select bb.dbmc, bb.dbh, bb.dblx, aa.id, aa.rjid, aa.djlx  ");
                //sql.Append(" FROM(select t1.id, t1.djlx, t1.rjid, t1.dbh ");
                //sql.Append("          FROM(select ta.*, tb.id as gc ");
                //sql.Append("                   FROM   base_dbrjgx ta, base_dbrjzx tb ");
                //sql.Append("                   where  ta.dbh = tb.dbh(+) ");
                //sql.Append("                   and    ta.rjid = tb.rjid(+)) t1 ");
                //sql.Append("          where  t1.gc is null) aa, base_dbxx bb ");
                //sql.Append(" where  aa.dbh = bb.dbh");

                using (var db = new OracleConnection(ConString))
                {
                    Dictionary<string, base_dbxx> dic = new Dictionary<string, base_dbxx>();
                    var list = db.Query<base_dbxx, base_dbrjgx, base_dbxx>(sql.ToString(),(ta,tb)=> {
                        base_dbxx entity = new base_dbxx();
                        if(!dic.TryGetValue(ta.dbh, out entity))
                        {
                            entity = ta;
                            entity.children = new List<base_dbrjgx>();
                            dic.Add(ta.dbh, ta);
                        }
                        entity.children.Add(tb);
                        return entity;
                    },splitOn:"id").Distinct().ToList();
                    return list;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<base_dbxx> Get_UnUse_DbInfo()
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select gcdm, dbmc, dblx, dbh, bz ");
                sql.Append(" FROM(select tb.*, (select count(*) FROM base_dbrjzx where dbh = tb.dbh) qty");
                sql.Append("          FROM(select t1.dblx, (select dbh");
                sql.Append("                            FROM   base_dbxx");
                sql.Append("                            where  dblx = t1.dblx");
                sql.Append("                            and    rownum < 2) as dbh");
                sql.Append("                   FROM(select distinct dblx FROM base_dbxx) t1) ta, base_dbxx tb");
                sql.Append("          where ta.dbh = tb.dbh) tm");
                sql.Append(" where  tm.qty = 0");
                sql.Append(" order by dblx asc");
                using (var db = new OracleConnection(ConString))
                {
                        return db.Query<base_dbxx>(sql.ToString());
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<base_gcxx> GetGCXX()
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        InitDB(db);
                        return Db.GetList<base_gcxx>();
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

        public IEnumerable<base_gwzd> GetGwZd()
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        StringBuilder sql = new StringBuilder();
                        sql.Append("select gcdm, scx, gwh, gwmc, gwlx,gwzlx,gwfl, glgwh, shbz, gzty, pcsip, bz, lrr, lrsj, shr, shsj, cjqxdl, dlsj, dlbbh from BASE_GWZD ");
                        var list = db.Query<base_gwzd>(sql.ToString()).OrderBy(t => t.gwh);
                        return list;
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

        public IEnumerable<base_rjxx> GetRjInfo()
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        InitDB(db);
                        return Db.GetList<base_rjxx>();
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

        public IEnumerable<base_scxxx> GetScxXX(string gcdm)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        List<string> scx = new List<string>();
                        scx.Add("J503");
                        scx.Add("J505");
                        scx.Add("J507");
                        InitDB(db);
                        if (string.IsNullOrEmpty(gcdm))
                        {
                            return Db.GetList<base_scxxx>().Where(t=>scx.Contains(t.scx)).OrderBy(t => t.scx);
                        }
                        else
                        {
                            return Db.GetList<base_scxxx>(Predicates.Field<base_scxxx>(t => t.gcdm, Operator.Eq, gcdm)).Where(t => scx.Contains(t.scx)).OrderBy(t => t.scx);
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

        public IEnumerable<zxjc_ryxx> GetUserCode(string key)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        InitDB(db);
                        var pre = Predicates.Field<zxjc_ryxx>(t => t.username, Operator.Like, key);
                        return Db.GetList<zxjc_ryxx>(pre);
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

        public IEnumerable<base_sbxx> Get_SBXX_List()
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        InitDB(db);
                        return Db.GetList<base_sbxx>();
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

        public IEnumerable<dynamic> Get_UnUse_RjInfo(List<string> dbh)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    var retlist = new List<dynamic>();
                    if (dbh.Count > 0)
                    {
                        StringBuilder sql = new StringBuilder();
                        sql.Append("select * ");
                        sql.Append(" FROM(select ta.*, (select count(*) FROM base_dbrjzx where rjid = ta.rjid) qty");
                        sql.Append("          from BASE_DBRJGX ta");
                        sql.Append("          where ta.dbh in :dbh) tm");
                        sql.Append(" where  tm.qty = 0");
                        var gxlist = db.Query<base_dbrjgx>(sql.ToString(), new { dbh = dbh });
                        var dislist = gxlist.Select(t => new { dbh = t.dbh, dblx = t.dblx }).Distinct();
                        foreach (var item in dislist)
                        {
                            var qty = retlist.Where(t => t.value == item.dbh).Count();
                            if (qty == 0)
                            {
                                var q = gxlist.Where(t => t.dbh == item.dbh);
                                List<dynamic> sub = new List<dynamic>();
                                foreach (var s in q)
                                {
                                    sub.Add(new
                                    {
                                        value = s.id,
                                        label = s.djlx,
                                    });
                                }
                                var entity = new
                                {
                                    label = item.dblx,
                                    value = item.dbh,
                                    children = sub
                                };
                                retlist.Add(entity);
                            }
                        }
                    }
                    return retlist;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<base_gwzd> GetGwListByScx(string scx)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    var scxgwlist = db.Query<base_gwzd>("select * from base_gwzd where scx = :scx", new { scx = scx });
                    return scxgwlist;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<base_gwzd> GetGwXX(string scx)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    var list = db.Query<base_gwzd>("select * from base_gwzd");
                    var scxgwlist = db.Query<base_gwzd>("select * from base_gwzd where scx = :scx", new { scx = scx });
                    foreach (var item in list)
                    {
                        var q = scxgwlist.Where(t => t.scx == item.scx && t.gwh == item.gwh);
                        if (q.Count() == 0)
                        {
                            item.disabled = true;
                        }
                        else
                        {
                            item.disabled = false;
                        }
                    }
                    return list;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<base_wlxx> WLBM_By_Key(string key)
        {
            using (var db = new OracleConnection(ConString))
            {
                try
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("select wlbm, wlmc ");
                    sql.Append(" FROM   base_wlxx ");
                    sql.Append(" where  wlmc like  :key and rownum < 10");
                    sql.Append(" order  by wlbm asc");
                    var list = db.Query<base_wlxx>(sql.ToString(), new { key = "%" + key + "%" });
                    return list;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
        public IEnumerable<mes_menu_entity> UserList()
        {
            using (var db = new OracleConnection(ConString))
            {
                try
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("select id, code, name FROM mes_user_entity order by name asc");
                    var list = db.Query<mes_menu_entity>(sql.ToString());
                    return list;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public IEnumerable<zxjc_ryxx> RyxxList()
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select user_code as usercode, user_name as username, scx, gwh,sfz FROM zxjc_ryxx ");
                using (var db = new OracleConnection(ConString))
                {
                    var list = db.Query<zxjc_ryxx>(sql.ToString());
                    return list;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<base_dbxx> GetDbInfo_By_Key(string key)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select dbmc, dblx, dbh ");
                sql.Append("FROM   base_dbxx ");
                sql.Append("where  dbh like :key ");
                sql.Append("or     dblx like :key ");
                using (var db = new OracleConnection(ConString))
                {
                    if (!string.IsNullOrEmpty(key))
                    {
                        return db.Query<base_dbxx>(sql.ToString(), new { key = "%" + key + "%" }).OrderBy(t => t.dbh);
                    }
                    else
                    {
                        return new List<base_dbxx>();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<base_rjxx> GetRjInfoByKey(string key)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select id,rjlx, rjmc, jgwz ");
                sql.Append(" FROM   base_rjxx");
                sql.Append(" where  rjlx like :key ");
                sql.Append(" or     jgwz like :key ");
                using (var db = new OracleConnection(ConString))
                {
                    if (!string.IsNullOrEmpty(key))
                    {
                        return db.Query<base_rjxx>(sql.ToString(), new { key = "%" + key + "%" });
                    }
                    else
                    {
                        return new List<base_rjxx>();
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

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

namespace ZDMesServices.LBJ.BaseInfo
{
    public class BaseInfoService : OracleBaseFixture, IBaseInfo
    {
        public BaseInfoService(string constr):base(constr)
        {

        }

        public IEnumerable<base_ftpfilepath> FtpConfig()
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    InitDB(db);
                    return Db.GetList<base_ftpfilepath>();
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

        public IEnumerable<base_dbxx> GetDbInfo()
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    InitDB(db);
                    return Db.GetList<base_dbxx>();
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
                    InitDB(db);
                    return Db.GetList<base_gcxx>();
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
                    InitDB(db);
                    return Db.GetList<base_gwzd>().OrderBy(t => t.gwh);
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

        public IEnumerable<base_rjxx> GetRjInfo()
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    InitDB(db);
                    return Db.GetList<base_rjxx>();
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
                    InitDB(db);
                    if (string.IsNullOrEmpty(gcdm))
                    {
                        return Db.GetList<base_scxxx>().OrderBy(t => t.scx);
                    }
                    else
                    {
                        return Db.GetList<base_scxxx>(Predicates.Field<base_scxxx>(t => t.gcdm, Operator.Eq, gcdm)).OrderBy(t => t.scx);
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
                    InitDB(db);
                    var pre = Predicates.Field<zxjc_ryxx>(t => t.username, Operator.Like, key);
                    return Db.GetList<zxjc_ryxx>(pre);
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
                    InitDB(db);
                    return Db.GetList<base_sbxx>();
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
    }
}

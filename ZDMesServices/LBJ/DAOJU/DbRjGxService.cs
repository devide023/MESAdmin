using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.LBJ.ImportData;
using ZDMesModels;
using ZDMesModels.LBJ;
using Oracle.ManagedDataAccess.Client;
using Dapper;
using Autofac.Extras.DynamicProxy;
using ZDMesInterceptor.LBJ;
using ZDMesServices.LBJ.ImportData;

namespace ZDMesServices.LBJ.DAOJU
{
    public class DbRjGxService:BaseDao<base_dbrjgx>
    {
        public DbRjGxService(string constr) :base(constr)
        {
        }

        public override int Add(IEnumerable<base_dbrjgx> entitys)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("insert into base_dbrjgx ");
                sql.Append(" (gcdm, dbh, cpzt, djlx, rjid, dblx, jgwz) ");
                sql.Append(" values ");
                sql.Append(" (:gcdm, :dbh, :cpzt, (select rjlx FROM base_rjxx where id=:rjid and rownum < 2), :rjid,(select dblx FROM base_dbxx where dbh = :dbh and rownum < 2),(select jgwz FROM base_rjxx where id=:rjid and rownum < 2)) ");

                StringBuilder czsql = new StringBuilder();
                czsql.Append("select count(id) from base_dbrjgx where cpzt = :cpzt and dbh=:dbh and rjid = :rjid ");
                using (var db = new OracleConnection(ConString))
                {
                    db.Open();
                    try
                    {
                        using (var trans = db.BeginTransaction())
                        {
                            try
                            {
                                foreach (var item in entitys)
                                {
                                    var cnt = db.ExecuteScalar<int>(czsql.ToString(), item);
                                    if (cnt == 0)
                                    {
                                        db.Execute(sql.ToString(), item, trans);
                                    }
                                }
                                trans.Commit();
                                return entitys.Count();
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

        public override IEnumerable<base_dbrjgx> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select gcdm, dbh, cpzt, (select wlmc ");
                sql.Append(" FROM   base_wlxx ");
                sql.Append(" where  wlbm = base_dbrjgx.cpzt ");
                sql.Append(" and    rownum < 2) as wlmc, djlx, rjid, id, dblx,");
                sql.Append(" (select rjmc FROM base_rjxx where id = base_dbrjgx.rjid) as rjmc,");
                sql.Append(" (select jgwz FROM base_rjxx where id = base_dbrjgx.rjid) as jgwz from base_dbrjgx where 1=1 ");
                StringBuilder sql_cnt = new StringBuilder();
                sql_cnt.Append($"select count(*) from base_dbrjgx where 1=1 ");
                if (parm.sqlexp != null && !string.IsNullOrWhiteSpace(parm.sqlexp))
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
                using (var db = new OracleConnection(ConString))
                {
                    var q = db.Query<base_dbrjgx>(OraPager(sql.ToString()), parm.sqlparam);
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                    return q;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}

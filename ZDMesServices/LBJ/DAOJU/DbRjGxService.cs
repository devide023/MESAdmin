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

        public override IEnumerable<base_dbrjgx> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select gcdm, dbh, cpzt, (select wlmc ");
                sql.Append(" FROM   base_wlxx ");
                sql.Append(" where  wlbm = base_dbrjgx.cpzt ");
                sql.Append(" and    rownum < 2) as wlmc, djlx, rjid, id, dblx from base_dbrjgx where 1=1 ");
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.LBJ.DaoJu;
using ZDMesModels;
using ZDMesModels.LBJ;
using Oracle.ManagedDataAccess.Client;
using Dapper;
namespace ZDMesServices.LBJ.DAOJU
{
    public class DjReportService: OracleBaseFixture, IDjReport
    {
        public DjReportService(string constr):base(constr)
        {

        }

        public IEnumerable<base_dbrjzx> GetRjZxList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select base_dbrjzx.*, (select dblx from base_dbxx where dbh = base_dbrjzx.dbh) as dblx, round((rjdqsm / rjbzsm) * 100, 2) as rjzt ");
                sql.Append(" from base_dbrjzx where 1=1 ");

                StringBuilder sql_cnt = new StringBuilder();
                sql_cnt.Append("select count(*) from base_dbrjzx where 1=1 ");

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
                    var q = db.Query<base_dbrjzx>(OraPager(sql.ToString()), parm.sqlparam);
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                    return q;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<base_sbxx> Get_SbxxBy_Scx(string scx)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select * FROM base_sbxx where scx = :scx");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<base_sbxx>(sql.ToString(), new { scx = scx });
                }                
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<base_dbxx> Get_ZxDbXX_By_Sbbh(string scx, string sbbh)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select * ");
                sql.Append(" FROM   base_dbxx ");
                sql.Append(" where  dbh in (select distinct dbh ");
                sql.Append("                from BASE_DBRJZX ");
                sql.Append("                where scx = :scx ");
                sql.Append("                and    sbbh = :sbbh )");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<base_dbxx>(sql.ToString(), new { scx = scx, sbbh = sbbh });
                } 
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

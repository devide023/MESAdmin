using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using ZDMesModels.LBJ;

namespace ZDMesServices.LBJ.SMJJK
{
    public class SmjjkService : BaseDao<zxjc_smls>
    {
        public SmjjkService(string constr) : base(constr)
        {

        }

        public override IEnumerable<zxjc_smls> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    StringBuilder sql_cnt = new StringBuilder();
                    sql_cnt.Append("select count(1) from ZXJC_SMLS t where 1=1 ");
                    StringBuilder sql = new StringBuilder();
                    sql.Append(@"SELECT t1.*, (CASE WHEN (t1.SMJBS = '首件' AND t2.SMJBS = '末件') OR ( t2.SMJBS = '首件' AND t1.SMJBS = '末件') THEN 'Y'
									                      ELSE 'N' 
									                      END) AS SFBH
                                       FROM ZXJC_SMLS t1
                                       LEFT JOIN ZXJC_SMLS t2 ON t1.SCX = t2.SCX AND t1.BC = t2.BC AND t1.JCSJ != t2.JCSJ");
                    sql.Append("");
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
                    try
                    {
                        var q = db.Query<zxjc_smls>(OraPager(sql.ToString()), parm.sqlparam);
                        resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                        return q;
                    }
                    catch (OracleException)
                    {
                        throw;
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

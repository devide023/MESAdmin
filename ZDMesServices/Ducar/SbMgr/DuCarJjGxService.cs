using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using ZDMesModels.Ducar;

namespace ZDMesServices.Ducar.SbMgr
{
    /// <summary>
    /// 夹具关系
    /// </summary>
    internal class DuCarJjGxService : BaseDao<jjgxb>
    {
        public DuCarJjGxService(string constr) : base(constr)
        {
        }

        public override IEnumerable<jjgxb> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                resultcount = 0;
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                sql.Append($"select guid, engine_no, jjh, jbh, agvno, bdsj, bdr, status_no, order_no, zpjhh, jx_no, twotqm, scx, gwh,(select gwmc from base_gwzd where gwh =jjgxb.gwh and scx=jjgxb.scx and rownum = 1) as gwmc from jjgxb where 1=1 ");
                sql_cnt.Append($"select count(*) from jjgxb where 1=1 ");

                if (parm.sqlexp != null && !string.IsNullOrWhiteSpace(parm.sqlexp))
                {
                    sql.Append(" and " + parm.sqlexp);
                    sql_cnt.Append(" and " + parm.sqlexp);
                }
                //前端排序
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
                using (var db  = new OracleConnection(ConString))
                {
                    var q = db.Query<jjgxb>(OraPager(sql.ToString()), parm.sqlparam);
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

using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using ZDMesModels.Ducar;

namespace ZDMesServices.Ducar.JhMgr
{
    public class DuCarDdzjService : BaseDao<pp_scddzj>
    {
        public DuCarDdzjService(string constr) : base(constr)
        {
        }
        public override IEnumerable<pp_scddzj> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                sql.Append($"select pcjhh, scddh, gc, ylh, ylxmh, zjwl,(select wlmc FROM base_wlxx where wlbm = pp_scddzj.zjwl and rownum = 1 ) as wlmc, zjsl, zjdw, fjck, zplx, pc, gys, xmwb, scbz, pxzfc,supl_lock from pp_scddzj where 1=1 ");
                sql_cnt.Append($"select count(*) from pp_scddzj where 1=1 ");

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
                    else
                    {
                        sql.Append($" order by zjwl asc ");
                    }
                }
                using (var db = new OracleConnection(ConString))
                {
                    var q = db.Query<pp_scddzj>(OraPager(sql.ToString()), parm.sqlparam);
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

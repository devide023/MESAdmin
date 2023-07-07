using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZDMesModels;
using ZDMesModels.LBJ;

namespace ZDMesServices.LBJ.DAOJU
{
    public class RjSmXhService: BaseDao<base_rjsmxh>
    {
        public RjSmXhService(string constr) : base(constr)
        {
            
        }

        public override IEnumerable<base_rjsmxh> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select rowid as rid,gcdm, rjlx, cpzt, (select wlmc ");
                sql.Append(" FROM   base_wlxx ");
                sql.Append(" where  wlbm = base_rjsmxh.cpzt ");
                sql.Append(" and    rownum < 2) as wlmc, scx, sbbh, (select sbmc ");
                sql.Append(" FROM   base_sbxx ");
                sql.Append(" where  sbbh = ");
                sql.Append("        base_rjsmxh.sbbh ");
                sql.Append(" and rownum< 2) as sbmc, mjxhsm ");
                sql.Append(" from   base_rjsmxh where 1=1 ");
                StringBuilder sql_cnt = new StringBuilder();
                sql_cnt.Append($"select count(*) from base_rjsmxh where 1=1 ");
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
                    var q = db.Query<base_rjsmxh>(OraPager(sql.ToString()), parm.sqlparam);
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                    return q;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override bool Del(IEnumerable<base_rjsmxh> entitys)
        {
            List<base_rjsmxh> oklist = new List<base_rjsmxh>();
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from base_rjsmxh where rowid = :rid");
            using (var db = new OracleConnection(ConString))
            {
                foreach (var item in entitys)
                {
                    var q = db.Execute(sql.ToString(), new { rid = item.rid});
                    if (q > 0)
                    {
                        oklist.Add(item);
                    }
                }
                return oklist.Count == entitys.Count();
            }
        }
    }
}

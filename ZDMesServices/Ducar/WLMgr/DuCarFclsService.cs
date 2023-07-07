using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using ZDMesModels.Ducar;
using static Slapper.AutoMapper;

namespace ZDMesServices.Ducar.WLMgr
{
    public class DuCarFclsService : BaseDao<zxjc_fcls>
    {
        public DuCarFclsService(string constr) : base(constr)
        {
        }
        public override IEnumerable<zxjc_fcls> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_main = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();

                sql_main.Append("select ta.id, ta.scx, ta.gwh, tb.gwmc, ta.engine_no, ta.gwbh, ta.xgbh, (select user_name ");
                sql_main.Append(" FROM   zxjc_ryxx  ");
                sql_main.Append("  where  user_code =  ");
                sql_main.Append(" ta.lrr  ");
                sql_main.Append(" and rownum = 1) as lrr, ta.lrsj, ta.gwghsj, gwbzsj, ta.gwsysj, ta.xgghsj, ta.xgbzsj, ta.xgsysj  ");
                sql_main.Append(" from   zxjc_fcls ta, base_gwzd tb  ");
                sql_main.Append(" where  ta.gwh = tb.gwh(+)  ");
                sql_main.Append(" and ta.scx = tb.scx(+) ");

                sql.Append("select * from (");
                sql.Append(sql_main);
                sql.Append(" ) zxjc_fcls where 1=1 ");

                sql_cnt.Append($"select count(*) from (");
                sql_cnt.Append(sql_main);
                sql_cnt.Append(" ) zxjc_fcls where 1=1 ");
                //
                StringBuilder sqlgwh = new StringBuilder();
                sqlgwh.Append("select scx, gwh, gwmc FROM base_gwzd order by scx asc");

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
                        sql.Append($" order by lrsj desc ");
                    }
                }
                using (var db = new OracleConnection(ConString))
                {
                    //var gwzdlist = db.Query<base_gwzd>(sqlgwh.ToString());
                    var qs = db.Query<zxjc_fcls>(OraPager(sql.ToString()), parm.sqlparam);
                    //foreach (var q in qs)
                    //{
                    //    q.gwhs = gwzdlist.Where(t => t.scx == q.scx).Select(t => new sys_options_list() { label = t.gwmc, value = t.gwh }).OrderBy(t => t.value).ToList();
                    //}
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                    return qs;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.DuCar;
using ZDMesInterfaces.TJ;
using ZDMesModels;
using ZDMesModels.Ducar;

namespace ZDMesServices.Ducar.RyMgr
{
    public class DuCarRyjnService : BaseDao<zxjc_ryxx_jn>, IDuCarRyjn, IBatAtachValue<zxjc_ryxx_jn>
    {
        public DuCarRyjnService(string constr) : base(constr)
        {
        }

        public override IEnumerable<zxjc_ryxx_jn> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                StringBuilder sql_main = new StringBuilder();
                sql_main.Append($"select ta.gcdm, ta.user_code as usercode, tb.user_name as username, ta.jnbh, ta.jnxx, ta.scx, ta.gwh,tc.gwmc, ta.sfhg, ta.lrr, ta.lrsj, ta.jnfl, ta.jnsj, ta.jnsld from zxjc_ryxx_jn ta,zxjc_ryxx tb,base_gwzd tc  ");
                sql_main.Append(" where ta.user_code = tb.user_code(+) ");
                sql_main.Append(" and ta.scx = tb.scx(+) ");
                sql_main.Append(" and ta.gwh = tc.gwh(+) ");
                sql_main.Append(" and ta.scx = tc.scx(+) ");
                //
                sql.Append("select * from (");
                sql.Append(sql_main);
                sql.Append(" ) zxjc_ryxx_jn where 1=1 ");
                sql_cnt.Append($"select count(*) from (");
                sql_cnt.Append(sql_main);
                sql_cnt.Append(" ) zxjc_ryxx_jn where 1=1 ");
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
                    var gwzdlist = db.Query<base_gwzd>(sqlgwh.ToString());
                    var q = db.Query<zxjc_ryxx_jn>(OraPager(sql.ToString()), parm.sqlparam);
                    foreach (var item in q)
                    {
                        item.gwhs = gwzdlist.Where(t => t.scx == item.scx).Select(t => new sys_options_list() { label = t.gwmc, value = t.gwh }).OrderBy(t => t.value).ToList();
                    }
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                    return q;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public string CreateJnCode()
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select seq_mes_jnbh.nextval FROM dual");
                using (var db = new OracleConnection(ConString))
                {
                    var no = db.ExecuteScalar<int>(sql.ToString());
                    return "JN" + no.ToString().PadLeft(5, '0');
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<zxjc_ryxx_jn> BatSetValue(List<zxjc_ryxx_jn> list)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select seq_mes_jnbh.nextval FROM dual");
                using (var db = new OracleConnection(ConString))
                {
                    foreach (var item in list)
                    {
                        var no = db.ExecuteScalar<int>(sql.ToString());
                        var jnbh = "JN" + no.ToString().PadLeft(5, '0');
                        item.jnbh = jnbh;
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

using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.TJ;
using ZDMesModels;
using ZDMesModels.Ducar;

namespace ZDMesServices.Ducar.RyMgr
{
    public class DuCarRyxxService : BaseDao<zxjc_ryxx>, IBatAtachValue<zxjc_ryxx>
    {
        public DuCarRyxxService(string constr) : base(constr)
        {
        }

        public override IEnumerable<zxjc_ryxx> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                StringBuilder sql_main = new StringBuilder();
                sql.Append("select * from (");
                sql_main.Append($"select ta.user_code as usercode, ta.user_name as username, ta.pass_word as password, ta.scx, ta.gwh, tb.gwmc, ta.bzxx, ta.hgsg, ta.rsrq, ta.jmh, ta.ryxb, ta.xpmc, ta.scbz, ta.rylx, ta.csrq from zxjc_ryxx ta, base_gwzd tb where ta.gwh = tb.gwh(+) and ta.scx = tb.scx(+) ");
                sql.Append(sql_main);
                sql.Append(" ) zxjc_ryxx where 1=1 ");
                sql_cnt.Append($"select count(*) from (");
                sql_cnt.Append(sql_main);
                sql_cnt.Append(" ) zxjc_ryxx where 1=1 ");
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
                }

                using (var db = new OracleConnection(ConString))
                {
                    var gwzdlist = db.Query<base_gwzd>(sqlgwh.ToString());
                    var q = db.Query<zxjc_ryxx>(OraPager(sql.ToString()), parm.sqlparam);
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

        public List<zxjc_ryxx> BatSetValue(List<zxjc_ryxx> list)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select seq_mes_rybh.nextval FROM dual");
                using (var db = new OracleConnection(ConString))
                {
                    foreach (var item in list)
                    {
                        var no = db.ExecuteScalar<int>(sql.ToString());
                        var ucode = no.ToString().PadLeft(6, '0');
                        item.usercode = ucode;
                    }
                }
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

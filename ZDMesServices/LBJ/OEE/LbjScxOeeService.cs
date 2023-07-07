using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using ZDMesModels.LBJ;
using static Dapper.SqlMapper;

namespace ZDMesServices.LBJ.OEE
{
    public class LbjScxOeeService : BaseDao<zxjc_scx_oee>
    {
        public LbjScxOeeService(string constr) : base(constr)
        {
        }

        public override IEnumerable<zxjc_scx_oee> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                StringBuilder sql_main = new StringBuilder();
                sql.Append("select * from (");
                sql_main.Append($"select gcdm, scx, rq, jhzxsj, zzwbqh, zzwcf, zzwbzxx, zzwsbby, px, xx, dlsj_jam as dlsjjam, dlsj_wait as dlsjwait, hdsj, hxsj, gzsj, qttjsj, lljp, hgpsl, bhgpsl, ");
                sql_main.Append(" oee_target as oeetarget, sfjs, jhyxsj, sjyxsj, jhtjsj, fjhtjsj, tjsj, jgllsj, sjcl, jph, sjkdl, xnkdl, hgpl, oee_real, teep, scxzx ");
                sql_main.Append(" FROM   zxjc_scx_oee ");
                sql.Append(sql_main);
                sql.Append(" ) zxjc_scx_oee where 1=1 ");
                sql_cnt.Append($"select count(*) from (");
                sql_cnt.Append(sql_main);
                sql_cnt.Append(" ) zxjc_scx_oee where 1=1 ");
                //
                StringBuilder szxzx = new StringBuilder();
                szxzx.Append("select scx,scxmc,scxzx,scxzxmc FROM base_scxxx_jj");

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
                    var scxzxlist = db.Query<base_scxxx_jj>(szxzx.ToString());
                    var q = db.Query<zxjc_scx_oee>(OraPager(sql.ToString()), parm.sqlparam);
                    foreach (var item in q)
                    {
                        item.scxzxs = scxzxlist.Where(t => t.scx == item.scx).Select(t => new option_list() { label = t.scxzxmc, value = t.scxzx }).OrderBy(t => t.value).ToList();
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

        public override int Add(IEnumerable<zxjc_scx_oee> entitys, out IEnumerable<zxjc_scx_oee> noklist)
        {
            try
            {
                List<zxjc_scx_oee> postdata = new List<zxjc_scx_oee>(); 
                List<zxjc_scx_oee> repeatlist = new List<zxjc_scx_oee>();
                StringBuilder sql = new StringBuilder();
                sql.Append("select count(*) FROM zxjc_scx_oee where scx = :scx and nvl(scxzx,0)= :scxzx and rq between trunc(:rq1) and trunc(:rq2) ");
                using (var db = new OracleConnection(ConString))
                {
                    foreach (var item in entitys)
                    {
                        var d1 = Convert.ToDateTime(item.rq.ToString("yyyy-MM-dd"));
                        var d2 = d1.AddDays(1);
                        var cnt = db.ExecuteScalar<int>(sql.ToString(), new { scx = item.scx, scxzx=string.IsNullOrEmpty(item.scxzx)?"0":item.scxzx, rq1 = d1, rq2 = d2 });
                        if (cnt > 0)
                        {
                            repeatlist.Add(item);
                        }
                        else
                        {
                            postdata.Add(item);
                        }
                    }
                    noklist = repeatlist;
                    return base.Add(postdata);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

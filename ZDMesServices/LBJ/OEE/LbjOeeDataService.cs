using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using ZDMesModels.LBJ;

namespace ZDMesServices.LBJ.OEE
{
    public class LbjOeeDataService : BaseDao<base_template_scx_oee>
    {
        public LbjOeeDataService(string constr) : base(constr)
        {
        }

        public override IEnumerable<base_template_scx_oee> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                sql.Append($"select rowid as rid,gcdm, scx,scxzx, jhzxsj, zzwbqh, zzwcf, zzwbzxx, zzwsbby, px, xx, dlsj_jam as dlsjjam, dlsj_wait as dlsjwait, hdsj, hxsj, gzsj, qttjsj, lljp, oee_target as oeetarget FROM base_template_scx_oee where 1=1 ");
                sql_cnt.Append($"select count(*) from base_template_scx_oee where 1=1 ");

                StringBuilder sqlscxzx = new StringBuilder();
                sqlscxzx.Append("select scx, scxmc, scxzx, scxzxmc FROM base_scxxx_jj");

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
                        sql.Append(" order by scx asc,scxzx asc ");
                    }
                }
                using (var db = new OracleConnection(ConString))
                {
                    var scxzxlist = db.Query<base_scxxx_jj>(sqlscxzx.ToString());
                    var q = db.Query<base_template_scx_oee>(OraPager(sql.ToString()), parm.sqlparam);
                    foreach (var item in q)
                    {
                        item.scxzxs = scxzxlist.Where(t => t.scx == item.scx).Select(t => new option_list() { label = t.scxzxmc, value = t.scxzx }).ToList();
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

        public override int Add(IEnumerable<base_template_scx_oee> entitys, out IEnumerable<base_template_scx_oee> noklist)
        {
            try
            {
                List<base_template_scx_oee> postdata = new List<base_template_scx_oee>();
                List<base_template_scx_oee> repeatlist = new List<base_template_scx_oee>();
                StringBuilder sql = new StringBuilder();
                sql.Append("select count(*) FROM base_template_scx_oee where scx = :scx and scxzx = :scxzx  ");
                using (var db = new OracleConnection(ConString))
                {
                    foreach (var item in entitys)
                    {
                        var cnt = db.ExecuteScalar<int>(sql.ToString(), new { scx = item.scx,scxzx = item.scxzx });
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

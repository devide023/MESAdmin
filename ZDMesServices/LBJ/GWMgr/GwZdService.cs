using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.Common;
using ZDMesModels.LBJ;
using Oracle.ManagedDataAccess.Client;
using ZDMesModels;
using Dapper;
namespace ZDMesServices.LBJ.GWMgr
{
    public class GwZdService:BaseDao<base_gwzd>
    {
        public GwZdService(string constr):base(constr)
        {

        }
        public override IEnumerable<base_gwzd> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append($"select rowid as rid, gcdm, scx, gwh, gwmc, gwlx,gwzlx, gwfl, glgwh, shbz, gzty, pcsip, bz, lrr, lrsj, shr, shsj, cjqxdl, dlsj, dlbbh,scxzx from base_gwzd where 1=1 ");
                StringBuilder sql_cnt = new StringBuilder();
                sql_cnt.Append($"select count(*) from base_gwzd where 1=1 ");
                //生产线子线
                StringBuilder sqlscxzx = new StringBuilder();
                sqlscxzx.Append("select scx, scxmc, scxzx, scxzxmc FROM base_scxxx_jj");
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
                    else
                    {
                        sql.Append($" order by gwh asc ");
                    }
                }
                using (var db = new OracleConnection(ConString))
                {
                    var scxzxlist = db.Query<base_scxxx_jj>(sqlscxzx.ToString());
                    var q = db.Query<base_gwzd>(OraPager(sql.ToString()), parm.sqlparam);
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
        public override bool Modify(IEnumerable<base_gwzd> entitys)
        {
            try
            {
                int index = 0;
                StringBuilder sql = new StringBuilder();
                sql.Append("update base_gwzd set gzty = :gzty where rowid = :rid");
                var rids = entitys.Select(t => t.rid).ToList();
                using (var db = new OracleConnection(ConString))
                {
                    foreach (var item in entitys)
                    {
                        var cnt = db.Execute(sql.ToString(), new { rid = rids, gzty = item.gzty });
                        if (cnt > 0)
                        {
                            index++;
                        }
                    }
                    return index > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using ZDMesModels.Ducar;

namespace ZDMesServices.Ducar.GyMgr
{
    public class DuCarGylxService : BaseDao<zxjc_gylx>
    {
        public DuCarGylxService(string constr) : base(constr)
        {
        }

        public override IEnumerable<zxjc_gylx> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                StringBuilder sql_main = new StringBuilder();
                sql.Append("select * from (");
                sql_main.Append($"select ta.gcdm, ta.scx, ta.status_no as status_no, ta.gwh, tb.gwmc, ta.zpsx, ta.mj, ta.fsbz, ta.shbz, ta.fjbh, ta.bz, ta.lrr, ta.lrsj, ta.shr, ta.shsj, ta.fsr, ta.fssj, ta.jx_no as jxno, ta.sfzp, ta.cfcs from zxjc_gylx ta, base_gwzd tb ");
                sql_main.Append(" where ta.gwh = tb.gwh(+) and ta.scx = tb.scx(+) ");
                sql.Append(sql_main);
                sql.Append(" ) zxjc_gylx where 1=1 ");
                sql_cnt.Append($"select count(*) from (");
                sql_cnt.Append(sql_main);
                sql_cnt.Append(" ) zxjc_gylx where 1=1 ");
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
                    var q = db.Query<zxjc_gylx>(OraPager(sql.ToString()), parm.sqlparam);
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

        public override int Add(IEnumerable<zxjc_gylx> entitys, out IEnumerable<zxjc_gylx> noklist)
        {
            try
            {
                List<zxjc_gylx> postdata = new List<zxjc_gylx>();
                List<zxjc_gylx> repeatdata = new List<zxjc_gylx>();
                StringBuilder sql = new StringBuilder();
                sql.Append("select count(*) FROM  zxjc_gylx where scx = :scx and jx_no = :jxno and gwh = :gwh and nvl(status_no,'_') = :statusno ");
                using (var db = new OracleConnection(ConString))
                {
                    foreach (var item in entitys)
                    {
                        var cnt = db.ExecuteScalar<int>(sql.ToString(), new { scx = item.scx, jxno = item.jxno, gwh = item.gwh, statusno = string.IsNullOrEmpty(item.statusno) ? "_" : item.statusno });
                        if (cnt == 0)
                        {
                            postdata.Add(item);
                        }
                        else
                        {
                            repeatdata.Add(item);
                        }
                    }
                }
                noklist = repeatdata;
                return base.Add(postdata);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

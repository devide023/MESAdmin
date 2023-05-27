using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.DuCar;
using ZDMesModels;
using ZDMesModels.Ducar;

namespace ZDMesServices.Ducar.JstzMgr
{
    public class DuCarJstzService : BaseDao<zxjc_t_jstc>, IDuCarJstz
    {
        public DuCarJstzService(string constr) : base(constr)
        {
        }

        public IEnumerable<zxjc_t_jstcfp> Jtfp_Detail(string jtid)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select ta.gcdm, ta.scx, ta.gwh, ta.jx_no as jxno, ta.status_no as statusno, ta.bz, ta.lrr1, ta.lrsj1, ta.lrr2, ta.lrsj2, tb.jtid,");
                sql.Append(" tb.jcbh, tb.jcmc, tb.jcms, tb.wjlj, tb.jwdx, tb.scry, tb.scpc, tb.scsj, tb.yxqx1, tb.yxqx2, tb.fp_flg as fp_flg, tb.fp_sj as fpsj, tb.fpr, tb.wjfl ");
                sql.Append(" FROM zxjc_t_jstcfp ta,zxjc_t_jstc tb where ta.jtid = tb.jtid and tb.jtid = :jtid ");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<zxjc_t_jstcfp, zxjc_t_jstc, zxjc_t_jstcfp>(sql.ToString(),(ta,tb)=> {
                        ta.jstc = tb;
                        ta.jtid= jtid;
                        return ta;
                    }, new { jtid = jtid },splitOn: "jtid");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<zxjc_t_jstc> Wfp_JtTz_List(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                sql.Append($"select jtid, jcbh, jcmc, jcms, wjlj, jwdx, scry, scpc, scsj, yxqx1, yxqx2, gcdm, fp_flg as fpflg, fp_sj as fpsj, fpr, wjfl, scx from zxjc_t_jstc where 1=1 ");
                sql_cnt.Append($"select count(*) from zxjc_t_jstc where 1=1 ");

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
                        sql.Append($" order by decode(fp_flg,'N',1,0) desc,yxqx2 desc ");
                    }
                }
                using (var db = new OracleConnection(ConString))
                {
                    var q = db.Query<zxjc_t_jstc>(OraPager(sql.ToString()), parm.sqlparam);
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

using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using ZDMesModels.Ducar;

namespace ZDMesServices.Ducar.JstzMgr
{
    public class DuCarJstzFpService : BaseDao<zxjc_t_jstcfp>
    {
        public DuCarJstzFpService(string constr) : base(constr)
        {
        }
        public override int Add(IEnumerable<zxjc_t_jstcfp> entitys)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" insert into zxjc_t_jstcfp");
                sql.Append(" (jtid, gcdm, scx, gwh, jx_no, status_no, bz, lrr1, lrsj1, lrr2, lrsj2, id) ");
                sql.Append(" values ");
                sql.Append(" (:jtid, :gcdm, :scx, :gwh, :jxno, :statusno, :bz, :lrr1, :lrsj1, :lrr2, :lrsj2, :id) ");
                StringBuilder updatesql = new StringBuilder();
                updatesql.Append(" update zxjc_t_jstc set fp_flg = 'Y',fp_sj=sysdate,fpr=:fpr where jtid=:jtid ");
                using (var db = new OracleConnection(ConString))
                {
                    db.Open();
                    try
                    {
                        using (var trans = db.BeginTransaction())
                        {
                            try
                            {
                                foreach (var item in entitys)
                                {
                                    db.Execute(sql.ToString(), item, trans);
                                    db.Execute(updatesql.ToString(), new {fpr=item.lrr1,jtid=item.jtid}, trans);
                                }
                                trans.Commit();
                                return 1;
                            }
                            catch (Exception)
                            {
                                trans.Rollback();
                                throw;
                            }
                        }
                    }
                    finally
                    {
                        db.Close();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override bool Del(IEnumerable<zxjc_t_jstcfp> entitys)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("delete from zxjc_t_jstcfp where id in :ids");
                StringBuilder usql = new StringBuilder();
                usql.Append("update zxjc_t_jstc set fp_flg = 'N',fp_sj=null,fpr=null where jtid in :jtids ");
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        db.Open();
                        using (var trans = db.BeginTransaction())
                        {
                            try
                            {
                                db.Execute(sql.ToString(), new { ids = entitys.Select(t => t.id).ToArray() },trans);
                                db.Execute(usql.ToString(), new { jtids = entitys.Select(t => t.jtid).Distinct().ToArray() },trans);
                                trans.Commit();
                                return true;
                            }
                            catch (Exception)
                            {
                                trans.Rollback();
                                throw;
                            }
                        }
                    }
                    finally
                    {
                        db.Close();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override IEnumerable<zxjc_t_jstcfp> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                resultcount = 0;
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                StringBuilder mx = new StringBuilder();
                sql.Append("select ta.gcdm, ta.scx, ta.gwh, ta.jx_no as jxno, ta.status_no as statusno, ta.bz, ta.lrr1, ta.lrsj1, ta.lrr2, ta.lrsj2, ta.jtid,ta.id");
                sql.Append(" FROM zxjc_t_jstcfp ta,(select jtid, jcbh, jcmc, jcms, wjlj, jwdx, scry, scpc, scsj, yxqx1, yxqx2, gcdm, fp_flg as fpflg, fp_sj as fpsj, fpr, wjfl from zxjc_t_jstc) tb where ta.jtid = tb.jtid ");
                sql_cnt.Append($"select count(*) from zxjc_t_jstcfp ta,(select jtid, jcbh, jcmc, jcms, wjlj, jwdx, scry, scpc, scsj, yxqx1, yxqx2, gcdm, fp_flg as fpflg, fp_sj as fpsj, fpr, wjfl from zxjc_t_jstc) tb where ta.jtid = tb.jtid ");
                mx.Append("select jtid, jcbh, jcmc, jcms, wjlj, jwdx, scry, scpc, scsj, yxqx1, yxqx2, gcdm, fp_flg as fpflg, fp_sj as fpsj, fpr, wjfl, scx FROM zxjc_t_jstc where jtid = :jtid");
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
                    var qs = db.Query<zxjc_t_jstcfp>(OraPager(sql.ToString()), parm.sqlparam);
                    foreach (var q in qs)
                    {
                        q.jstc = db.Query<zxjc_t_jstc>(mx.ToString(), new { jtid = q.jtid }).FirstOrDefault();
                    }
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

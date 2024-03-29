﻿using Aspose.Cells.Revisions;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using ZDMesModels.Ducar;

namespace ZDMesServices.Ducar.JhMgr
{
    internal class DuCarOrderXhService : BaseDao<zxjc_order_sxh>
    {
        public DuCarOrderXhService(string constr) : base(constr)
        {
        }

        public override bool Del(IEnumerable<zxjc_order_sxh> entitys)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("delete FROM zxjc_order_sxh where rowid in :rid");
                using (var db = new OracleConnection(ConString))
                {
                   return db.Execute(sql.ToString(), new { rid = entitys.Select(t => t.rid) })>0;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override bool Modify(IEnumerable<zxjc_order_sxh> entitys)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("update zxjc_order_sxh set order_no=:orderno,xh=:xh,scx=:scx,pcsl=:pcsl,sjscsj=:sjscsj where rowid = :rid ");
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        db.Open();
                        using (var trans = db.BeginTransaction())
                        {
                            try
                            {
                                foreach (var item in entitys)
                                {
                                    db.Execute(sql.ToString(), item, trans);
                                }
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

        public override IEnumerable<zxjc_order_sxh> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                StringBuilder orderinfosql = new StringBuilder();
                //
                orderinfosql.Append("select zpjhh, order_no, scx, xh, scddlx, scsl, scsj, jhsj, ztbm, jx, first_flag,");
                orderinfosql.Append("scbz, zt, khdm, khpch, khlsh, jhh, xshh, xsbz, xssx, jtdh, yzpsl, jdcqyy, cqyy, ");
                orderinfosql.Append("cqyy_lrsj, jypyb, jypyb2, cpyhjg, yhph, udcg, sapzt, gc, lrsj, yjhh, yxshh, zjxxsl,");
                orderinfosql.Append("bzxxsl, rksl, cksl, yksl, zjxxsl_sj, bzxxsl_sj, rksl_sj, cksl_sj, scrq, yksl_sj, shjzt,");
                orderinfosql.Append("lshsc, jhlb, plan_type, tsdz, sc_jhc, sc_fxh, qslsh, jslsh, write_req, seq_length, non_series,");
                orderinfosql.Append("js_qsh, js_jsh, sjscrq, xsfhrq, ddcjsj, ddcjrq, ddshsj, ddshrq, pxbj, jypyb_sl, jypyb2_sl, sj_scrq,");
                orderinfosql.Append("jd_bm, jd_bz, write_flg, bsxcpm, yqjhrq, cljhrq, ychf, write_exc, dcyy, zrbm, yqscsj, jj_bj, jj_bz, csmc,");
                orderinfosql.Append("bjmc, yjscrq, jd_lrsj, zhtbsj FROM pp_zpjh where order_no = :orderno");
                //
                sql.Append($"select rowid as rid,order_no as orderno, xh, sjscsj, lrsj,scx,pcsl from zxjc_order_sxh where sjscsj >= trunc(sysdate) ");
                sql_cnt.Append($"select count(*) from zxjc_order_sxh where sjscsj >= trunc(sysdate) ");

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
                        sql.Append($" order by order_no asc, xh asc ");
                    }
                }
                using (var db = new OracleConnection(ConString))
                {
                    var qs = db.Query<zxjc_order_sxh>(OraPager(sql.ToString()), parm.sqlparam);
                    foreach (var q in qs)
                    {
                        var v = db.Query<pp_zpjh>(orderinfosql.ToString(), new { orderno = q.orderno });
                        if (v.Count() > 0)
                        {
                            q.orderinfo = v.FirstOrDefault();
                        }
                        else
                        {
                            q.orderinfo = new pp_zpjh();
                        }
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

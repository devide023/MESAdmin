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
    /// <summary>
    /// 装配计划
    /// </summary>
    public class DuCarZpjhService : BaseDao<pp_zpjh>
    {
        public DuCarZpjhService(string constr) : base(constr)
        {
        }

        public override bool Modify(IEnumerable<pp_zpjh> entitys)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("update pp_zpjh set scx = :scx,xh = :xh where order_no=:order_no");
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

        public override IEnumerable<pp_zpjh> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sqljy= new StringBuilder();
                sqljy.Append("select order_no, qdjy, gdbomjy, gylxjy, kzbmjy, lbjcjjy, ptjxjy, status, jjyzbsl, sjbz, lrsj, scx from ZXJC_ORDER_JY where order_no = :orderno and rownum = 1 ");
                //
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                StringBuilder sql_main = new StringBuilder();
                sql.Append("select * from (");
                sql_main.Append("select zpjhh, order_no, scx, xh, scddlx, scsl, scsj, jhsj, ztbm, jx, first_flag, scbz, zt,");
                sql_main.Append("khdm, khpch, khlsh, jhh, xshh, xsbz, xssx, jtdh, yzpsl, jdcqyy, cqyy, cqyy_lrsj, jypyb,");
                sql_main.Append("jypyb2, cpyhjg, yhph, udcg, sapzt, gc, lrsj, yjhh, yxshh, zjxxsl, bzxxsl, rksl, cksl,");
                sql_main.Append("yksl, zjxxsl_sj, bzxxsl_sj, rksl_sj, cksl_sj, scrq, yksl_sj, shjzt, lshsc, jhlb, ");
                sql_main.Append("plan_type, tsdz, sc_jhc, sc_fxh, qslsh, jslsh, write_req, seq_length, non_series, ");
                sql_main.Append("js_qsh, js_jsh, sjscrq, xsfhrq, ddcjsj, ddcjrq, ddshsj, ddshrq, pxbj, jypyb_sl, ");
                sql_main.Append("jypyb2_sl, sj_scrq, jd_bm, jd_bz, write_flg, bsxcpm, yqjhrq, cljhrq, ychf, write_exc,");
                sql_main.Append("dcyy, zrbm, yqscsj, jj_bj, jj_bz, csmc, bjmc, yjscrq, jd_lrsj, zhtbsj");
                sql_main.Append(",(select count(order_no) FROM zxjc_order_jy where status = '已完成' and order_no = pp_zpjh.order_no) cnt ");
                sql_main.Append(" from pp_zpjh ");
                sql.Append(sql_main);
                sql.Append(" ) pp_zpjh where cnt = 0 ");
                sql_cnt.Append($"select count(*) from ( ");
                sql_cnt.Append(sql_main);
                sql_cnt.Append(") pp_zpjh where cnt = 0 ");
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
                        sql.Append($" order by jhsj desc ");
                    }
                }
                using (var db = new OracleConnection(ConString))
                {
                    var qs = db.Query<pp_zpjh>(OraPager(sql.ToString()), parm.sqlparam);
                    foreach (var q in qs)
                    {
                        var v = db.Query<zxjc_order_jy>(sqljy.ToString(), new { orderno = q.order_no });
                        if (v.Count() > 0)
                        {
                            q.orderjy = v.FirstOrDefault();
                        }
                        else
                        {
                            q.orderjy = new zxjc_order_jy();
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

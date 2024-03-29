﻿using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.DuCar;
using ZDMesModels.Ducar;

namespace ZDMesServices.Ducar.JhMgr
{
    public class DuCarOrderService : OracleBaseFixture, IDuCarOrder
    {
        public DuCarOrderService(string constr) : base(constr)
        {
        }

        public sys_order_jy_result HasBOM(string orderno)
        {
            try
            {
                orderno = orderno.Replace("\r", "").Replace("\n", "").Trim();
                sys_order_jy_result ret = new sys_order_jy_result();
                StringBuilder sql = new StringBuilder();
                sql.Append("select count(scddh) FROM pp_scddzj where scddh = :orderno ");
                using (var db = new OracleConnection(ConString))
                {
                    var isbom = db.ExecuteScalar<int>(sql.ToString(), new { orderno = orderno.Replace("\r","").Replace("\n","").Trim()});
                    if(isbom>0)
                    {
                        ret.result = true;
                        ret.msg = $"[{orderno}]已具备BOM</br>";
                        ret.orderno = orderno;
                    }
                    else
                    {
                        ret.result = false;
                        ret.msg = $"<span style='color:red;'>[{orderno}]未维护BOM</span></br>";
                        ret.orderno = orderno;
                    }
                }
                return ret;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public pp_zpjh Get_OrdrInfo(string orderno)
        {
            try
            {
                orderno = orderno.Replace("\r", "").Replace("\n", "").Trim();
                StringBuilder sql = new StringBuilder();
                sql.Append("select zpjhh, order_no, scx, xh, scddlx, scsl, scsj, jhsj, ztbm, jx, first_flag,");
                sql.Append("scbz, zt, khdm, khpch, khlsh, jhh, xshh, xsbz, xssx, jtdh, yzpsl, jdcqyy, cqyy, ");
                sql.Append("cqyy_lrsj, jypyb, jypyb2, cpyhjg, yhph, udcg, sapzt, gc, lrsj, yjhh, yxshh, zjxxsl,");
                sql.Append("bzxxsl, rksl, cksl, yksl, zjxxsl_sj, bzxxsl_sj, rksl_sj, cksl_sj, scrq, yksl_sj, shjzt,");
                sql.Append("lshsc, jhlb, plan_type, tsdz, sc_jhc, sc_fxh, qslsh, jslsh, write_req, seq_length, non_series,");
                sql.Append("js_qsh, js_jsh, sjscrq, xsfhrq, ddcjsj, ddcjrq, ddshsj, ddshrq, pxbj, jypyb_sl, jypyb2_sl, sj_scrq,");
                sql.Append("jd_bm, jd_bz, write_flg, bsxcpm, yqjhrq, cljhrq, ychf, write_exc, dcyy, zrbm, yqscsj, jj_bj, jj_bz, csmc,");
                sql.Append("bjmc, yjscrq, jd_lrsj, zhtbsj FROM pp_zpjh where replace(order_no,chr(13)||chr(10),'') = :orderno");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<pp_zpjh>(sql.ToString(), new { orderno = orderno.Replace("\r", "").Replace("\n", "").Trim() }).FirstOrDefault();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public sys_order_jy_result HasGylx(string orderno)
        {
            try
            {
                orderno = orderno.Replace("\r", "").Replace("\n", "").Trim();
                sys_order_jy_result ret = new sys_order_jy_result();
                StringBuilder sql = new StringBuilder();
                sql.Append("select count(ta.jx_no) FROM zxjc_gylx ta,pp_zpjh tb where ta.jx_no = tb.jx and replace(tb.order_no,chr(13)||chr(10),'') = :orderno ");
                using (var db = new OracleConnection(ConString))
                {
                    var isbom = db.ExecuteScalar<int>(sql.ToString(), new { orderno = orderno.Replace("\r", "").Replace("\n", "").Trim() });
                    if (isbom > 0)
                    {
                        ret.result = true;
                        ret.msg = $"[{orderno}]已具备工艺路线</br>";
                        ret.orderno = orderno;
                    }
                    else
                    {
                        ret.result = false;
                        ret.msg = $"<span style='color:red;'>[{orderno}]未维护工艺路线</span></br>";
                        ret.orderno = orderno;
                    }
                }
                return ret;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool IsOrderJy(string orderno)
        {
            try
            {
                orderno = orderno.Replace("\r", "").Replace("\n", "").Trim();
                StringBuilder sql = new StringBuilder();
                sql.Append("select count(*) FROM zxjc_order_jy where replace(order_no,chr(13)||chr(10),'') = :orderno");
                //
                StringBuilder sqljy = new StringBuilder();
                sqljy.Append("insert into zxjc_order_jy ");
                sqljy.Append(" (order_no, qdjy, gdbomjy, gylxjy, kzbmjy, ptjxjy, status, lrsj, scx) ");
                sqljy.Append(" values ");
                sqljy.Append(" (:order_no, :qdjy, :gdbomjy, :gylxjy, 'N', 'N', :status, sysdate, :scx )");
                //
                StringBuilder sqlzpjh = new StringBuilder();
                sqlzpjh.Append("select scx, jx, ztbm from pp_zpjh where replace(order_no,chr(13)||chr(10),'') = :orderno and rownum = 1");
                using (var db = new OracleConnection(ConString))
                {
                    var isok = db.ExecuteScalar<int>(sql.ToString(), new { orderno = orderno.Replace("\r", "").Replace("\n", "").Trim() });
                    if(isok== 0) {
                        pp_zpjh zpjhobj = db.Query<pp_zpjh>(sqlzpjh.ToString(), new {orderno= orderno.Replace("\r", "").Replace("\n", "").Trim() }).FirstOrDefault();
                        db.Execute(sqljy.ToString(), new {
                            order_no = orderno,
                            qdjy = "N",
                            gdbomjy = "N",
                            gylxjy="N",
                            status="未校验",
                            scx = zpjhobj.scx
                        });
                    }
                    return isok>0;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public sys_order_jy_result WQT(string orderno)
        {
            try
            {
                orderno = orderno.Replace("\r", "").Replace("\n", "").Trim();
                StringBuilder sql = new StringBuilder();
                sql.Append("update ZXJC_ORDER_JY set qdjy = :qt where replace(order_no,chr(13)||chr(10),'') = :orderno ");
                IsOrderJy(orderno);
                using (var db = new OracleConnection(ConString))
                {
                    var ret = db.Execute(sql.ToString(), new { orderno = orderno.Replace("\r", "").Replace("\n", "").Trim(), qt = "N" });
                    Update_JyStatus(orderno.Replace("\r", "").Replace("\n", "").Trim());
                    return new sys_order_jy_result()
                    {
                        result = false,
                        msg = $"<span style='color:red;'>[{orderno}]未齐套</span></br>",
                        orderno = orderno,
                    };
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public sys_order_jy_result YQT(string orderno)
        {
            try
            {
                orderno = orderno.Replace("\r", "").Replace("\n", "").Trim();
                StringBuilder sql = new StringBuilder();
                sql.Append("update ZXJC_ORDER_JY set qdjy = :qt where replace(order_no,chr(13)||chr(10),'') = :orderno ");
                IsOrderJy(orderno);
                using (var db = new OracleConnection(ConString))
                {
                    var ret = db.Execute(sql.ToString(), new { orderno = orderno.Replace("\r", "").Replace("\n", "").Trim(), qt = "Y" });
                    Update_JyStatus(orderno);
                    return new sys_order_jy_result()
                    {
                        result = false,
                        msg = $"[{orderno}]已齐套</br>",
                        orderno = orderno,
                    };
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public sys_order_jy_result Set_OrderJy_Gylx(string orderno)
        {
            try
            {
                orderno = orderno.Replace("\r", "").Replace("\n", "").Trim();
                StringBuilder sql = new StringBuilder();
                sql.Append("update ZXJC_ORDER_JY set gylxjy = :gylx where replace(order_no,chr(13)||chr(10),'') = :orderno ");
                IsOrderJy(orderno);
                var result = HasGylx(orderno);
                using (var db = new OracleConnection(ConString))
                {
                    if (result.result)
                    {
                        db.Execute(sql.ToString(), new { orderno = orderno.Replace("\r", "").Replace("\n", "").Trim(), gylx = "Y" });
                    }
                    else
                    {
                        db.Execute(sql.ToString(), new { orderno = orderno.Replace("\r", "").Replace("\n", "").Trim(), gylx = "N" });
                    }
                    Update_JyStatus(orderno);
                    return result;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public sys_order_jy_result Set_OrderJy_BOM(string orderno)
        {
            try
            {
                orderno = orderno.Replace("\r", "").Replace("\n", "").Trim();
                StringBuilder sql = new StringBuilder();
                sql.Append("update ZXJC_ORDER_JY set gdbomjy = :bom where replace(order_no,chr(13)||chr(10),'') = :orderno ");
                IsOrderJy(orderno);
                var result = HasBOM(orderno);
                using (var db = new OracleConnection(ConString))
                {
                    if (result.result)
                    {
                        db.Execute(sql.ToString(), new { orderno = orderno.Replace("\r", "").Replace("\n", "").Trim(), bom = "Y" });
                    }
                    else
                    {
                        db.Execute(sql.ToString(), new { orderno = orderno.Replace("\r", "").Replace("\n", "").Trim(), bom = "N" });
                    }
                    Update_JyStatus(orderno.Replace("\r", "").Replace("\n", "").Trim());
                    return result;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Update_JyStatus(string orderno)
        {
            try
            {
                orderno = orderno.Replace("\r", "").Replace("\n", "").Trim();
                StringBuilder sql = new StringBuilder();
                StringBuilder sqlcheck = new StringBuilder();
                sqlcheck.Append("select count(*) from ZXJC_ORDER_JY where replace(order_no,chr(13)||chr(10),'') = :orderno and qdjy='Y' and gdbomjy='Y' and gylxjy='Y' ");
                sql.Append("update ZXJC_ORDER_JY set status = '已完成校验' where replace(order_no,chr(13)||chr(10),'') = :orderno and qdjy='Y' and gdbomjy='Y' and gylxjy='Y' ");
                using (var db = new OracleConnection(ConString))
                {
                    var cnt = db.ExecuteScalar<int>(sqlcheck.ToString(), new { orderno = orderno.Replace("\r", "").Replace("\n", "").Trim() });
                    if (cnt > 0)
                    {
                        db.Execute(sql.ToString(), new { orderno = orderno.Replace("\r", "").Replace("\n", "").Trim() });
                    }
                    else
                    {
                        db.Execute("update ZXJC_ORDER_JY set status = '未完成校验' where replace(order_no,chr(13)||chr(10),'') = :orderno", new { orderno = orderno });
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

using Dapper;
using Oracle.ManagedDataAccess.Client;
using System.Collections.Generic;
using System.Text;
using ZDMesInterfaces.LBJ.BHDGL;
using ZDMesModels;
using ZDMesModels.LBJ;

namespace ZDMesServices.LBJ.BHDGL
{
    public class Lbj4MBhdDealService : OracleBaseFixture, IDeal4MBHD
    {
        public Lbj4MBhdDealService(string constr) : base(constr)
        {
        }

        public IEnumerable<lbj_qms_4mbhd> Get_Bhzx_BHD_List(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                sql.Append($"select id, bmbm, scx, scxzx, cjr, cjrmc, cjsj, jt, cpxh, cpmc, bhbw, gzxx, fsddbh, fxddbh, zssl, yzpcl, r, j, l, f, h, c, tsff, qrff, djffry, djffrymc, cxtzry, cxtzrymc, qtbz, czygsdd, czyscsj, czypdjg, czyczr, czyczrmc, czyczsj, scbzgsdd, scbzscsj, scbzpdjg, scbzczr, scbzczrmc, scbzczsj, xcxjgsdd, xcxjscsj, xcxjpdjg, xcxjczr, xcxjczrmc, xcxjczsj, rwzt, djffryqr, cxtzryqr, gcdm, gwh, trig_type as trigtype, change_type as changetype,(select scxzxmc FROM base_scxxx_jj where scx = lbj_qms_4mbhd.scx and scxzx = lbj_qms_4mbhd.scxzx and rownum =1) as scxzxmc from lbj_qms_4mbhd where rwzt='01' ");
                sql_cnt.Append($"select count(id) from lbj_qms_4mbhd where rwzt='01' ");

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
                        sql.Append(" order by cjsj desc");
                    }
                }
                using (var db = new OracleConnection(ConString))
                {
                    var q = db.Query<lbj_qms_4mbhd>(OraPager(sql.ToString()), parm.sqlparam);
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                    return q;
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public IEnumerable<lbj_qms_4mbhd> Get_Czy_BHD_List(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                sql.Append($"select id, bmbm, scx, scxzx, cjr, cjrmc, cjsj, jt, cpxh, cpmc, bhbw, gzxx, fsddbh, fxddbh, zssl, yzpcl, r, j, l, f, h, c, tsff, qrff, djffry, djffrymc, cxtzry, cxtzrymc, qtbz, czygsdd, czyscsj, czypdjg, czyczr, czyczrmc, czyczsj, scbzgsdd, scbzscsj, scbzpdjg, scbzczr, scbzczrmc, scbzczsj, xcxjgsdd, xcxjscsj, xcxjpdjg, xcxjczr, xcxjczrmc, xcxjczsj, rwzt, djffryqr, cxtzryqr, gcdm, gwh, trig_type as trigtype, change_type as changetype,(select scxzxmc FROM base_scxxx_jj where scx = lbj_qms_4mbhd.scx and scxzx = lbj_qms_4mbhd.scxzx and rownum =1) as scxzxmc from lbj_qms_4mbhd where rwzt='03' ");
                sql_cnt.Append($"select count(id) from lbj_qms_4mbhd where rwzt='03' ");

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
                        sql.Append(" order by cjsj desc");
                    }
                }
                using (var db = new OracleConnection(ConString))
                {
                    var q = db.Query<lbj_qms_4mbhd>(OraPager(sql.ToString()), parm.sqlparam);
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                    return q;
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public IEnumerable<lbj_qms_4mbhd> Get_Scbz_BHD_List(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                sql.Append($"select id, bmbm, scx, scxzx, cjr, cjrmc, cjsj, jt, cpxh, cpmc, bhbw, gzxx, fsddbh, fxddbh, zssl, yzpcl, r, j, l, f, h, c, tsff, qrff, djffry, djffrymc, cxtzry, cxtzrymc, qtbz, czygsdd, czyscsj, czypdjg, czyczr, czyczrmc, czyczsj, scbzgsdd, scbzscsj, scbzpdjg, scbzczr, scbzczrmc, scbzczsj, xcxjgsdd, xcxjscsj, xcxjpdjg, xcxjczr, xcxjczrmc, xcxjczsj, rwzt, djffryqr, cxtzryqr, gcdm, gwh, trig_type as trigtype, change_type as changetype,(select scxzxmc FROM base_scxxx_jj where scx = lbj_qms_4mbhd.scx and scxzx = lbj_qms_4mbhd.scxzx and rownum =1) as scxzxmc from lbj_qms_4mbhd where rwzt='05' ");
                sql_cnt.Append($"select count(id) from lbj_qms_4mbhd where rwzt='05' ");

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
                        sql.Append(" order by cjsj desc");
                    }
                }
                using (var db = new OracleConnection(ConString))
                {
                    var q = db.Query<lbj_qms_4mbhd>(OraPager(sql.ToString()), parm.sqlparam);
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                    return q;
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public IEnumerable<lbj_qms_4mbhd> Get_Xcxj_BHD_List(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                sql.Append($"select id, bmbm, scx, scxzx, cjr, cjrmc, cjsj, jt, cpxh, cpmc, bhbw, gzxx, fsddbh, fxddbh, zssl, yzpcl, r, j, l, f, h, c, tsff, qrff, djffry, djffrymc, cxtzry, cxtzrymc, qtbz, czygsdd, czyscsj, czypdjg, czyczr, czyczrmc, czyczsj, scbzgsdd, scbzscsj, scbzpdjg, scbzczr, scbzczrmc, scbzczsj, xcxjgsdd, xcxjscsj, xcxjpdjg, xcxjczr, xcxjczrmc, xcxjczsj, rwzt, djffryqr, cxtzryqr, gcdm, gwh, trig_type as trigtype, change_type as changetype,(select scxzxmc FROM base_scxxx_jj where scx = lbj_qms_4mbhd.scx and scxzx = lbj_qms_4mbhd.scxzx and rownum =1) as scxzxmc from lbj_qms_4mbhd where rwzt='07' ");
                sql_cnt.Append($"select count(id) from lbj_qms_4mbhd where rwzt='07'  ");

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
                        sql.Append(" order by cjsj desc");
                    }
                }
                using (var db = new OracleConnection(ConString))
                {
                    var q = db.Query<lbj_qms_4mbhd>(OraPager(sql.ToString()), parm.sqlparam);
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                    return q;
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public bool Save_ZC_BHD_Deal(lbj_qms_4mbhd entity)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("update lbj_qms_4mbhd set rwzt = '01', xcxjgsdd= :xcxjgsdd,xcxjscsj=:xcxjscsj,xcxjpdjg=:xcxjpdjg,xcxjczr=:xcxjczr,xcxjczrmc=:xcxjczrmc,scbzczsj=sysdate,tsff=:tsff,qrff=:qrff,yzpcl=:yzpcl where id = :id and rwzt='00'");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Execute(sql.ToString(), new { id = entity.id, xcxjgsdd = entity.xcxjgsdd, xcxjscsj = entity.xcxjscsj, xcxjpdjg = entity.xcxjpdjg, xcxjczr = entity.xcxjczr, xcxjczrmc = entity.xcxjczrmc, tsff = entity.tsff, qrff = entity.qrff, yzpcl = entity.yzpcl }) > 0;
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 保存变化执行
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Save_BhZx_BHD_Deal(lbj_qms_4mbhd entity)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("update lbj_qms_4mbhd set rwzt = '03', czygsdd = :czygsdd,czyscsj=:czyscsj,czypdjg=:czypdjg,czyczr=:czyczr,czyczrmc=:czyczrmc,czyczsj=sysdate,tsff=:tsff,qrff=:qrff,yzpcl=:yzpcl where id = :id and rwzt = '01' ");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Execute(sql.ToString(), new { id = entity.id, czygsdd = entity.czygsdd, czyscsj = entity.czyscsj, czypdjg = entity.czypdjg, czyczr = entity.czyczr, czyczrmc = entity.czyczrmc, tsff = entity.tsff, qrff = entity.qrff, yzpcl = entity.yzpcl }) > 0;
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public bool Save_Czy_BHD_Deal(lbj_qms_4mbhd entity)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("update lbj_qms_4mbhd set rwzt = '05', czygsdd = :czygsdd,czyscsj=:czyscsj,czypdjg=:czypdjg,czyczr=:czyczr,czyczrmc=:czyczrmc,czyczsj=sysdate,tsff=:tsff,qrff=:qrff,yzpcl=:yzpcl where id = :id and rwzt = '03'");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Execute(sql.ToString(), new { id = entity.id, czygsdd = entity.czygsdd, czyscsj = entity.czyscsj, czypdjg = entity.czypdjg, czyczr = entity.czyczr, czyczrmc = entity.czyczrmc,tsff=entity.tsff,qrff=entity.qrff,yzpcl=entity.yzpcl }) > 0;
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public bool Save_Scbz_BHD_Deal(lbj_qms_4mbhd entity)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("update lbj_qms_4mbhd set rwzt = '07',scbzgsdd= :scbzgsdd,scbzscsj=:scbzscsj,scbzpdjg=:scbzpdjg,scbzczr=:scbzczr,scbzczrmc=:scbzczrmc,scbzczsj=sysdate,tsff=:tsff,qrff=:qrff,yzpcl=:yzpcl where id = :id and rwzt = '05'");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Execute(sql.ToString(), new { id = entity.id, scbzgsdd = entity.scbzgsdd, scbzscsj = entity.scbzscsj, scbzpdjg = entity.scbzpdjg, scbzczr = entity.scbzczr, scbzczrmc = entity.scbzczrmc, tsff = entity.tsff, qrff = entity.qrff, yzpcl = entity.yzpcl }) > 0;
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public bool Save_Xcxj_BHD_Deal(lbj_qms_4mbhd entity)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("update lbj_qms_4mbhd set rwzt = '49', xcxjgsdd= :xcxjgsdd,xcxjscsj=:xcxjscsj,xcxjpdjg=:xcxjpdjg,xcxjczr=:xcxjczr,xcxjczrmc=:xcxjczrmc,scbzczsj=sysdate,tsff=:tsff,qrff=:qrff,yzpcl=:yzpcl where id = :id and rwzt = '07'");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Execute(sql.ToString(), new { id = entity.id, xcxjgsdd = entity.xcxjgsdd, xcxjscsj = entity.xcxjscsj, xcxjpdjg = entity.xcxjpdjg, xcxjczr = entity.xcxjczr, xcxjczrmc = entity.xcxjczrmc, tsff = entity.tsff, qrff = entity.qrff, yzpcl = entity.yzpcl }) > 0;
                }
            }
            catch (System.Exception)
            {

                throw;
            }
        }

    }
}

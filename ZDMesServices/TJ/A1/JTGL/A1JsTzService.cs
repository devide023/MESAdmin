using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.TJ.A1;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using ZDMesInterfaces.TJ;
using ZDMesInterfaces.Common;
using Microsoft.Extensions.Primitives;
using ZDMesModels;
using System.Net.Http;
using ZDMesServices.TJ.Common;
using ZDMesServices.Common;

namespace ZDMesServices.TJ.A1.JTGL
{
    public class A1JsTzService:BaseDao<zxjc_t_jstc>, IJTFPSCX,IA1MyDoc,IA1JtFpzt
    {
        private UserUtilService uservice;
        public A1JsTzService(string constr):base(constr)
        {
            uservice = new UserUtilService(constr);
        }

        public override IEnumerable<zxjc_t_jstc> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                StringBuilder sql_read = new StringBuilder();
                //
                sql_read.Append($",(select count(id) FROM mes_pdm_jstz_yd where jcbh = zxjc_t_jstc.jcbh and zbid = zxjc_t_jstc.scx and ydrid = {uservice.CurrentUser.id}) as readcnt,(select istogwh from zxjc_t_jstcfp_group where zbid = zxjc_t_jstc.scx) as istogwh ");
                StringBuilder sql_where = new StringBuilder();
                sql.Append(" select * from ( ");
                sql.Append($"select * from (select scx,jtly,jtid,wjfl,jcbh,jcmc,wjlj,scry,scsj,yxqx2,fpr,fp_sj as fpsj,fp_flg as fpflg,lrr,lrsj");
                sql.Append(sql_read);
                sql.Append(" from zxjc_t_jstc where fp_flg ='N' )  where readcnt=0 ");
                sql.Append(" union ");
                sql.Append($"select * from (select scx,jtly,jtid,wjfl,jcbh,jcmc,wjlj,scry,scsj,yxqx2,fpr,fp_sj as fpsj,fp_flg as fpflg,lrr,lrsj");
                sql.Append(sql_read);
                sql.Append(" from zxjc_t_jstc where fp_flg ='N' )  where readcnt > 0  and istogwh = 'Y' ");
                sql.Append(") zxjc_t_jstc where 1=1 ");
                sql.Append(" and (select count(jcbh) from zxjc_t_jstcfp_yfp where jcbh = zxjc_t_jstc.jcbh) = 0 ");
                //
                sql_cnt.Append($"select count(*) from ( select * from (select scx,jtly,jtid,wjfl,jcbh,jcmc,wjlj,scry,scsj,yxqx2,fpr,fp_sj as fpsj,fp_flg as fpflg,lrr,lrsj");
                sql_cnt.Append(sql_read);
                sql_cnt.Append(" from zxjc_t_jstc where fp_flg ='N' )  where readcnt=0 ");
                sql_cnt.Append(" union ");
                sql_cnt.Append(" select * from( select scx,jtly,jtid,wjfl,jcbh,jcmc,wjlj,scry,scsj,yxqx2,fpr,fp_sj as fpsj,fp_flg as fpflg,lrr,lrsj ");
                sql_cnt.Append(sql_read);
                sql_cnt.Append(" from zxjc_t_jstc where fp_flg ='N' )  where readcnt > 0  and istogwh = 'Y' ");
                sql_cnt.Append(" ) zxjc_t_jstc  where 1=1 ");
                sql_cnt.Append(" and (select count(jcbh) from zxjc_t_jstcfp_yfp where jcbh = zxjc_t_jstc.jcbh) = 0 ");
                //
                sql_where.Append($" and scx in (select distinct zbid FROM zxjc_t_jstcfp_ry where usercode = '{uservice.CurrentUser.code}') ");

                sql.Append(sql_where);
                sql_cnt.Append(sql_where);

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
                        sql.Append(" order by lrsj desc ");
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

        public bool CanRemove(string jcbh)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select count(jtid) from zxjc_t_jstcfp where jtid = :jtid ");
                using (var db = new OracleConnection(ConString))
                {
                    return db.ExecuteScalar<int>(sql.ToString(), new { jtid = jcbh }) == 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// PDM技通列表
        /// </summary>
        /// <param name="parm"></param>
        /// <param name="resultcount"></param>
        /// <returns></returns>
        public IEnumerable<zxjc_t_jstc> Get_PDM_JSTZ_List(sys_page parm, out int resultcount)
        {
            try
            {
                int uid = 0;
                using (var db = new OracleConnection(ConString))
                {
                    var token = ZDToolHelper.TokenHelper.GetToken;
                    var qu = db.Query<mes_user_entity>("select id,name from mes_user_entity where token = :token", new { token = token });
                    if (qu.Count() > 0)
                    {
                        uid = qu.First().id;
                    }
                    StringBuilder sql_cnt = new StringBuilder();
                    StringBuilder sql = new StringBuilder();
                    sql.Append("select * from ");
                    sql.Append(" (select jtid,jtly,wjfl,no as jcbh,name as jcmc,url as wjlj,creator as scry,jcms,");
                    sql.Append(" cjsj as scsj,expire_date as yxqx2,(select lrr from   zxjc_t_jstc where  jcbh = v_zxjc_t_jstc.no and  rownum =1) as fpr,");
                    sql.Append(" (select lrsj from zxjc_t_jstc where jcbh = v_zxjc_t_jstc.no and rownum = 1) as fpsj,");
                    sql.Append(" (select  wmsys.wm_concat(distinct bb.zbmc) from zxjc_t_jstc aa,ZXJC_T_JSTCFP_GROUP bb where aa.scx=bb.zbid and aa.jcbh = v_zxjc_t_jstc.no ) as fpscxlist,");
                    sql.Append(" (select decode(count(jcbh),0,'N','Y') from   zxjc_t_jstc where  jcbh = v_zxjc_t_jstc.no) as fpflg ");
                    if (uid != 0)
                    {
                        sql.Append(" ,(select count(*) from mes_pdm_jstz_yd where jcbh = no and ydrid =" + uid + ") as rcnt ");
                    }
                    else
                    {
                        sql.Append(" ,0 as rcnt ");
                    }
                    sql.Append(" FROM v_zxjc_t_jstc) m where fpflg = 'N' ");
                    sql_cnt.Append("select count(m.jcbh) from ( ");
                    sql_cnt.Append(" select jtly,wjfl,no as jcbh,name as jcmc,url as wjlj,creator as scry,cjsj as scsj,expire_date as yxqx2,jcms,");
                    sql_cnt.Append(" (select lrr from zxjc_t_jstc where jcbh = v_zxjc_t_jstc.no and rownum =1) as fpr,");
                    sql_cnt.Append(" (select lrsj from zxjc_t_jstc where jcbh = v_zxjc_t_jstc.no and rownum = 1) as fpsj,");
                    sql_cnt.Append(" (select  wmsys.wm_concat(distinct bb.zbmc) from zxjc_t_jstc aa,ZXJC_T_JSTCFP_GROUP bb where aa.scx=bb.zbid and aa.jcbh = v_zxjc_t_jstc.no ) as fpscxlist,");
                    sql_cnt.Append(" (select decode(count(jcbh),0,'N','Y') from   zxjc_t_jstc where  jcbh = v_zxjc_t_jstc.no) as fpflg ");
                    sql_cnt.Append(" FROM v_zxjc_t_jstc) m ");
                    sql_cnt.Append(" where fpflg='N' ");
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
                        if (!string.IsNullOrWhiteSpace(parm.sqlconfig.sql_orderby))
                        {
                            sql.Append("order by m.scsj desc nulls last");
                        }
                    }

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

        public bool Read_PDM_JSTZ(string jcbh)
        {
            try
            {
                var token = ZDToolHelper.TokenHelper.GetToken;                
                StringBuilder sql = new StringBuilder();
                sql.Append("insert into mes_pdm_jstz_yd ");
                sql.Append("(jcbh, ydr, ydsj, ydrid,usercode,zbid,jtid) ");
                sql.Append(" select :jcbh, :ydr, sysdate, :ydrid,:usercode,:zbid,:jtid from dual where not exists (select * from mes_pdm_jstz_yd where jcbh = :jcbh and ydrid= :ydrid and zbid = :zbid )");
                using (var db = new OracleConnection(ConString))
                {
                    string zbid = string.Empty;
                    string jtid = string.Empty;
                    var jtids = db.Query<zxjc_t_jstc>("select scx,jtid from zxjc_t_jstc where jcbh = :jcbh ", new { jcbh = jcbh });
                    var q = db.Query<mes_user_entity>("select id,code,name from mes_user_entity where token = :token", new { token = token });
                    foreach (var tzobj in jtids)
                    {
                        zbid = tzobj.scx;
                        jtid = tzobj.jtid;
                        if (q.Count() > 0)
                        {
                            int uid = q.First().id;
                            string uname = q.First().name;
                            string ucode = q.First().code;
                            db.Execute(sql.ToString(), new { jcbh = jcbh, ydr = uname, ydrid = uid, usercode = ucode, zbid = zbid, jtid = jtid });
                        }
                    }
                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 我的技术通知列表
        /// </summary>
        /// <param name="parm"></param>
        /// <param name="resultcount"></param>
        /// <returns></returns>
        public IEnumerable<zxjc_t_jstc> Get_MyJstz_List(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                StringBuilder sql_read = new StringBuilder();
                //
                sql_read.Append($",(select count(id) FROM mes_pdm_jstz_yd where jcbh = zxjc_t_jstc.jcbh and zbid = zxjc_t_jstc.scx and ydrid = {uservice.CurrentUser.id}) as readcnt ");
                
                sql.Append(" select * from ");
                sql.Append(" (select distinct jcbh, jcmc, jcms, wjlj, jwdx, scry, scsj, yxqx1, yxqx2, fp_flg as fpflg, fp_sj as fpsj, fpr, wjfl, lrr, lrsj, jtly");
                sql.Append(sql_read);
                sql.Append(" FROM zxjc_t_jstc where  ");
                sql.Append($" scx in (select distinct zbid FROM zxjc_t_jstcfp_ry where usercode = '{uservice.CurrentUser.code}') ");
                sql.Append(" ) zxjc_t_jstc");
                sql.Append(" where  readcnt = 0 ");
                //
                sql_cnt.Append(" select count(*) from ");
                sql_cnt.Append(" (select distinct jcbh, jcmc, jcms, wjlj, jwdx, scry, scsj, yxqx1, yxqx2, fp_flg as fpflg, fp_sj as fpsj, fpr, wjfl, lrr, lrsj, jtly");
                sql_cnt.Append(sql_read);
                sql_cnt.Append(" FROM zxjc_t_jstc where  ");
                sql_cnt.Append($" scx in (select distinct zbid FROM zxjc_t_jstcfp_ry where usercode = '{uservice.CurrentUser.code}') ");
                sql_cnt.Append(" ) zxjc_t_jstc");
                sql_cnt.Append(" where  readcnt = 0 ");

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
                        sql.Append(" order by lrsj desc ");
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
        /// <summary>
        /// PDM分配列表
        /// </summary>
        /// <param name="parm"></param>
        /// <param name="resultcount"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<zxjc_t_jstc> Get_PDMFP_List(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql_ydrs = new StringBuilder();
                sql_ydrs.Append("select ydr from MES_PDM_JSTZ_YD where jtid = :jtid");
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                //
                sql.Append("select jtid, jcbh, jcmc, jcms, wjlj, jwdx, scry, scpc, scsj, yxqx1, yxqx2, gcdm, fp_flg as fpflg, fp_sj as fpsj, fpr, wjfl, scx, lrr, lrsj, jtly");
                sql.Append(" from zxjc_t_jstc where 1=1 ");
                sql_cnt.Append("select count(jtid) from zxjc_t_jstc where 1=1 ");
                //
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
                        sql.Append(" order by lrsj desc ");
                    }
                }
                using (var db = new OracleConnection(ConString))
                {
                    var qs = db.Query<zxjc_t_jstc>(OraPager(sql.ToString()), parm.sqlparam);
                    foreach (var q in qs)
                    {
                        string t_ydrs = string.Empty;
                        var rdrs = db.Query<string>(sql_ydrs.ToString(), new { jtid = q.jtid });
                        rdrs.ToList().ForEach(t => t_ydrs = t + "," + t_ydrs);
                        q.ydrs = t_ydrs;
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

        public bool Set_JtFpZt(string jtbh)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("insert into ZXJC_T_JSTC_QT(jcbh) values (:jcbh) ");
                using (var db = new OracleConnection(ConString))
                {
                  return  db.Execute(sql.ToString(), new { jcbh = jtbh })>0;
                }
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool UnSet_JtFpZt(string jtbh)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("delete from ZXJC_T_JSTC_QT where jcbh = :jcbh ");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Execute(sql.ToString(), new { jcbh = jtbh }) > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Set_JtFpYfpGwh(string jtbh)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("insert into zxjc_t_jstcfp_yfp(jcbh,lrr) values (:jcbh,:lrr) ");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Execute(sql.ToString(), new { jcbh = jtbh,lrr = uservice.CurrentUser.name }) > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool UnSet_JtFpYfpGwh(string jtbh)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("delete from zxjc_t_jstcfp_yfp where jcbh = :jcbh ");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Execute(sql.ToString(), new { jcbh = jtbh }) > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

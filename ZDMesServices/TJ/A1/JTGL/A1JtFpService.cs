using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.TJ;
using ZDMesModels.TJ.A1;
using Oracle.ManagedDataAccess.Client;
using Dapper;
using ZDMesModels;
using ZDMesServices.Common;

namespace ZDMesServices.TJ.A1.JTGL
{
    public class A1JtFpService:BaseDao<zxjc_t_jstcfp>,IJTFP
    {
        private UserUtilService _us;
        public A1JtFpService(string constr):base(constr)
        {
            _us = new UserUtilService(constr);
        }

        public override IEnumerable<zxjc_t_jstcfp> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                sql.Append($"select id, jtid,(select jtly from zxjc_t_jstc where jcbh = zxjc_t_jstcfp.jtid and scx = zxjc_t_jstcfp.scx and rownum=1) as jtly,");
                sql.Append(" (select wjlj from zxjc_t_jstc where jcbh = zxjc_t_jstcfp.jtid and scx = zxjc_t_jstcfp.scx  and rownum=1) as wjlj,");
                sql.Append(" (select jcbh from zxjc_t_jstc where jcbh = zxjc_t_jstcfp.jtid and scx = zxjc_t_jstcfp.scx and rownum=1) as jcbh,");
                sql.Append(" (select jcmc from zxjc_t_jstc where jcbh = zxjc_t_jstcfp.jtid and scx = zxjc_t_jstcfp.scx and rownum=1) as jcmc,");
                sql.Append(" (select jcms from zxjc_t_jstc where jcbh = zxjc_t_jstcfp.jtid and scx = zxjc_t_jstcfp.scx  and rownum=1) as jcms,");
                sql.Append(" gcdm, scx, gwh, jx_no as jxno, status_no as statusno, bz, lrr1, lrsj1,");
                sql.Append(" (select lrr from zxjc_t_jstc where jcbh = zxjc_t_jstcfp.jtid and scx = zxjc_t_jstcfp.scx and rownum = 1) as lrr2,");
                sql.Append(" (select lrsj from zxjc_t_jstc where jcbh = zxjc_t_jstcfp.jtid and scx = zxjc_t_jstcfp.scx and rownum = 1)as lrsj2 ");
                sql.Append($" from zxjc_t_jstcfp where scx in (select distinct zbid from   zxjc_t_jstcfp_ry where  usercode = '{_us.CurrentUser.code}') ");
                sql_cnt.Append($" select count(id) from zxjc_t_jstcfp where scx in (select distinct zbid from   zxjc_t_jstcfp_ry where  usercode = '{_us.CurrentUser.code}')  ");

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
                        sql.Append(" order by lrsj1 desc,gwh asc,jx_no asc,status_no asc ");
                    }
                }
                using (var db = new OracleConnection(ConString))
                {
                    var q = db.Query<zxjc_t_jstcfp>(OraPager(sql.ToString()), parm.sqlparam);
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                    return q;
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
                sql.Append(" update zxjc_t_jstc ");
                sql.Append(" set    fp_flg = 'N',");
                sql.Append("       fp_sj = null,");
                sql.Append("       fpr = null ");
                sql.Append(" where  fp_flg = 'Y'");
                sql.Append(" and jcbh in :jcbh ");
                sql.Append(" and    not exists");
                sql.Append(" (select * FROM zxjc_t_jstcfp where jtid = zxjc_t_jstc.jcbh)");
                StringBuilder sql1 = new StringBuilder();
                sql1.Append("delete from zxjc_t_jstcfp where id in :ids");
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        db.Open();
                        using (var trans = db.BeginTransaction())
                        {
                            try
                            {
                                var jcbhlist = entitys.Select(t => t.jcbh).Distinct().ToList();
                                var ids = entitys.Select(t => t.id).Distinct().ToList();
                                db.Execute(sql1.ToString(), new { ids = ids }, trans);
                                db.Execute(sql.ToString(), new { jcbh = jcbhlist }, trans);
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

        public override int Add(IEnumerable<zxjc_t_jstcfp> entitys)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("update zxjc_t_jstc set fp_flg='Y',fp_sj=sysdate,fpr=:fpr where fp_flg='N' and jcbh in :jcbh");
                StringBuilder sql2 = new StringBuilder();
                sql2.Append("insert into zxjc_t_jstcfp ");
                sql2.Append(" (id,jtid, gcdm, scx, gwh, jx_no, status_no, bz, lrr1, lrsj1)");
                sql2.Append(" values");
                sql2.Append(" (:id,:jtid, :gcdm, :scx, :gwh, :jxno, :statusno, :bz, :lrr1, :lrsj1)");
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        db.Open();
                        using (var trans = db.BeginTransaction())
                        {
                            try
                            {
                                var jcbhlist = entitys.Select(t => t.jtid).Distinct().ToList();
                                var fpr = entitys.Select(t => t.lrr1).First();
                                foreach (var item in entitys)
                                {
                                    db.Execute(sql2.ToString(), item, trans);
                                }
                                db.Execute(sql.ToString(), new { jcbh = jcbhlist,fpr= fpr }, trans);
                                trans.Commit();
                                return entitys.Count();
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

        public List<zxjc_t_jstcfp> IsDistribute(List<zxjc_t_jstcfp> list)
        {
            List<zxjc_t_jstcfp> retlist = new List<zxjc_t_jstcfp>();    
               StringBuilder sql = new StringBuilder();
            sql.Append("select count(id) from ZXJC_T_JSTCFP where jtid = :jtid and gwh=:gwh and jx_no=:jxno and status_no=:statusno");
            using (var db = new OracleConnection(ConString))
            {
                foreach (var item in list)
                {
                   var cnt = db.ExecuteScalar<int>(sql.ToString(), new { jtid = item.jtid, gwh=item.gwh, jxno = item.jxno, statusno = item.statusno });
                    if (cnt > 0)
                    {
                        retlist.Add(item);
                    }    
                }
            }
            return retlist;
        }

        public List<zxjc_t_jstc_scx> IsJtToScx(List<zxjc_t_jstc_scx> list)
        {
            List<zxjc_t_jstc_scx> retlist = new List<zxjc_t_jstc_scx>();
            StringBuilder sql = new StringBuilder();
            sql.Append("select count(jtid) from zxjc_t_jstc where jcbh=:jcbh and scx=:scx");
            using (var db = new OracleConnection(ConString))
            {
                foreach (var item in list)
                {
                    var cnt = db.ExecuteScalar<int>(sql.ToString(), new { jcbh = item.jcbh, scx=item.scx });
                    if (cnt > 0)
                    {
                        retlist.Add(item);
                    }
                }
            }
            return retlist;
        }

        public bool Jstz_To_Scx(List<zxjc_t_jstc_scx> list)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select no,name,url,expire_date FROM ZSDL_JSTZ@ln_pdm98 where no=:jtno");
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        db.Open();
                        InitDB(db);
                        using (var trans = db.BeginTransaction())
                        {
                            try
                            {
                                foreach (var item in list)
                                {
                                    var q = db.Query<dynamic>(sql.ToString(), new { jtno = item.jcbh });
                                    if (q.Count() > 0)
                                    {
                                        DateTime yxqjs = default;
                                        var pdmobj = q.First();
                                        DateTime.TryParse(pdmobj.EXPIRE_DATE, out yxqjs);
                                        zxjc_t_jstc jstcentity = new zxjc_t_jstc();
                                        jstcentity.jcbh = item.jcbh;
                                        jstcentity.scry = item.lrr;
                                        jstcentity.scx = item.scx;
                                        jstcentity.yxqx2 = yxqjs;
                                        jstcentity.fpflg = "N";
                                        jstcentity.jcmc = pdmobj.URL;
                                        jstcentity.jcms = pdmobj.NAME;
                                        jstcentity.wjlj = pdmobj.URL;
                                        jstcentity.scsj = DateTime.Now;
                                        jstcentity.lrr = item.lrr;
                                        jstcentity.jtly = 1;
                                        Db.Insert<zxjc_t_jstc>(jstcentity, trans);
                                    }
                                    //Db.Insert<zxjc_t_jstc_scx>(item, trans);
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
            finally
            {
                Db?.Dispose();
            }
        }
    }
}

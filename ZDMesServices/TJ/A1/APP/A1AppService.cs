using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZDMesInterfaces.TJ;
using ZDMesModels.TJ.A1;

namespace ZDMesServices.TJ.A1.APP
{
    public class A1AppService : OracleBaseFixture, IA1App
    {
        public A1AppService(string constr) : base(constr)
        {
        }

        public IEnumerable<zxjc_jcbill> Get_JCBills(zxjc_jcbill bill)
        {
            try
            {
                DynamicParameters p = new DynamicParameters();
                StringBuilder sql = new StringBuilder();
                sql.Append("select id, scx, jclx, jcrq, zcbh, sbgly, jcjg, lrr, lrsj,jdqr,jdrqsj from ZXJC_JCBILL where 1=1");
                if (!string.IsNullOrEmpty(bill.scx))
                {
                    sql.Append(" and scx = :scx ");
                    p.Add(":scx", bill.scx);
                }
                if (bill.jcrq != null)
                {
                    sql.Append(" and jcrq = :jcrq ");
                    p.Add(":jcrq", bill.jcrq, System.Data.DbType.Date, System.Data.ParameterDirection.Input);
                }
                if (!string.IsNullOrEmpty(bill.jclx))
                {
                    sql.Append(" and jclx like :jclx ");
                    p.Add(":jclx", "%" + bill.jclx + "%");
                }
                if (!string.IsNullOrEmpty(bill.zcbh))
                {
                    sql.Append(" and zcbh = :zcbh ");
                    p.Add(":zcbh", bill.zcbh);
                }
                StringBuilder sqlmx = new StringBuilder();
                sqlmx.Append("select id,scx,gwh,jclx,jclb,xh,jcyq,jcjg,jcz,bzz,bzsx,bzxx,bz FROM  zxjc_jcmx where jcjg_id = :billid");
                using (var db = new OracleConnection(ConString))
                {
                    var list = db.Query<zxjc_jcbill>(sql.ToString(), p);
                    foreach (var item in list)
                    {
                        item.jcmxlist = db.Query<zxjc_jcmx>(sqlmx.ToString(), new { billid = item.id }).ToList();
                    }
                    return list;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<zxjc_jclx> Get_JCLX(sys_jclx_form parm)
        {
            try
            {
                DynamicParameters p = new DynamicParameters();
                StringBuilder sql = new StringBuilder();
                sql.Append("select ta.gcdm, ta.scx, ta.jclx, tb.scxmc FROM   zxjc_jclx ta, tj_base_scxxx tb where ta.scx = tb.scx and ta.scbz = 'N' ");
                if (!string.IsNullOrEmpty(parm.key))
                {
                    sql.Append(" and ta.jclx like :key ");
                    p.Add(":key", "%" + parm.key + "%", System.Data.DbType.String, System.Data.ParameterDirection.Input);
                }
                if (!string.IsNullOrEmpty(parm.scx))
                {
                    sql.Append(" and ta.scx like :scx ");
                    p.Add(":scx", parm.scx, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                }
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<zxjc_jclx>(sql.ToString(), p);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<zxjc_jcjcxx> Get_JCXM(sys_jcmx_form parm)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    DynamicParameters p = new DynamicParameters();
                    DynamicParameters pt = new DynamicParameters();
                    StringBuilder sql = new StringBuilder();
                    StringBuilder sqltemp = new StringBuilder();
                    //查询人员信息
                    StringBuilder sqlryxx = new StringBuilder();
                    sqlryxx.Append("select user_name FROM zxjc_ryxx where tel = :lrr ");
                    //手机号查询人员信息
                    string lrrname = string.Empty;
                    var ryxxq = db.Query<string>(sqlryxx.ToString(), new { lrr = parm.lrr });
                    if (ryxxq.Count() > 0)
                    {
                        lrrname = ryxxq.First();
                    }
                    else
                    {
                        lrrname = parm.lrr;
                    }

                    if (!string.IsNullOrEmpty(parm.jclx))
                    {
                        sqltemp.Append(" and ta.jclx = :jclx");
                        p.Add(":jclx", parm.jclx);
                    }
                    if (!string.IsNullOrEmpty(parm.scx))
                    {
                        sqltemp.Append(" and ta.scx = :scx");
                        p.Add(":scx", parm.scx);
                    }
                    sql.Append(" select t1.id,t1.scx,t1.gwh,t1.jclb,t1.jcyq,t1.xh,t1.jcd,t2.zjcjg,t2.jcjg from ");
                    sql.Append(" (select ta.id,ta.gcdm,ta.scx,ta.gwh,ta.jclb,ta.jcyq,ta.xh,ta.jcd ");
                    sql.Append(" FROM zxjc_jcjcxx ta  ");
                    sql.Append(" where  ta.scbz = 'N' ");
                    sql.Append(sqltemp);
                    if (!string.IsNullOrEmpty(parm.bc))
                    {
                        sql.Append(" and ta.jcd = :bc");
                        p.Add(":bc", parm.bc);
                    }
                    sql.Append(") t1,");
                    sql.Append(" ( ");
                    sql.Append(" select tb.jcjg,tb.jcxid FROM zxjc_jcbill ta, zxjc_jcmx tb ");
                    sql.Append(" where ta.id = tb.jcjg_id ");
                    sql.Append(" and ta.jcrq = trunc(sysdate) ");
                    sql.Append(sqltemp);
                    if (!string.IsNullOrEmpty(lrrname))
                    {
                        sql.Append(" and ta.lrr = :lrr");
                        p.Add(":lrr", lrrname);
                    }
                    if (!string.IsNullOrEmpty(parm.zcbh))
                    {
                        sql.Append(" and ta.zcbh = :sbbh");
                        p.Add(":sbbh", parm.zcbh);
                    }
                    else
                    {
                        sql.Append(" and ta.zcbh = :sbbh");
                        p.Add(":sbbh", "");
                    }
                    sql.Append(" ) t2 ");
                    sql.Append(" where t1.id = t2.jcxid(+) ");
                    sql.Append(" order by t1.jclb asc, t1.xh asc ");


                    var list = db.Query<zxjc_jcjcxx>(sql.ToString(), p);
                    return list;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<zxjc_jcjcxx> Get_Jcxm_ByZcbh(sys_app_dj_form form)
        {
            try
            {
                DynamicParameters p = new DynamicParameters();
                StringBuilder sql = new StringBuilder();
                sql.Append("select t1.id, t1.scx,(select scxmc from tj_base_scxxx where scx = t1.scx) as scxmc, t1.gwh, t1.jclb, t1.jcyq, t1.xh, t1.jcd, t2.jcjg,t2.jdqr ");
                sql.Append(" from(select tc.id, tc.gcdm, tc.scx, tc.gwh, tc.jclb, tc.jcyq, tc.xh, tc.jcd ");
                sql.Append("          FROM   zxjc_jcsbxx ta, zxjc_jclx tb, zxjc_jcjcxx tc ");
                sql.Append("          where  ta.jcid = tb.id ");
                sql.Append("          and    tb.jclx = tc.jclx ");
                sql.Append("          and    tc.scbz = 'N' ");
                if (!string.IsNullOrEmpty(form.zcbh))
                {
                    p.Add(":sbbh", form.zcbh);
                    sql.Append("and    ta.sbbh = :sbbh ");
                }
                if (!string.IsNullOrEmpty(form.bc))
                {
                    p.Add(":bc", form.bc);
                    sql.Append("and    tc.jcd = :bc ");
                }
                sql.Append("  ) t1, (select ta.jdqr,ta.jcjg as zjcjg,tb.jcjg, tb.jcxid ");
                sql.Append("          FROM   zxjc_jcbill ta, zxjc_jcmx tb ");
                sql.Append("          where  ta.id = tb.jcjg_id ");
                if (!string.IsNullOrEmpty(form.zcbh))
                {
                    p.Add(":sbbh", form.zcbh);
                    sql.Append("and    ta.zcbh = :sbbh ");
                }
                sql.Append("          and ta.jcrq = trunc(sysdate)) t2 ");
                sql.Append(" where  t1.id = t2.jcxid(+) ");
                sql.Append(" order by t1.jclb asc, t1.xh asc ");
                using (var db = new OracleConnection(ConString))
                {
                    var list = db.Query<zxjc_jcjcxx>(sql.ToString(), p);
                    return list;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<zxjc_jcjcxx> Get_JDQR_JcxList(string billid)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select ta.*,tb.jcjg FROM ");
                sql.Append(" (  ");
                sql.Append(" select id, jclb, jcyq, xh FROM zxjc_jcjcxx where jclx= :jclx and scx = :scx  ");
                sql.Append(" ) ta,  ");
                sql.Append(" (  ");
                sql.Append(" select jcxid, jcjg FROM zxjc_jcmx where jcjg_id = :billid  ");
                sql.Append(" )  tb  ");
                sql.Append(" where ta.id = tb.jcxid(+)  ");
                sql.Append(" order by ta.jclb asc, ta.xh asc ");
                StringBuilder s = new StringBuilder();
                s.Append("select id,scx,jclx from zxjc_jcbill where id = :billid");
                //
                using (var db = new OracleConnection(ConString))
                {
                    var q = db.Query<zxjc_jcbill>(s.ToString(), new { billid = billid });
                    zxjc_jcbill bill = new zxjc_jcbill();
                    if (q.Count() > 0)
                    {
                        bill = q.FirstOrDefault();
                        var list = db.Query<zxjc_jcjcxx>(sql.ToString(), new { scx = bill.scx, jclx = bill.jclx, billid = bill.id });
                        return list;
                    }
                    else
                    {
                        List<zxjc_jcjcxx> list = new List<zxjc_jcjcxx>();
                        return list;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool JcBill_JDQR(sys_jcbill_jdqr parm)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("update zxjc_jcbill set jdqr = :jdqr,jdrqsj = sysdate where id = :billid ");
                using (var db = new OracleConnection(ConString))
                {
                    //查询人员信息
                    StringBuilder sqlryxx = new StringBuilder();
                    sqlryxx.Append("select user_name FROM zxjc_ryxx where tel = :lrr ");
                    //手机号查询人员信息
                    string lrrname = string.Empty;
                    var ryxxq = db.Query<string>(sqlryxx.ToString(), new { lrr = parm.jdqr });
                    if (ryxxq.Count() > 0)
                    {
                        lrrname = ryxxq.First();
                    }
                    else
                    {
                        lrrname = parm.jdqr;
                    }
                    return db.Execute(sql.ToString(), new { jdqr = lrrname, billid = parm.billid }) > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Save_App_JCMX(sys_app_jc_form form)
        {
            try
            {
                //查询检测类型
                StringBuilder sqljclx = new StringBuilder();
                sqljclx.Append("select tb.jclx FROM zxjc_jcsbxx ta, zxjc_jclx tb where ta.jcid = tb.id and ta.sbbh = :zcbh ");
                //查询表单数据
                StringBuilder sql = new StringBuilder();
                sql.Append("select id, scx, jclx, jcrq, zcbh, sbgly, jcjg, lrr, lrsj, jdqr, jdrqsj FROM zxjc_jcbill where scx = :scx and zcbh=:zcbh and jclx = :jclx ");
                //sql.Append(" (select tb.jclx FROM zxjc_jcsbxx ta, zxjc_jclx tb where ta.jcid = tb.id and ta.sbbh = :zcbh and rownum =1) ");
                sql.Append(" and trunc(jcrq) = trunc(:jcrq) and lrr = :lrr ");
                //保存表单数据
                StringBuilder sqlinsert = new StringBuilder();
                sqlinsert.Append(" insert into zxjc_jcbill ");
                sqlinsert.Append("(id, scx, jclx, jcrq, zcbh, sbgly, jcjg, lrr, lrsj, jdqr, jdrqsj) ");
                sqlinsert.Append(" values ");
                sqlinsert.Append(" (:id, :scx, :jclx, :jcrq, :zcbh, :sbgly, :jcjg, :lrr, :lrsj, :jdqr, :jdrqsj) ");
                //查询人员信息
                StringBuilder sqlryxx = new StringBuilder();
                sqlryxx.Append("select user_name FROM zxjc_ryxx where tel = :lrr ");
                //更新表单
                StringBuilder sqlupdate = new StringBuilder();
                sqlupdate.Append("update zxjc_jcbill set zcbh=:zcbh,sbgly=:sbgly,jcjg = :jcjg where id = :id ");
                //查询检测明细
                StringBuilder sqljcmx = new StringBuilder();
                sqljcmx.Append("select id,jcxid,scx,gwh,jclb,xh,jcyq,jcjg,jcz,bz from ZXJC_JCMX where jcjg_id = :billid and jcxid = :jcxid ");
                //保存检测项明细
                StringBuilder sqlinsertmx = new StringBuilder();
                sqlinsertmx.Append(" insert into zxjc_jcmx ");
                sqlinsertmx.Append(" (gcdm,scx, gwh, jclx, jclb, xh, jcyq, bzz, bzsx, bzxx, jcjg, jcz, smjbs, bz, scbz, lrr, lrsj, jcjg_id, jcxid)  ");
                sqlinsertmx.Append(" values  ");
                sqlinsertmx.Append(" (:gcdm,:scx, :gwh, :jclx, :jclb, :xh, :jcyq, :bzz, :bzsx, :bzxx, :jcjg, :jcz, null, :bz, 'N', :lrr, sysdate, :jcjg_id, :jcxid)  ");
                //更新检测项明细
                StringBuilder sqlupdatemx = new StringBuilder();
                sqlupdatemx.Append("update zxjc_jcmx set jcjg =:jcjg, jcz =:jcz, bz = :bz where jcjg_id =:jcjg_id and jcxid = :jcxid ");
                //查询检查项内容
                StringBuilder sqljcitem = new StringBuilder();
                sqljcitem.Append("select gcdm,scx,gwh,jclx,jclb,xh,jcyq,bzz,bzsx,bzxx FROM zxjc_jcjcxx where id = :jcxid ");
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        db.Open();
                        using (var trans = db.BeginTransaction())
                        {
                            try
                            {
                                zxjc_jcbill bill = new zxjc_jcbill();
                                //手机号查询人员信息
                                string lrrname = string.Empty;
                                string jclx = string.Empty;
                                var ryxxq = db.Query<string>(sqlryxx.ToString(), new { lrr = form.lrr });
                                jclx = db.Query<string>(sqljclx.ToString(), new { zcbh = form.zcbh }).FirstOrDefault();
                                form.jclx= jclx;
                                if (ryxxq.Count() > 0)
                                {
                                    lrrname = ryxxq.First();
                                }
                                else
                                {
                                    lrrname = form.lrr;
                                }
                                var billq = db.Query<zxjc_jcbill>(sql.ToString(), new { scx = form.scx, form.jclx, zcbh = form.zcbh, jcrq = form.jcrq, lrr = lrrname });
                                if (billq.Count() > 0)
                                {
                                    bill = billq.FirstOrDefault();
                                    db.Execute(sqlupdate.ToString(), bill, trans);
                                }
                                else
                                {

                                    bill.jcrq = form.jcrq;
                                    bill.jclx = form.jclx;
                                    bill.jcjg = form.zjcjg;
                                    bill.jdqr = form.jdqr;
                                    bill.scx = form.scx;
                                    bill.sbgly = form.sbgly;
                                    bill.zcbh = form.zcbh;
                                    bill.id = Guid.NewGuid().ToString();
                                    bill.lrsj = DateTime.Now;
                                    bill.lrr = lrrname;
                                    db.Execute(sqlinsert.ToString(), bill, trans);
                                }
                                //查询明细是否存在
                                zxjc_jcmx jcmx = new zxjc_jcmx();
                                var mxq = db.Query<zxjc_jcmx>(sqljcmx.ToString(), new { billid = bill.id, jcxid = form.jcxid });
                                if (mxq.Count() > 0)
                                {
                                    jcmx = mxq.First();
                                    jcmx.jcjg = form.jcjg;
                                    jcmx.jcz = form.jcz;
                                    db.Execute(sqlupdatemx.ToString(), jcmx, trans);
                                }
                                else
                                {
                                    var jcxq = db.Query<zxjc_jcjcxx>(sqljcitem.ToString(), new { jcxid = form.jcxid });
                                    zxjc_jcjcxx item = new zxjc_jcjcxx();
                                    if (jcxq.Count() > 0)
                                    {
                                        item = jcxq.First();
                                    }
                                    jcmx.jcjg_id = bill.id;
                                    jcmx.jcxid = form.jcxid;
                                    jcmx.jcjg = form.jcjg;
                                    jcmx.jcz = form.jcz;
                                    jcmx.lrr = bill.lrr;
                                    jcmx.gcdm = item.gcdm;
                                    jcmx.scx = item.scx;
                                    jcmx.jclx = item.jclx;
                                    jcmx.jclb = item.jclb;
                                    jcmx.jcyq = item.jcyq;
                                    jcmx.xh = item.xh;
                                    jcmx.bzz = item.bzz;
                                    jcmx.bzsx = item.bzsx;
                                    jcmx.bzxx = item.bzxx;
                                    jcmx.gwh = item.gwh;
                                    db.Execute(sqlinsertmx.ToString(), jcmx, trans);
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
    }
}

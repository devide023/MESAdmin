﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.CDGC;
using ZDMesModels.CDGC;
using Oracle.ManagedDataAccess.Client;
using ZDMesModels;
using Dapper;
using DapperExtensions;
using DapperExtensions.Predicate;
using System.Data;

namespace ZDMesServices.CDGC.JJBGL
{
    /// <summary>
    /// 电机壳交接班服务类
    /// </summary>
    public class DJKJJBService:BaseDao<zxjc_djkjjb_bill>, IDjkjjb
    {
        public DJKJJBService(string constr) : base(constr)
        {

        }
        public override bool Del(IEnumerable<zxjc_djkjjb_bill> entitys)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("delete from zxjc_djkjjb_bill where id = :id");
            StringBuilder sql1 = new StringBuilder();
            sql1.Append("delete from zxjc_djkjjb_detail where billid = :id");
            StringBuilder sql2 = new StringBuilder();
            sql2.Append("delete from zxjc_djkjjb_hx_detail where billid = :id");
            StringBuilder sql3 = new StringBuilder();
            sql3.Append("delete from zxjc_djkjjb_gfmx where detailid in (select id from zxjc_djkjjb_detail where billid = :id)");
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
                                db.Execute(sql.ToString(), new { id = item.id }, trans);
                                db.Execute(sql1.ToString(), new { id = item.id }, trans);
                                db.Execute(sql2.ToString(), new { id = item.id }, trans);
                                db.Execute(sql3.ToString(), new { id = item.id }, trans);
                            }
                            trans.Commit();
                            return true;
                        }
                        catch (Exception)
                        {
                            trans.Rollback();
                            return false;
                            throw;
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    db.Close();
                }
            }
        }

        public IEnumerable<string> GetCpList()
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select cpmc from zxjc_cpb where cpfl = '电机壳'");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<string>(sql.ToString());
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override IEnumerable<zxjc_djkjjb_bill> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                parm.default_order_colname = "rq";
              return base.GetList(parm,out resultcount);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public zxjc_djkjjb_bill Get_Djkjjb_Bill_ByBc(string rq, string bc)
        {
            try
            {
                rq = rq.Replace("T", " ");
                StringBuilder sql = new StringBuilder();
                sql.Append("select id, rq, bc, jbr, zlqk, sbqk, qtqk, lrr, lrsj, hxry from zxjc_djkjjb_bill where trunc(rq) = trunc(to_date(:rq,'yyyy-MM-dd HH24:mi:ss')) and bc = :bc ");
                StringBuilder sqljj = new StringBuilder();
                sqljj.Append("select id, billid, cpmc, kcsl, jgsl, gfsl, lfsl, hgsl, kcsysl FROM zxjc_djkjjb_detail where  billid = :billid ");
                StringBuilder sqlhx = new StringBuilder();
                sqlhx.Append("select id, billid, xmmc, trjgsl, dpssl, gfsl, lfsl, hgsl FROM   zxjc_djkjjb_hx_detail  where  billid = :billid ");
                StringBuilder gfmxsql = new StringBuilder();
                gfmxsql.Append("select * from zxjc_djkjjb_gfmx where detailid = :mxid");
                using (var db = new OracleConnection(ConString))
                {
                    zxjc_djkjjb_bill bill = new zxjc_djkjjb_bill();
                    var q = db.Query<zxjc_djkjjb_bill>(sql.ToString(), new { rq = rq, bc = bc });
                    if (q.Count() > 0)
                    {
                        bill = q.First();
                        bill.rq = bill.rq?.Date;
                        var jjmx = db.Query<zxjc_djkjjb_detail>(sqljj.ToString(), new { billid = bill.id });
                        bill.djkjjbdetail = jjmx.ToList();
                        foreach (var item in jjmx)
                        {
                            item.gfmxlist = db.Query<zxjc_djkjjb_gfmx>(gfmxsql.ToString(), new { mxid = item.id }).ToList();
                        }
                        var hxmx = db.Query<zxjc_djkjjb_hx_detail>(sqlhx.ToString(), new { billid = bill.id });
                        bill.djkjjbdetailhx = hxmx.ToList();
                    }
                    return bill;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Save_Djkjjb(zxjc_djkjjb_bill bill, List<zxjc_djkjjb_detail> jjmx, List<zxjc_djkjjb_hx_detail> hxmx)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    InitDB(db);
                    try
                    {
                        StringBuilder mxsql = new StringBuilder();
                        mxsql.Append(" insert into zxjc_djkjjb_detail");
                        mxsql.Append(" (billid, cpmc, kcsl, jgsl, gfsl, lfsl, hgsl, kcsysl) ");
                        mxsql.Append(" values ");
                        mxsql.Append(" (:billid, :cpmc, :kcsl, :jgsl, :gfsl, :lfsl, :hgsl, :kcsysl) returning id into :id");
                        using (var trans = db.BeginTransaction())
                        {
                            try
                            {
                                StringBuilder sql = new StringBuilder();
                                sql.Append("select id, rq, bc, jbr, zlqk, sbqk, qtqk, lrr, lrsj, hxry ");
                                sql.Append(" FROM   zxjc_djkjjb_bill where bc=:bc and trunc(rq) = trunc(:rq) ");
                                //
                                var q = db.Query<zxjc_djkjjb_bill>(sql.ToString(), new { bc = bill.bc, rq = bill.rq });
                                int billid = 0;
                                if (q.Count() == 0)
                                {
                                    bill.rq = bill.rq?.Date;
                                    billid = Db.Insert<zxjc_djkjjb_bill>(bill, trans);
                                }
                                else
                                {
                                    var obj = q.OrderBy(t => t.id).First();
                                    bill.id = obj.id;
                                    db.Execute("update zxjc_djkjjb_bill set rq = trunc(:rq),bc=:bc,jbr=:jbr,zlqk=:zlqk,sbqk=:sbqk,hxry=:hxry where id = :id", bill, trans);
                                }
                                sql.Clear();
                                sql.Append("select count(id) FROM ZXJC_DJKJJB_DETAIL where billid=:billid");
                                var qjjmx = db.ExecuteScalar<int>(sql.ToString(), new { billid = bill.id });
                                List<int> mxids = new List<int>();
                                List<zxjc_djkjjb_gfmx> gfmxlist = new List<zxjc_djkjjb_gfmx>();
                                if(qjjmx > 0)
                                {
                                    mxids = db.Query<int>("select id from zxjc_djkjjb_detail where billid=:billid", new { billid = bill.id }).ToList();
                                    gfmxlist = db.Query<zxjc_djkjjb_gfmx>("select * from zxjc_djkjjb_gfmx where detailid in :mxids", new { mxids = mxids.ToList() }).ToList();
                                    db.Execute("delete from zxjc_djkjjb_detail where billid= :billid", new { billid = bill.id }, trans);
                                    db.Execute("delete from zxjc_djkjjb_gfmx where detailid in :mxids", new { mxids = mxids.ToList() }, trans);
                                }
                                //机加明细
                                foreach (var item in jjmx)
                                {
                                    item.billid = bill.id;
                                    item.hgsl = item.jgsl - item.gfsl - item.lfsl;
                                    item.kcsysl = item.kcsl - item.jgsl;
                                    DynamicParameters dyp = new DynamicParameters();
                                    dyp.Add(":billid", item.billid, DbType.Int32, ParameterDirection.Input);
                                    dyp.Add(":cpmc", item.cpmc, DbType.String, ParameterDirection.Input);
                                    dyp.Add(":kcsl", item.kcsl, DbType.Int32, ParameterDirection.Input);
                                    dyp.Add(":jgsl", item.jgsl, DbType.Int32, ParameterDirection.Input);
                                    dyp.Add(":gfsl", item.gfsl, DbType.Int32, ParameterDirection.Input);
                                    dyp.Add(":lfsl", item.lfsl, DbType.Int32, ParameterDirection.Input);
                                    dyp.Add(":hgsl", item.hgsl, DbType.Int32, ParameterDirection.Input);
                                    dyp.Add(":kcsysl", item.kcsysl, DbType.Int32, ParameterDirection.Input);
                                    dyp.Add(":id", null, DbType.Int32, ParameterDirection.Output);
                                    db.Execute(mxsql.ToString(),dyp, trans);
                                    var newmxid = dyp.Get<int>(":id");
                                    foreach (var sitem in item.gfmxlist)
                                    {
                                        sitem.detailid = newmxid;
                                        Db.Insert<zxjc_djkjjb_gfmx>(sitem,trans);
                                    }
                                }
                                sql.Clear();
                                sql.Append("select count(id) FROM zxjc_djkjjb_hx_detail where billid=:billid");
                                var qhxmx = db.ExecuteScalar<int>(sql.ToString(), new { billid = bill.id });
                                if (qhxmx > 0)
                                {
                                    db.Execute("delete from zxjc_djkjjb_hx_detail where billid= :billid", new { billid = bill.id }, trans);
                                }
                                //后序明细
                                foreach (var item in hxmx)
                                {
                                    item.billid = bill.id;
                                    item.hgsl = item.trjgsl - item.dpssl - item.gfsl - item.lfsl;
                                    Db.Insert<zxjc_djkjjb_hx_detail>(item, trans);
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

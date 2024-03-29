﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.CDGC;
using ZDMesModels.CDGC;
using Oracle.ManagedDataAccess.Client;
using Dapper;
using DapperExtensions.Predicate;
using DapperExtensions;
using System.Linq.Expressions;

namespace ZDMesServices.CDGC.GTJC
{
    public class GTJCDataService:BaseDao<zxjc_gtjc_bill>, IGtjc_Result,IGtjcHis
    {
        public GTJCDataService(string constr) : base(constr)
        {

        }

        public override bool Modify(zxjc_gtjc_bill entity)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("update zxjc_gtjc_bill set cljl=:cljl,clsj=sysdate,clr=:clr where id=:id");
                using(var db = new OracleConnection(ConString))
                {
                    return db.Execute(sql.ToString(), entity)>0;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override bool Del(IEnumerable<zxjc_gtjc_bill> entitys)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select count(*) FROM zxjc_gtjc_bill where vim = :vin and cplx=:cplx and rq = :rq");
            StringBuilder delsql = new StringBuilder();
            delsql.Append("delete from zxjc_gtjc_bill where id = :id ");
            StringBuilder delmxsql = new StringBuilder();
            delmxsql.Append("delete from zxjc_gtjc_detail where billid = :id");
            try
            {
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
                                    var sfcz = db.ExecuteScalar<int>(sql.ToString(), item);
                                    if (sfcz > 0)
                                    {
                                        db.Execute(delsql.ToString(), item, trans);
                                        db.Execute(delmxsql.ToString(), item, trans);
                                    }
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

        public List<int> Create_Bill_Ids(string cplx, DateTime rq)
        {
            try
            {
                List<int> ids = new List<int>() { 0, 0, 0, 0 };
                StringBuilder sql = new StringBuilder();
                sql.Append("select * FROM   zxjc_gtjc_bill where  cplx = :cplx  and  trunc(rq) = trunc(:rq) and  jth is null and vin is null and mh is null order  by id");
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        InitDB(db);
                        var q = db.Query<zxjc_gtjc_bill>(sql.ToString(), new { cplx = cplx, rq = rq });
                        if (q.Count() >= 4)
                        {
                            ids = q.Take(4).Select(t => t.id).ToList();
                        }
                        else if (q.Count() > 0 && q.Count() < 4)
                        {
                            ids = q.Select(t => t.id).ToList();
                            for (int i = 0; i < 4 - q.Count(); i++)
                            {
                                zxjc_gtjc_bill billentity = new zxjc_gtjc_bill();
                                var billid = Db.Insert<zxjc_gtjc_bill>(new zxjc_gtjc_bill()
                                {
                                    rq = rq,
                                    cplx = cplx,
                                    lrsj = DateTime.Now
                                });
                                ids.Add(billid);
                            }
                        }
                        else if (q.Count() == 0)
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                zxjc_gtjc_bill billentity = new zxjc_gtjc_bill();
                                var billid = Db.Insert<zxjc_gtjc_bill>(new zxjc_gtjc_bill()
                                {
                                    rq = rq,
                                    cplx = cplx,
                                    lrsj = DateTime.Now
                                });
                                ids.Add(billid);
                            }
                        }
                        return ids;
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

        public bool Update_Gtjc_Bill(zxjc_gtjc_bill bill)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        InitDB(db);
                        return Db.Update(bill);
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

        public bool Save_Gtjc_CheckData(zxjc_gtjc_bill bill)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        InitDB(db);
                        using (var trans = db.BeginTransaction())
                        {
                            try
                            {
                                PredicateGroup pgbill = new PredicateGroup();
                                pgbill.Operator = GroupOperator.And;
                                pgbill.Predicates = new List<IPredicate>();
                                pgbill.Predicates.Add(Predicates.Field<zxjc_gtjc_bill>(t => t.vin, Operator.Eq, bill.vin));
                                pgbill.Predicates.Add(Predicates.Field<zxjc_gtjc_bill>(t => t.cplx, Operator.Eq, bill.cplx));
                                pgbill.Predicates.Add(Predicates.Field<zxjc_gtjc_bill>(t => t.rq, Operator.Eq, Convert.ToDateTime(bill.rq?.ToString("yyyy-MM-dd"))));
                                var bill_q = Db.GetList<zxjc_gtjc_bill>(pgbill).OrderByDescending(t => t.rq);
                                if (bill_q.Count() > 0)
                                {
                                    zxjc_gtjc_bill entity_bill = bill_q.First();
                                    bill.id = entity_bill.id;
                                    bill.lrsj = entity_bill.lrsj;
                                    bill.rq = Convert.ToDateTime(entity_bill.rq?.ToString("yyyy-MM-dd"));
                                    bill.zxjcgtjcdetail.ForEach(t => t.billid = entity_bill.id);
                                    Db.Update<zxjc_gtjc_bill>(bill, trans);
                                }
                                else
                                {
                                    bill.rq = Convert.ToDateTime(bill.rq?.ToString("yyyy-MM-dd"));
                                    var billid = Db.Insert<zxjc_gtjc_bill>(new zxjc_gtjc_bill()
                                    {
                                        jth = bill.jth,
                                        rq = bill.rq,
                                        lrsj = DateTime.Now,
                                        th = bill.th,
                                        bc = bill.bc,
                                        vin = bill.vin,
                                        cplx = bill.cplx,
                                        jylb = bill.jylb,
                                        mh = bill.mh,
                                        lrr = bill.lrr,
                                        jyry = bill.jyry,
                                    }, trans);
                                    bill.id = billid;
                                }
                                PredicateGroup pg = new PredicateGroup();
                                pg.Operator = GroupOperator.And;
                                pg.Predicates = new List<IPredicate>();
                                foreach (var item in bill.zxjcgtjcdetail)
                                {
                                    item.billid = bill.id;
                                    if (item.sdtype == "T")
                                    {
                                        item.sdmjval = "T";
                                    }
                                    pg.Predicates.Clear();
                                    pg.Predicates.Add(Predicates.Field<zxjc_gtjc_detail>(t => t.jcid, Operator.Eq, item.jcid));
                                    pg.Predicates.Add(Predicates.Field<zxjc_gtjc_detail>(t => t.billid, Operator.Eq, item.billid));
                                    var q = Db.GetList<zxjc_gtjc_detail>(pg);
                                    if (q.Count() == 0)
                                    {
                                        Db.Insert<zxjc_gtjc_detail>(item, trans);
                                    }
                                    else
                                    {
                                        item.id = q.First().id;
                                        Db.Update<zxjc_gtjc_detail>(item, trans);
                                    }
                                }
                                db.Execute("update zxjc_gtjc_bill set pdjg =(select jcjg from zxjc_gtjc_detail where billid = zxjc_gtjc_bill.id and cpfw='结果' and kxmc='评定结果' and rownum = 1)  where id = :id", new { id = bill.id }, trans);
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

        public IEnumerable<zxjc_gtjc_detail> Get_CheckData_Vin(string rq,string code, string cplx )
        {
            try
            {
                List<zxjc_gtjc_detail> retlist = new List<zxjc_gtjc_detail>();
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        InitDB(db);
                        DateTime ckrq = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                        if (!string.IsNullOrEmpty(rq))
                        {
                            ckrq = Convert.ToDateTime(Convert.ToDateTime(rq).ToString("yyyy-MM-dd"));
                        }
                        PredicateGroup pg = new PredicateGroup();
                        pg.Operator = GroupOperator.And;
                        pg.Predicates = new List<IPredicate>();
                        pg.Predicates.Add(Predicates.Field<zxjc_gtjc_bill>(t => t.vin, Operator.Eq, code));
                        pg.Predicates.Add(Predicates.Field<zxjc_gtjc_bill>(t => t.cplx, Operator.Eq, cplx));
                        pg.Predicates.Add(Predicates.Field<zxjc_gtjc_bill>(t => t.rq, Operator.Eq, ckrq));
                        var q = Db.GetList<zxjc_gtjc_bill>(pg);
                        if (q.Count() > 0)
                        {
                            var billid = q.First().id;
                            PredicateGroup detailpg = new PredicateGroup();
                            detailpg.Operator = GroupOperator.And;
                            detailpg.Predicates = new List<IPredicate>();
                            detailpg.Predicates.Add(Predicates.Field<zxjc_gtjc_detail>(t => t.billid, Operator.Eq, billid));
                            var detaillist = Db.GetList<zxjc_gtjc_detail>(detailpg).OrderBy(t => t.jcid);
                            retlist = detaillist.ToList();
                        }
                        return retlist;
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

        public zxjc_gtjc_bill Get_GtjcInfo(int billid)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select id, cplx, rq, jyry, jylb, jth, vin, mh, pdjg, cljl, clr, clsj, lrr, lrsj, th FROM  zxjc_gtjc_bill where  id = :billid ");
                StringBuilder sqlmx = new StringBuilder();
                sqlmx.Append("select id, billid, jcid, cpfw, kxmc, kxcc, sdmj, kjval, sdmjval, jcjg FROM zxjc_gtjc_detail where  billid = :billid ");
                //缸体检测基础数据
                StringBuilder gtjcsql = new StringBuilder();
                gtjcsql.Append("select id, cplx, mh, cpfw, th, kxmc, kjcc, kjcc_sx as kjccsx, kjcc_xx as kjccxx, sdmj, sdmj_sx as sdmjsx, sdmj_xx as sdmjxx, lrr, lrsj, kjtype, sdtype, kjzsz, sdzsz, seq ");
                gtjcsql.Append(" from zxjc_base_gtjc");
                gtjcsql.Append(" where id = :jcid ");
                using (var db = new OracleConnection(ConString))
                {
                    var entity = db.Query<zxjc_gtjc_bill>(sql.ToString(), new { billid = billid }).FirstOrDefault();
                    var mxlist = db.Query<zxjc_gtjc_detail>(sqlmx.ToString(), new { billid = billid });
                    //foreach (var item in mxlist)
                    //{
                    //   item.base_gtjc = db.Query<zxjc_base_gtjc>(gtjcsql.ToString(), new { jcid = item.jcid }).FirstOrDefault();
                    //}
                    entity.zxjcgtjcdetail = mxlist.ToList();
                    return entity;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

using System;
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

namespace ZDMesServices.CDGC.GTJC
{
    public class GTJCDataService:BaseDao<zxjc_gtjc_bill>, IGtjc_Result
    {
        public GTJCDataService(string constr) : base(constr)
        {

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
                                pgbill.Predicates.Add(Predicates.Field<zxjc_gtjc_bill>(t => t.rq, Operator.Eq, bill.rq));
                                var bill_q = Db.GetList<zxjc_gtjc_bill>(pgbill).OrderBy(t => t.rq);
                                if (bill_q.Count() > 0)
                                {
                                    zxjc_gtjc_bill entity_bill = bill_q.First();
                                    bill.id = entity_bill.id;
                                    bill.lrsj = entity_bill.lrsj;
                                    bill.rq = Convert.ToDateTime(entity_bill.rq.ToString("yyyy-MM-dd"));
                                    bill.zxjcgtjcdetail.ForEach(t => t.billid = entity_bill.id);
                                }
                                else
                                {
                                    bill.rq = Convert.ToDateTime(bill.rq.ToString("yyyy-MM-dd"));
                                    if (bill.lrsj == Convert.ToDateTime(null))
                                    {
                                        bill.lrsj = DateTime.Now;
                                    }
                                }
                                Db.Update<zxjc_gtjc_bill>(bill, trans);
                                PredicateGroup pg = new PredicateGroup();
                                pg.Operator = GroupOperator.And;
                                pg.Predicates = new List<IPredicate>();
                                foreach (var item in bill.zxjcgtjcdetail)
                                {
                                    item.billid = bill.id;
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
                                trans.Commit();
                            }
                            catch (Exception)
                            {
                                trans.Rollback();
                            }

                            return true;
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
    }
}

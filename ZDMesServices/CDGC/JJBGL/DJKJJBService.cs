using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.CDGC;
using ZDMesModels.CDGC;
using Oracle.ManagedDataAccess.Client;
using ZDMesModels;
using DapperExtensions;
using DapperExtensions.Predicate;
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

        public override IEnumerable<zxjc_djkjjb_bill> GetList(sys_page parm, out int resultcount)
        {
            try
            {
              return base.GetList(parm,out resultcount);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Save_Djkjjb(zxjc_djkjjb_bill bill)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    InitDB(db);
                    try
                    {
                        using (var trans = db.BeginTransaction())
                        {
                            try
                            {
                                int billid = 0;
                                billid = Db.Insert<zxjc_djkjjb_bill>(bill);
                                /*
                                PredicateGroup pg = new PredicateGroup();
                                pg.Operator = GroupOperator.And;
                                pg.Predicates = new List<IPredicate>();
                                pg.Predicates.Add(Predicates.Field<zxjc_djkjjb_bill>(t => t.rq, Operator.Eq, bill.rq));
                                pg.Predicates.Add(Predicates.Field<zxjc_djkjjb_bill>(t => t.bc, Operator.Eq, bill.bc));
                                //
                                PredicateGroup detailpg = new PredicateGroup();
                                detailpg.Operator = GroupOperator.And;
                                detailpg.Predicates = new List<IPredicate>();
                                //
                                PredicateGroup hxdetailpg = new PredicateGroup();
                                hxdetailpg.Operator = GroupOperator.And;
                                hxdetailpg.Predicates = new List<IPredicate>();

                                var q = Db.GetList<zxjc_djkjjb_bill>(pg);
                                int billid = 0;
                                if (q.Count() == 0) {
                                    billid = Db.Insert<zxjc_djkjjb_bill>(bill, trans);
                                }
                                else
                                {
                                    billid = q.OrderBy(t=>t.id).First().id;
                                }
                                detailpg.Predicates.Add(Predicates.Field<zxjc_djkjjb_detail>(t => t.billid, Operator.Eq, billid));
                                //
                                hxdetailpg.Predicates.Add(Predicates.Field<zxjc_djkjjb_hx_detail>(t => t.billid, Operator.Eq, billid));
                                var detailq = Db.GetList<zxjc_djkjjb_detail>(detailpg);
                                var hxdetailq = Db.GetList<zxjc_djkjjb_hx_detail>(hxdetailpg);
                                //机加明细
                                foreach (var item in bill.details)
                                {
                                    item.billid = billid;
                                    if (detailq.Count() > 0)
                                    {
                                        Db.Update<zxjc_djkjjb_detail>(item, trans);
                                    }
                                    else
                                    {
                                        Db.Insert<zxjc_djkjjb_detail>(item, trans);
                                    }
                                }
                                //后序明细
                                foreach (var item in bill.hxdetails)
                                {
                                    item.billid = billid;
                                    if (hxdetailq.Count() > 0)
                                    {
                                        Db.Update<zxjc_djkjjb_hx_detail>(item, trans);
                                    }
                                    else
                                    {
                                        Db.Insert<zxjc_djkjjb_hx_detail>(item, trans);
                                    }
                                }*/
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.LBJ.SBWB;
using ZDMesModels.LBJ;
using Dapper;
using DapperExtensions;
using DapperExtensions.Predicate;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Autofac.Extras.DynamicProxy;
using ZDMesInterceptor.LBJ;

namespace ZDMesServices.LBJ.SBWB
{
    public class WbZqService:BaseDao<base_sbwb_ls>, ISbWbZq
    {
        private ISBGW _sbgw;
        public WbZqService(string constr, ISBGW sbgw) :base(constr)
        {
            _sbgw = sbgw;
        }

        public override int Add(IEnumerable<base_sbwb_ls> entitys)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        db.Open();
                        InitDB(db);
                        using (var tran = db.BeginTransaction())
                        {
                            try
                            {
                                PredicateGroup pg = new PredicateGroup();
                                pg.Operator = GroupOperator.And;
                                pg.Predicates = new List<IPredicate>();
                                foreach (var item in entitys)
                                {
                                    pg.Predicates.Clear();
                                    pg.Predicates.Add(Predicates.Field<base_sbwb_ls>(t => t.gcdm, Operator.Eq, item.gcdm));
                                    pg.Predicates.Add(Predicates.Field<base_sbwb_ls>(t => t.scx, Operator.Eq, item.scx));
                                    pg.Predicates.Add(Predicates.Field<base_sbwb_ls>(t => t.sbbh, Operator.Eq, item.sbbh));
                                    pg.Predicates.Add(Predicates.Field<base_sbwb_ls>(t => t.wbzt, Operator.Eq, "计划中"));
                                    Db.Delete<base_sbwb_ls>(pg, tran);
                                }
                                Db.Insert<base_sbwb_ls>(entitys, tran);
                                tran.Commit();
                                return entitys.Count();
                            }
                            catch
                            {
                                tran.Rollback();
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
                Db.Dispose();
            }
        }

        public override bool Del(IEnumerable<base_sbwb_ls> entitys)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    InitDB(db);
                    PredicateGroup pg = new PredicateGroup()
                    {
                        Operator = GroupOperator.And,
                        Predicates = new List<IPredicate>()
                    };
                    List<bool> ret = new List<bool>();
                    foreach (var item in entitys)
                    {
                        pg.Predicates.Clear();
                        pg.Predicates.Add(Predicates.Field<base_sbwb_ls>(t => t.wbzt, Operator.Eq, "计划中"));
                        pg.Predicates.Add(Predicates.Field<base_sbwb_ls>(t => t.autoid, Operator.Eq, item.autoid));
                        ret.Add(Db.Delete<base_sbwb_ls>(pg));
                    }
                    return ret.Where(t => t).Count() > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Db.Dispose();
            }
        }

        public IEnumerable<base_sbwb> WbXxList(base_sbwb item)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select autoid, gcdm, scx, sbbh, gwh, wbsh, wbxx, bz from BASE_SBWB where  scbz = 'N' ");
                DynamicParameters p = new DynamicParameters();
                if (item.scxs!=null && item.scxs.Count > 0)
                {
                    sql.Append(" and scx in :scx ");
                    p.Add(":scx", item.scxs);
                }
                if (!string.IsNullOrEmpty(item.scx))
                {
                    sql.Append(" and scx = :scx ");
                    p.Add(":scx",item.scx);
                }
                if (!string.IsNullOrEmpty(item.sbbh))
                {
                    sql.Append(" and sbbh = :sbbh ");
                    p.Add(":sbbh", item.sbbh);
                }
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<base_sbwb>(sql.ToString(),p); 
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<base_sbwb_ls> WbZqList()
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        InitDB(db);
                        List<base_sbwb_ls> result = new List<base_sbwb_ls>();
                        StringBuilder sql = new StringBuilder();
                        sql.Append("select autoid, gcdm, scx, sbbh, wbsh, wbxx, wbjhsj,wbjhsj_end as wbjhsjend, wbzt, wbwcsj, wbwcr, lrr, lrsj ");
                        sql.Append(" from BASE_SBWB_LS t ");
                        sql.Append(" where trunc(wbjhsj) = (select max(trunc(wbjhsj)) from BASE_SBWB_LS)");
                        result = db.Query<base_sbwb_ls>(sql.ToString()).ToList();
                        var list = Db.GetList<base_sbwb>().OrderBy(t=>t.scx).ThenBy(t=>t.sbbh).ThenBy(t=>t.wbsh);
                        List<base_sbwb_ls> result_list = new List<base_sbwb_ls>();
                        foreach (var item in list)
                        {
                            var q = result.Where(t => t.gcdm == item.gcdm && t.scx == item.scx && t.sbbh == item.sbbh && t.wbxx == item.wbxx);
                            if (q.Count() == 0)
                            {
                                result_list.Add(new base_sbwb_ls()
                                {
                                    gcdm = item.gcdm,
                                    scx = item.scx,
                                    sbbh = item.sbbh,
                                    wbxx = item.wbxx,
                                    wbsh = item.wbsh,
                                    wbzt = "",
                                    wbjhsj = null,
                                    wbjhsjend=null,
                                    sfwb = "Y"
                                }) ;
                            }
                            else
                            {
                                var o = q.First();
                                result_list.Add(new base_sbwb_ls()
                                {
                                    gcdm = item.gcdm,
                                    scx = item.scx,
                                    sbbh = item.sbbh,
                                    wbxx = item.wbxx,
                                    wbsh = item.wbsh,
                                    wbzt = o.wbzt,
                                    wbjhsj = o.wbjhsj,
                                    wbjhsjend = o.wbjhsjend,
                                    wbwcr = o.wbwcr,
                                    wbwcsj = o.wbwcsj,
                                    sfwb = "Y"
                                });
                            }
                        }
                        return result_list.OrderBy(t => t.gcdm).ThenBy(t => t.scx).ThenBy(t=>t.sbbh).ThenBy(t => t.wbsh);
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
                Db.Dispose();
            }
        }
    }
}

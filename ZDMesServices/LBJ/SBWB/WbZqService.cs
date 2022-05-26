﻿using System;
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

namespace ZDMesServices.LBJ.SBWB
{
    public class WbZqService:BaseDao<base_sbwb_ls>, ISbWbZq
    {
        public WbZqService(string constr):base(constr)
        {

        }

        public override int Add(IEnumerable<base_sbwb_ls> entitys)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        InitDB(db);
                        db.Open();
                        using (var tran = Db.Connection.BeginTransaction())
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
                                    pg.Predicates.Add(Predicates.Field<base_sbwb_ls>(t => t.gwh, Operator.Eq, item.gwh));
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
        }

        public IEnumerable<base_sbwb_ls> WbZqList()
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    List<base_sbwb_ls> result = new List<base_sbwb_ls>();
                    StringBuilder sql = new StringBuilder();
                    sql.Append("select autoid, gcdm, scx, gwh, wbsh, wbxx, wbjhsj, wbzt, wbwcsj, wbwcr, lrr, lrsj ");
                    sql.Append(" from BASE_SBWB_LS t ");
                    sql.Append(" where trunc(wbjhsj) = (select max(trunc(wbjhsj)) from BASE_SBWB_LS)");
                    result = db.Query<base_sbwb_ls>(sql.ToString()).ToList();
                    var list = Db.GetList<base_sbwb>();
                    foreach (var item in list)
                    {
                        var q = result.Where(t => t.gcdm == item.gcdm && t.scx == item.scx && t.gwh == item.gwh && t.wbxx == item.wbxx);
                        if (q.Count() == 0)
                        {
                            result.Add(new base_sbwb_ls()
                            {
                                gcdm = item.gcdm,
                                scx = item.scx,
                                gwh = item.gwh,
                                wbxx = item.wbxx,
                                wbsh = item.wbsh,
                                wbzt = "计划中",
                                sfwb = "Y"
                            });
                        }
                    }
                    return result.OrderBy(t => t.gcdm).ThenBy(t => t.scx).ThenBy(t => t.wbsh);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

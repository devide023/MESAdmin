using Dapper;
using DapperExtensions;
using DapperExtensions.Predicate;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ.SBWB;
using ZDMesModels;
using ZDMesModels.LBJ;

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

        public IEnumerable<base_sbwb_ls> Get_WbJh_List(sys_page parm, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                sql.Append($"select autoid, gcdm, scx, gwh, wbsh, wbxx, wbjhsj, wbzt, wbwcsj, wbwcr, lrr, lrsj, wbjhsj_end as wbjhsjend, sbbh, scxzx from base_sbwb_ls where wbzt='计划中' ");
                sql_cnt.Append($"select count(autoid) from base_sbwb_ls where wbzt='计划中' ");

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
                }
                using (var db = new OracleConnection(ConString))
                {
                    var q = db.Query<base_sbwb_ls>(OraPager(sql.ToString()), parm.sqlparam);
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                    return q;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool SaveSbWbInfo(List<base_sbwb_ls> entitys)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" insert into base_sbwb_ls ");
                sql.Append(" (autoid, gcdm, scx, gwh, wbsh, wbxx, wbjhsj, wbzt, lrr, lrsj, wbjhsj_end, sbbh, scxzx) ");
                sql.Append(" select  ");
                sql.Append(" : autoid, :gcdm, :scx, :gwh, :wbsh, :wbxx, :wbjhsj, :wbzt, :lrr, :lrsj, :wbjhsjend, :sbbh, :scxzx from dual  ");
                sql.Append("  where not exists ( ");
                sql.Append("    select * from base_sbwb_ls where gcdm =:gcdm and scx = :scx and scxzx =:scxzx and sbbh =:sbbh and wbxx =:wbxx and wbzt = '计划中' ) ");
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
                                    db.Execute(sql.ToString(), new { 
                                        item.autoid, 
                                        item.gcdm, 
                                        item.scx, 
                                        item.gwh, 
                                        item.wbsh, 
                                        item.wbxx, 
                                        item.wbjhsj, 
                                        item.wbzt, 
                                        item.lrr, 
                                        item.lrsj, 
                                        item.wbjhsjend, 
                                        item.sbbh, 
                                        item.scxzx }, trans);
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

        public IEnumerable<base_sbwb> ScxZxWbXxList(base_sbwb item)
        {
            try
            {
                List<base_scxxx_jj> templist = new List<base_scxxx_jj>();
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_temp = new StringBuilder();
                sql.Append("select autoid, gcdm, scx,scxzx, sbbh, gwh, wbsh, wbxx, bz from BASE_SBWB where  scbz = 'N' ");
                DynamicParameters p = new DynamicParameters();
                foreach (var scx in item.scxs)
                {
                    var l = scx?.Split(new char[] { '-' });
                    if (l?.Length == 2)
                    {
                        templist.Add(new base_scxxx_jj() { scx = l[0], scxzx = l[1] });
                    }
                    else if (l?.Length == 1)
                    {
                        templist.Add(new base_scxxx_jj() { scx = l[0] });
                    }

                }
                if (templist.Count > 0)
                {
                    for (int i = 0; i < templist.Count; i++)
                    {
                        if (i == templist.Count - 1)
                        {
                            sql_temp.Append($" ( scx = :scx{i} and nvl(scxzx,' ') = :scxzx{i} ) ");
                        }
                        else
                        {
                            sql_temp.Append($" ( scx = :scx{i} and nvl(scxzx,' ') = :scxzx{i} ) or ");
                        }
                        p.Add($":scx{i}", templist[i].scx);
                        p.Add($":scxzx{i}",  string.IsNullOrEmpty(templist[i].scxzx)?" ": templist[i].scxzx);
                    }
                    sql.Append(" and ");
                    sql.Append("(");
                    sql.Append(sql_temp);
                    sql.Append(")");
                    
                }
                if (!string.IsNullOrEmpty(item.sbbh))
                {
                    sql.Append(" and sbbh = :sbbh ");
                    p.Add(":sbbh", item.sbbh);
                }
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<base_sbwb>(sql.ToString(), p);
                }
            }
            catch (Exception)
            {

                throw;
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
                        sql.Append("select autoid, gcdm, scx,scxzx, sbbh, wbsh, wbxx, wbjhsj,wbjhsj_end as wbjhsjend, wbzt, wbwcsj,(select user_name FROM zxjc_ryxx where user_code = t.wbwcr and rownum = 1 ) as wbwcr, lrr, lrsj ");
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

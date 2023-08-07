using Castle.Components.DictionaryAdapter.Xml;
using Dapper;
using DapperExtensions;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.LBJ.ZLGL;
using ZDMesModels;
using ZDMesModels.LBJ;
using ZDMesServices.Common;

namespace ZDMesServices.LBJ.ZLGL
{
    public class LBJ_CheckBillService : BaseDao<zxjc_check_bill>,ILBJCheckBill
    {
        private UserUtilService _u;
        public LBJ_CheckBillService(string constr) : base(constr)
        {
            _u = new UserUtilService(constr);
        }

        public override bool Del(IEnumerable<zxjc_check_bill> entitys)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("delete from zxjc_check_bill where id = :id ");
                StringBuilder sqlmx = new StringBuilder();
                sqlmx.Append("delete FROM zxjc_check_bill_detail where billid = :id ");
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
                                    db.Execute(sqlmx.ToString(), new { id = item.id }, trans);
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

        public override int Add(IEnumerable<zxjc_check_bill> entitys)
        {
            try
            {
                StringBuilder sqlisexist = new StringBuilder();
                sqlisexist.Append("select * FROM zxjc_check_bill where scx=:scx and trunc(rq) = trunc(:rq) and bc = :bc and cpxh = :cpxh ");
                //表单
                StringBuilder sql = new StringBuilder();
                sql.Append("insert into zxjc_check_bill ");
                sql.Append("(scx, bmmc, rq, bc, cpxh, cpmc, gxmc, khmc, jcjg, bz, lrr, lrsj, shr, shsj,vin,smjbs) ");
                sql.Append("values ");
                sql.Append("(:scx, :bmmc, :rq, :bc, :cpxh, :cpmc, :gxmc, :khmc, :jcjg, :bz, :lrr, sysdate, null, null,:vin,:smjbs) returning id into :id ");
                //明细
                StringBuilder sqlmx = new StringBuilder();
                sqlmx.Append("insert into zxjc_check_bill_detail ");
                sqlmx.Append("(billid, checkid, checkval) ");
                sqlmx.Append(" values");
                sqlmx.Append(" (:billid, :checkid, :checkval) ");
                //更新表单
                StringBuilder sqlupdate = new StringBuilder();
                sqlupdate.Append("update zxjc_check_bill set scx=:scx,vin=:vin,smjbs=:smjbs,bmmc=:bmmc,rq=:rq,bc=:bc,cpxh=:cpxh,cpmc=:cpmc,gxmc=:gxmc,khmc=:khmc,jcjg=:jcjg,bz=:bz,xgr=:xgr,xgsj=sysdate where id = :id ");
                //删除明细
                StringBuilder sqldel = new StringBuilder();
                sqldel.Append("delete from zxjc_check_bill_detail where billid = :billid ");
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
                                    var isexist = db.Query<zxjc_check_bill>(sqlisexist.ToString(), new { scx = item.scx, rq = item.rq, bc = item.bc, cpxh = item.cpxh });
                                    if (isexist.Count() == 0)
                                    {
                                        DynamicParameters p = new DynamicParameters();
                                        p.Add(":scx", item.scx);
                                        p.Add(":bmmc", item.bmmc);
                                        p.Add(":rq", item.rq);
                                        p.Add(":bc", item.bc);
                                        p.Add(":cpxh", item.cpxh);
                                        p.Add(":cpmc", item.cpmc);
                                        p.Add(":gxmc", item.gxmc);
                                        p.Add(":khmc", item.khmc);
                                        p.Add(":jcjg", item.jcjg);
                                        p.Add(":bz", item.bz);
                                        p.Add(":vin", item.vin);
                                        p.Add(":smjbs", item.smjbs);
                                        p.Add(":lrr", item.lrr);
                                        p.Add(":id", null, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);
                                        db.Execute(sql.ToString(), p, trans);
                                        int billid = p.Get<int>(":id");
                                        //明细
                                        foreach (var sitem in item.BillDetails)
                                        {
                                            DynamicParameters mx = new DynamicParameters();
                                            mx.Add(":billid", billid, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                                            mx.Add(":checkid", sitem.checkid, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                                            mx.Add(":checkval", sitem.checkval, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                                            db.Execute(sqlmx.ToString(), mx, trans);
                                        }
                                    }
                                    else
                                    {
                                        var billid = isexist.FirstOrDefault().id;
                                        item.id = billid;
                                        item.xgr = _u.CurrentUser.name;
                                        db.Execute(sqlupdate.ToString(), item, trans);
                                        db.Execute(sqldel.ToString(), new { billid = billid }, trans);
                                        //明细
                                        foreach (var sitem in item.BillDetails)
                                        {
                                            DynamicParameters mx = new DynamicParameters();
                                            mx.Add(":billid", billid, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                                            mx.Add(":checkid", sitem.checkid, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                                            mx.Add(":checkval", sitem.checkval, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                                            db.Execute(sqlmx.ToString(), mx, trans);
                                        }
                                    }
                                }
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

        public override bool Modify(IEnumerable<zxjc_check_bill> entitys)
        {
            try
            {
                //明细
                StringBuilder sqlmx = new StringBuilder();
                sqlmx.Append("insert into zxjc_check_bill_detail ");
                sqlmx.Append("(billid, checkid, checkval,val1,val2) ");
                sqlmx.Append(" values");
                sqlmx.Append(" (:billid, :checkid, :checkval,:val1,:val2) ");
                //更新表单
                StringBuilder sqlupdate = new StringBuilder();
                sqlupdate.Append("update zxjc_check_bill set scx=:scx,vin=:vin,smjbs=:smjbs,bmmc=:bmmc,rq=trunc(:rq),bc=:bc,cpxh=:cpxh,cpmc=:cpmc,gxmc=:gxmc,khmc=:khmc,jcjg=:jcjg,bz=:bz,xgr=:xgr,xgsj=sysdate where id = :id ");
                //删除明细
                StringBuilder sqldel = new StringBuilder();
                sqldel.Append("delete from zxjc_check_bill_detail where billid = :billid ");
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
                                    var q = item.BillDetails.Where(t => t.checkval == "不合格" || string.IsNullOrEmpty(t.checkval));
                                    if (q.Count() > 0)
                                    {
                                        item.jcjg = "不合格";
                                    }
                                    else
                                    {
                                        item.jcjg = "合格";
                                    }
                                    item.xgr = _u.CurrentUser.name;
                                    db.Execute(sqlupdate.ToString(), item, trans);
                                    db.Execute(sqldel.ToString(), new { billid = item.id }, trans);
                                    foreach (var sitem in item.BillDetails)
                                    {
                                        DynamicParameters mx = new DynamicParameters();
                                        mx.Add(":billid", item.id, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                                        mx.Add(":checkid", sitem.checkid, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                                        mx.Add(":checkval", sitem.checkval, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                                        mx.Add(":val1", sitem.val1, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                                        mx.Add(":val2", sitem.val2, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                                        db.Execute(sqlmx.ToString(), mx, trans);
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

        public IEnumerable<zxjc_check_bill> Hj_Check_Bill_List(sys_page parm, out int resultcount)
        {
            try
            {
                resultcount = 0;
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                sql.Append("select id, scx, bmmc, rq, bc, cpxh, cpmc, gxmc, khmc, jcjg, bz, lrr, lrsj, shr, shsj, vin, jjh, xgr, xgsj, smjbs, shjg, xjsj, xjr, xjjg, xjclyj");
                sql.Append(" FROM zxjc_check_bill where lrr is not null and shr is null");
                //
                sql_cnt.Append("select count(id) from zxjc_check_bill where lrr is not null and xjr is null ");
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
                    var q = db.Query<zxjc_check_bill>(OraPager(sql.ToString()), parm.sqlparam);
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                    return q;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<zxjc_check_bill> Xj_Check_Bill_List(sys_page parm, out int resultcount)
        {
            try
            {
                resultcount = 0;
                StringBuilder sql = new StringBuilder();
                StringBuilder sql_cnt = new StringBuilder();
                sql.Append("select id, scx, bmmc, rq, bc, cpxh, cpmc, gxmc, khmc, jcjg, bz, lrr, lrsj, shr, shsj, vin, jjh, xgr, xgsj, smjbs, shjg, xjsj, xjr, xjjg, xjclyj");
                sql.Append(" FROM zxjc_check_bill where shr is not null and xjr is null");
                //
                sql_cnt.Append("select count(id) from zxjc_check_bill where shr is not null and xjr is null ");
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
                    var q = db.Query<zxjc_check_bill>(OraPager(sql.ToString()), parm.sqlparam);
                    resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                    return q;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Xj_Audit(List<zxjc_check_bill> entitys)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("update zxjc_check_bill set xjr=:xjr,xjsj=sysdate,xjjg = :xjjg where id = :id");
                using (var db = new OracleConnection(ConString))
                {
                    List<int> l = new List<int>();
                    foreach (var item in entitys)
                    {
                       l.Add( db.Execute(sql.ToString(), new { xjr = _u.CurrentUser.name,xjjg = item.xjjg, id = item.id }));
                    }
                    return entitys.Count() == l.Count();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Hj_Audit(List<zxjc_check_bill> entitys)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                
                sql.Append("update zxjc_check_bill set shr=:shr,shsj=sysdate,shjg=:shjg where id = :id");
                using (var db = new OracleConnection(ConString))
                {
                    List<int> l = new List<int>();
                    foreach (var item in entitys)
                    {
                        l.Add(db.Execute(sql.ToString(), new { shr = _u.CurrentUser.name, shjg = item.shjg, id = item.id }));
                    }
                    return entitys.Count() == l.Count();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

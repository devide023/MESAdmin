using Aspose.Cells;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.LBJ.ZLGL;
using ZDMesModels.LBJ;
using ZDMesServices.Common;

namespace ZDMesServices.LBJ.ZLGL
{
    public class LbjCheckService : OracleBaseFixture, ILBJProductCheck
    {
        private UserUtilService _u;
        public LbjCheckService(string constr) : base(constr)
        {
            _u = new UserUtilService(constr);
        }

        public IEnumerable<zxjc_base_check> GetCheckItemsByCpxh(string cpxh)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select id, cpfw, cpxh, th, jcxm, jcpc, jcgj, jcz, jcxx, jcsx, srlx, seq, lrr, lrsj from ZXJC_BASE_CHECK where scbz='N' and cpxh = :cpxh order by cpfw asc,th asc,jcpc asc,jcgj asc ");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<zxjc_base_check>(sql.ToString(), new { cpxh = cpxh });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<string> GetCpxhByKey(string key)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select distinct cpxh from zxjc_base_check where cpxh like :key ");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<string>(sql.ToString(), new { key = "%" + key.Trim().ToUpper() + "%" });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<string> GetCpxhList()
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select distinct cpxh from zxjc_base_check ");
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

        public IEnumerable<zxjc_base_check> GetFxItemsByCpxh(string cpxh)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select id, cpfw, cpxh, th, jcxm, jcpc, jcgj, jcz, jcxx, jcsx ");
                sql.Append(" from ZXJC_BASE_CHECK  ");
                sql.Append(" where cpxh = :cpxh  ");
                sql.Append(" and isfx = 'Y'  ");
                sql.Append(" and scbz = 'N'  ");
                sql.Append(" order by seq  ");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<zxjc_base_check>(sql.ToString(), new { cpxh = cpxh });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public zxjc_check_bill Get_BillInfo_ById(string billid)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select id, scx, bmmc, rq, bc, cpxh, cpmc, gxmc, khmc, jcjg, bz, lrr, lrsj, shr, shsj, vin, jjh, xgr, xgsj,smjbs FROM zxjc_check_bill where id = :billid ");
                //
                StringBuilder sqlmx = new StringBuilder();
                sqlmx.Append("select ta.id as checkid, ta.checkval,ta.val1,ta.val2,tb.id,tb.cpfw, tb.th, tb.jcxm, tb.jcpc, tb.jcgj,tb.srlx,tb.jcxx,tb.jcsx ");
                sqlmx.Append(" FROM zxjc_check_bill_detail ta, zxjc_base_check tb ");
                sqlmx.Append(" where ta.checkid = tb.id ");
                sqlmx.Append(" and ta.billid = :billid ");
                using (var db = new OracleConnection(ConString))
                {
                    zxjc_check_bill resultobj  = new zxjc_check_bill();
                    resultobj = db.Query<zxjc_check_bill>(sql.ToString(), new { billid = billid }).FirstOrDefault();
                    resultobj.BillDetails = db.Query<zxjc_check_bill_detail,zxjc_base_check, zxjc_check_bill_detail>(sqlmx.ToString(),(ta,tb)=> {                        
                        ta.billid = Convert.ToInt32(billid);
                        ta.id = ta.checkid;
                        ta.checkid = tb.id;
                        ta.CheckItem = tb;
                        return ta;
                    },new { billid = billid },splitOn: "id").ToList();
                    return resultobj;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Save_CheckBill_Data(sys_checkbill_form form)
        {
            try
            {
                StringBuilder sqlisexist = new StringBuilder();
                sqlisexist.Append("select * FROM zxjc_check_bill where scx=:scx and rq = trunc(:rq) and bc = :bc and cpxh = :cpxh and vin = :vin ");
                //表单
                StringBuilder sql = new StringBuilder();
                sql.Append("insert into zxjc_check_bill ");
                sql.Append("(scx, bmmc, rq, bc, cpxh, cpmc, gxmc, khmc, jcjg, bz, lrr, lrsj, shr, shsj,vin,jjh,smjbs) ");
                sql.Append("values ");
                sql.Append("(:scx, :bmmc, trunc(:rq), :bc, :cpxh, :cpmc, :gxmc, :khmc, :jcjg, :bz, :lrr, sysdate, null, null,:vin,:jjh,:smjbs) returning id into :id ");
                //明细
                StringBuilder sqlmx = new StringBuilder();
                sqlmx.Append("insert into zxjc_check_bill_detail ");
                sqlmx.Append("(billid, checkid, checkval,val1,val2) ");
                sqlmx.Append(" values");
                sqlmx.Append(" (:billid, :checkid, :checkval,:val1,:val2) ");
                //更新表单
                StringBuilder sqlupdate = new StringBuilder();
                sqlupdate.Append("update zxjc_check_bill set scx=:scx,bmmc=:bmmc,rq=trunc(:rq),bc=:bc,cpxh=:cpxh,cpmc=:cpmc,gxmc=:gxmc,khmc=:khmc,jcjg=:jcjg,bz=:bz,vin=:vin,smjbs=:smjbs,jjh=:jjh,xgr=:xgr,xgsj=sysdate where id = :id ");
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
                                var isexist = db.Query<zxjc_check_bill>(sqlisexist.ToString(), new { scx = form.scx, rq = Convert.ToDateTime(form.rq.ToString("yyyy-MM-dd")), bc = form.bc, cpxh = form.cpxh,vin=form.vin });
                                var q = form.BillDetails.Where(t => t.checkval == "不合格" || string.IsNullOrEmpty(t.checkval));
                                if (q.Count() > 0)
                                {
                                    form.jcjg = "不合格";
                                }
                                else
                                {
                                    form.jcjg = "合格";
                                }
                                if (isexist.Count() == 0)
                                {
                                    DynamicParameters p = new DynamicParameters();
                                    p.Add(":scx", form.scx);
                                    p.Add(":bmmc", form.bmmc);
                                    p.Add(":rq", form.rq);
                                    p.Add(":bc", form.bc);
                                    p.Add(":cpxh", form.cpxh);
                                    p.Add(":cpmc", form.cpmc);
                                    p.Add(":gxmc", form.gxmc);
                                    p.Add(":khmc", form.khmc);
                                    p.Add(":jcjg", form.jcjg);
                                    p.Add(":bz", form.bz);
                                    p.Add(":vin", form.vin);
                                    p.Add(":smjbs", form.smjbs);
                                    p.Add(":jjh", form.jjh);
                                    p.Add(":lrr", _u.CurrentUser.name);
                                    p.Add(":id", null, System.Data.DbType.Int32, System.Data.ParameterDirection.Output);
                                    db.Execute(sql.ToString(), p, trans);
                                    int billid = p.Get<int>(":id");
                                    //明细
                                    foreach (var sitem in form.BillDetails)
                                    {
                                        DynamicParameters mx = new DynamicParameters();
                                        mx.Add(":billid", billid, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                                        mx.Add(":checkid", sitem.checkid, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
                                        mx.Add(":checkval", sitem.checkval, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                                        mx.Add(":val1", sitem.val1, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                                        mx.Add(":val2", sitem.val2, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                                        db.Execute(sqlmx.ToString(), mx, trans);
                                    }
                                }
                                else
                                {
                                    var billid = isexist.FirstOrDefault().id;
                                    form.id = billid;
                                    form.xgr = _u.CurrentUser.name;
                                    db.Execute(sqlupdate.ToString(), form, trans);
                                    db.Execute(sqldel.ToString(), new { billid = billid }, trans);
                                    //明细
                                    foreach (var sitem in form.BillDetails)
                                    {
                                        DynamicParameters mx = new DynamicParameters();
                                        mx.Add(":billid", billid, System.Data.DbType.Int32, System.Data.ParameterDirection.Input);
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

        public bool Save_CheckDetail_His(string billid)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" insert into zxjc_check_bill_detail_his ");
                sql.Append(" (billid, checkid, checkval, val1, val2, lrr)  ");
                sql.Append(" select billid, checkid, checkval, val1, val2, :uname  ");
                sql.Append(" from zxjc_check_bill_detail  ");
                sql.Append(" where billid = :billid  ");
                using (var db = new OracleConnection(ConString))
                {
                   return db.Execute(sql.ToString(), new { billid = billid, uname = _u.CurrentUser.name }) > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

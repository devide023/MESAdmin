using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using ZDMesModels.CDGC;
using ZDToolHelper;
using Newtonsoft.Json;
using System.Web;

namespace ZDMesInterceptor.CDGC
{
    /// <summary>
    /// 交接班日志
    /// </summary>
    public class JJBLog: IInterceptor
    {
        private string _constr = string.Empty;
        private bool isadd = false;
        private string djk_olddata = string.Empty;
        private string gt_olddata = string.Empty;
        private StringBuilder sqllog = new StringBuilder();
        private List<zxjc_gtjjb_bill_detail> gtjjb_detail = new List<zxjc_gtjjb_bill_detail>();
        public JJBLog(string constr)
        {
            _constr = ConfigurationManager.ConnectionStrings[constr]?.ToString();
            sqllog.Append(" insert into mes_oper_log ");
            sqllog.Append(" (name, lx, olddata, newdata, czrq, czr, path, czrid) ");
            sqllog.Append(" values ");
            sqllog.Append(" (:name, :lx, :olddata, :newdata, :czrq, :czr, :path, :czrid)");
        }

        public void Intercept(IInvocation invocation)
        {
            try
            {
                var method_name = invocation.Method.Name.ToLower();
                switch (method_name)
                {
                    case "save_djkjjb":
                        DJJK_JJb_Before(invocation);
                        break;
                    case "save_gtjjb":
                        DT_JJb_Before(invocation);
                        break;
                    default:
                        break;
                }

                invocation.Proceed();

                switch (method_name)
                {
                    case "save_djkjjb":
                        DJJK_JJb_After(invocation);
                        break;
                    case "save_gtjjb":
                        DT_JJb_After(invocation);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 电机壳交接班
        /// </summary>
        /// <param name="invocation"></param>
        private void DJJK_JJb_Before(IInvocation invocation)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select id from zxjc_djkjjb_bill where rq = :rq and bc = :bc ");
                StringBuilder sqlbill = new StringBuilder();
                sqlbill.Append("select id, rq, bc, jbr, zlqk, sbqk, qtqk, lrr, lrsj, hxry from zxjc_djkjjb_bill where id = :id ");
                StringBuilder sqlmx = new StringBuilder();
                sqlmx.Append(" select id, billid, cpmc, kcsl, jgsl, gfsl, lfsl, hgsl, kcsysl FROM zxjc_djkjjb_detail where billid = :id ");
                StringBuilder sqlhx = new StringBuilder();
                sqlhx.Append("select id, billid, xmmc, trjgsl, dpssl, gfsl, lfsl, hgsl FROM zxjc_djkjjb_hx_detail where billid = :id");
                StringBuilder gfmxsql = new StringBuilder();
                gfmxsql.Append("select detailid, vin, yx FROM zxjc_djkjjb_gfmx where detailid = :mxid");
                using (var db = new OracleConnection(_constr))
                {
                    if (invocation.Arguments.Length > 0)
                    {
                       var typ = invocation.Arguments[0].GetType();
                        if(typ == typeof(zxjc_djkjjb_bill))
                        {
                            zxjc_djkjjb_bill bill = invocation.Arguments[0] as zxjc_djkjjb_bill;
                            var q = db.Query<int>(sql.ToString(), new { rq =Convert.ToDateTime(bill.rq?.ToString("yyyy-MM-dd")),bc=bill.bc});
                            isadd = q.Count() == 0 ? true : false;
                            if (!isadd)
                            {
                                var id = q.First();
                                var billobj =  db.Query<zxjc_djkjjb_bill>(sqlbill.ToString(), new { id = id }).First();
                                var mxlist = db.Query<zxjc_djkjjb_detail>(sqlmx.ToString(), new { id = id }).ToList();
                                foreach (var item in mxlist)
                                {
                                    item.gfmxlist = db.Query<zxjc_djkjjb_gfmx>(gfmxsql.ToString(), new { mxid = item.id }).ToList();
                                }
                                billobj.djkjjbdetail = mxlist;
                                billobj.djkjjbdetailhx = db.Query<zxjc_djkjjb_hx_detail>(sqlhx.ToString(), new { id = id }).ToList();
                                djk_olddata = JsonConvert.SerializeObject(billobj);
                            }
                        }
                    }
                    
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void DJJK_JJb_After(IInvocation invocation)
        {
            try
            {
                string url = HttpContext.Current.Request.Path;
                using (var db = new OracleConnection(_constr))
                {
                    if (invocation.Arguments.Length > 0)
                    {
                        var typ = invocation.Arguments[0].GetType();
                        if (typ == typeof(zxjc_djkjjb_bill))
                        {
                            zxjc_djkjjb_bill bill = invocation.Arguments[0] as zxjc_djkjjb_bill;
                            var retval = invocation.ReturnValue;
                            if (retval.GetType() == typeof(bool))
                            {
                                var czlx = isadd ? "add" : "update";
                                var uquery = db.Query<mes_user_entity>("select id,code,name from mes_user_entity where token = :token", new { token = TokenHelper.GetToken });
                                var uname = uquery.Count() > 0 ? uquery.First().name : "";
                                var uid = uquery.Count() > 0 ? uquery.First().id : 0;
                                var newdata = string.Empty;
                                if (invocation.Arguments.Length > 1)
                                {
                                    bill.djkjjbdetail = invocation.Arguments[1] as List<zxjc_djkjjb_detail>;
                                }
                                if (invocation.Arguments.Length > 2)
                                {
                                    bill.djkjjbdetailhx = invocation.Arguments[2] as List<zxjc_djkjjb_hx_detail>;
                                }
                                newdata = JsonConvert.SerializeObject(bill);

                                db.Execute(sqllog.ToString(), new { name = "zxjc_djkjjb_bill", lx = czlx, olddata = djk_olddata, newdata = newdata, czrq = DateTime.Now, czr = uname, path = url, czrid = uid });
                            }
                        }
                    }

                }                
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// 缸体交接班
        /// </summary>
        /// <param name="invocation"></param>
        private void DT_JJb_Before(IInvocation invocation)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select id from zxjc_gtjjb_bill where rq = :rq and bc = :bc ");
                StringBuilder sqlbill = new StringBuilder();
                sqlbill.Append("select id, rq, bc, jbr, dbzz, slry, mcry, jyry, zlqk, sbqk, qtqk, lrr, lrsj from zxjc_gtjjb_bill where id = :id ");
                StringBuilder sqlmx = new StringBuilder();
                sqlmx.Append("select id, billid, cpmc, sbcmpyl, dbmpsl, hcsl, trjgs, gfsl, lfsl, hgsl, dbmpyl  FROM zxjc_gtjjb_bill_detail where billid =:id");
                StringBuilder gfmxsql = new StringBuilder();
                gfmxsql.Append("select detailid, vin, yx FROM zxjc_gtjjb_gfmx where detailid = :mxid");
                using (var db = new OracleConnection(_constr))
                {
                    if (invocation.Arguments.Length > 0) {
                        var typ = invocation.Arguments[0].GetType();
                        if (typ == typeof(zxjc_gtjjb_bill))
                        {
                            zxjc_gtjjb_bill bill = invocation.Arguments[0] as zxjc_gtjjb_bill;
                            gtjjb_detail = bill.mxlist;
                            var q = db.Query<int>(sql.ToString(), new { rq = Convert.ToDateTime(bill.rq?.ToString("yyyy-MM-dd")), bc = bill.bc });
                            isadd = q.Count() == 0 ? true : false;
                            if (!isadd)
                            {
                                var id = q.First();
                                var billobj = db.Query<zxjc_gtjjb_bill>(sqlbill.ToString(), new { id = id }).First();
                                var mxlist = db.Query<zxjc_gtjjb_bill_detail>(sqlmx.ToString(), new { id = id }).ToList();
                                foreach (var item in mxlist)
                                {
                                    item.gfmxlist = db.Query<zxjc_gtjjb_gfmx>(gfmxsql.ToString(), new { mxid = item.id }).ToList();
                                }
                                billobj.mxlist = mxlist;
                                gt_olddata = JsonConvert.SerializeObject(billobj);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void DT_JJb_After(IInvocation invocation)
        {
            try
            {
                string url = HttpContext.Current.Request.Path;
                using (var db = new OracleConnection(_constr))
                {
                    if (invocation.Arguments.Length > 0)
                    {
                        var typ = invocation.Arguments[0].GetType();
                        if (typ == typeof(zxjc_gtjjb_bill))
                        {
                            zxjc_gtjjb_bill bill = invocation.Arguments[0] as zxjc_gtjjb_bill;
                            var retval = invocation.ReturnValue;
                            if (retval.GetType() == typeof(bool))
                            {
                                var czlx = isadd ? "add" : "update";
                                var uquery = db.Query<mes_user_entity>("select id,code,name from mes_user_entity where token = :token", new { token = TokenHelper.GetToken });
                                var uname = uquery.Count() > 0 ? uquery.First().name : "";
                                var uid = uquery.Count() > 0 ? uquery.First().id : 0;
                                bill.mxlist = gtjjb_detail;
                                var newdata = JsonConvert.SerializeObject(bill);
                                db.Execute(sqllog.ToString(), new { name = "zxjc_gtjjb_bill", lx = czlx, olddata = gt_olddata, newdata = newdata, czrq = DateTime.Now, czr = uname, path = url, czrid = uid });
                            }
                        }
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

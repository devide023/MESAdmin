using Castle.DynamicProxy;
using Dapper;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ZDMesModels;
using ZDMesModels.CDGC;
using ZDToolHelper;

namespace ZDMesInterceptor.CDGC
{
    public class GTJCLog : IInterceptor
    {
        private string _constr = string.Empty;
        private string gt_olddata = string.Empty;
        private bool isadd = false;
        public GTJCLog(string constr)
        {
            _constr = ConfigurationManager.ConnectionStrings[constr]?.ToString();
        }
        public void Intercept(IInvocation invocation)
        {
            try
            {
                var method_name = invocation.Method.Name.ToLower();
                switch (method_name)
                {
                    case "save_gtjc_checkdata":
                        GtJc_Before(invocation);
                        break;
                    case "create_bill_ids":
                        break;
                    default:
                        break;
                }

                invocation.Proceed();

                switch (method_name)
                {
                    case "save_gtjc_checkdata":
                        GtJc_After(invocation);
                        break;
                    case "create_bill_ids":
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

        private void GtJc_Before(IInvocation invocation)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select id from zxjc_gtjc_bill where rq = :rq and cplx=:cplx and vin=:vin ");
                StringBuilder sqlbill = new StringBuilder();
                sqlbill.Append("select id, cplx, rq, jyry, jylb, jth, vin, mh, pdjg, cljl, clr, clsj, lrr, lrsj, th  FROM   zxjc_gtjc_bill  where  id = :id");
                StringBuilder sqlmx = new StringBuilder();
                sqlmx.Append("select id, billid, jcid, cpfw, kxmc, kxcc, sdmj, kjval, sdmjval, jcjg  FROM   zxjc_gtjc_detail  where  billid = :id");
                using (var db = new OracleConnection(_constr))
                {
                    if (invocation.Arguments.Length > 0)
                    {
                        var typ = invocation.Arguments[0].GetType();
                        if (typ == typeof(zxjc_gtjc_bill))
                        {
                            zxjc_gtjc_bill bill = invocation.Arguments[0] as zxjc_gtjc_bill;
                            var q = db.Query<int>(sql.ToString(), new { rq = Convert.ToDateTime(bill.rq?.ToString("yyyy-MM-dd")), cplx = bill.cplx,vin=bill.vin });
                            isadd = q.Count() == 0 ? true : false;
                            if (!isadd)
                            {
                                var id = q.First();
                                var billobj = db.Query<zxjc_gtjc_bill>(sqlbill.ToString(), new { id = id }).First();
                                var mxlist = db.Query<zxjc_gtjc_detail>(sqlmx.ToString(), new { id = id }).ToList();
                                billobj.zxjcgtjcdetail = mxlist;
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
        private void GtJc_After(IInvocation invocation)
        {
            try
            {
                string url = HttpContext.Current.Request.Path;
                StringBuilder sqllog = new StringBuilder();
                sqllog.Append(" insert into mes_oper_log ");
                sqllog.Append(" (name, lx, olddata, newdata, czrq, czr, path, czrid) ");
                sqllog.Append(" values ");
                sqllog.Append(" (:name, :lx, :olddata, :newdata, :czrq, :czr, :path, :czrid)");
                using (var db = new OracleConnection(_constr))
                {
                    if (invocation.Arguments.Length > 0)
                    {
                        var typ = invocation.Arguments[0].GetType();
                        if (typ == typeof(zxjc_gtjc_bill))
                        {
                            zxjc_gtjc_bill bill = invocation.Arguments[0] as zxjc_gtjc_bill;
                            var retval = invocation.ReturnValue;
                            if (retval.GetType() == typeof(bool))
                            {
                                var czlx = isadd ? "add" : "update";
                                var uquery = db.Query<mes_user_entity>("select id,code,name from mes_user_entity where token = :token", new { token = TokenHelper.GetToken });
                                var uname = uquery.Count() > 0 ? uquery.First().name : "";
                                var uid = uquery.Count() > 0 ? uquery.First().id : 0;
                                var newdata = JsonConvert.SerializeObject(bill);
                                DynamicParameters p = new DynamicParameters();
                                p.Add(":name", "zxjc_gtjc_bill", DbType.String, ParameterDirection.Input);
                                p.Add(":lx", czlx, DbType.String, ParameterDirection.Input);
                                p.Add(":olddata", gt_olddata, DbType.String, ParameterDirection.Input);
                                p.Add(":newdata", newdata, DbType.String, ParameterDirection.Input);
                                p.Add(":czrq", DateTime.Now, DbType.DateTime, ParameterDirection.Input);
                                p.Add(":czr", uname, DbType.String, ParameterDirection.Input);
                                p.Add(":path", url, DbType.String, ParameterDirection.Input);
                                p.Add(":czrid", uid, DbType.Int32, ParameterDirection.Input);
                                db.Execute(sqllog.ToString(), p);
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

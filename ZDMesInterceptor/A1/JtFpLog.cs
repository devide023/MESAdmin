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
using ZDMesModels.TJ.A1;
using ZDToolHelper;

namespace ZDMesInterceptor.A1
{
    /// <summary>
    /// 技通分配日志
    /// </summary>
    public class JtFpLog : IInterceptor
    {
        private string _constr = string.Empty;
        public JtFpLog(string constr)
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
                    case "jstz_to_scx":
                        JtToScxBefore(invocation);
                        break;
                    default:
                        break;
                }

                invocation.Proceed();

                switch (method_name)
                {
                    case "jstz_to_scx":
                        JtToScxAfter(invocation);
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

        private void JtToScxBefore(IInvocation invocation)
        {
            try
            {
                
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void JtToScxAfter(IInvocation invocation)
        {
            try
            {
                string url = HttpContext.Current.Request.Path;
                StringBuilder sql = new StringBuilder();
                sql.Append("select jtid, jcbh, jcmc, jcms, wjlj, jwdx, scry, scpc, scsj, yxqx1, yxqx2, gcdm, fp_flg as fpflg, fp_sj as fpsj, fpr, wjfl, scx, lrr, lrsj, jtly  from  zxjc_t_jstc  where  jcbh in :jcbh ");
                StringBuilder sqllog = new StringBuilder();
                sqllog.Append(" insert into mes_oper_log ");
                sqllog.Append(" (name, lx, olddata, newdata, czrq, czr, path, czrid) ");
                sqllog.Append(" values ");
                sqllog.Append(" (:name, :lx, :olddata, :newdata, :czrq, :czr, :path, :czrid)");
                if (invocation.Arguments.Length > 0)
                {
                    var typ = invocation.Arguments[0].GetType();
                    if (typ == typeof(List<zxjc_t_jstc_scx>))
                    {
                        var list = invocation.Arguments[0] as IEnumerable<zxjc_t_jstc_scx>;
                        var jcbhlist = list.Select(t => t.jcbh).Distinct();
                        var retval = invocation.ReturnValue;
                        if(retval.GetType() == typeof(bool))
                        {
                            var isok = Convert.ToBoolean(retval);
                            if (isok)
                            {
                                using (var db = new OracleConnection(_constr))
                                {
                                    var bills = db.Query<zxjc_t_jstc>(sql.ToString(), new { jcbh = jcbhlist });
                                    var uquery = db.Query<mes_user_entity>("select id,code,name from mes_user_entity where token = :token", new { token = TokenHelper.GetToken });
                                    var uname = uquery.Count() > 0 ? uquery.First().name : "";
                                    var uid = uquery.Count() > 0 ? uquery.First().id : 0;
                                    var newdata = JsonConvert.SerializeObject(bills);
                                    DynamicParameters p = new DynamicParameters();
                                    p.Add(":name", "zxjc_t_jstc", DbType.String, ParameterDirection.Input);
                                    p.Add(":lx", "add", DbType.String, ParameterDirection.Input);
                                    p.Add(":olddata", "", DbType.String, ParameterDirection.Input);
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
                
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

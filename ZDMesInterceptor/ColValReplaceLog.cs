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
using System.Web.UI.WebControls.WebParts;
using ZDMesModels;
using ZDMesServices;
using ZDMesServices.Common;
using ZDToolHelper;

namespace ZDMesInterceptor
{
    public class ColValReplaceLog : OracleBaseFixture,IInterceptor
    {
        private mes_oper_log updatelog = new mes_oper_log();
        private UserUtilService _userservice;
        private StringBuilder sqllog = new StringBuilder();
        public ColValReplaceLog(string constr):base(constr)
        {
            _userservice = new UserUtilService(constr);
            sqllog.Append(" insert into mes_oper_log ");
            sqllog.Append(" (name, lx, olddata, newdata, czrq, czr, path, czrid) ");
            sqllog.Append(" values ");
            sqllog.Append(" (:name, :lx, :olddata, :newdata, :czrq, :czr, :path, :czrid)");
        }
        public void Intercept(IInvocation invocation)
        {
            var methodname = invocation.Method.Name.ToLower();
            switch (methodname)
            {
                case "replace_column_value":
                    Before_ColVal(invocation);
                    break;
                default:
                    break;
            }

            invocation.Proceed();

            switch (methodname)
            {
                case "replace_column_value":
                    After_ColVal(invocation);
                    break;
                default:
                    break;
            }
        }

        private void Before_ColVal(IInvocation invocation)
        {
            string url = HttpContext.Current.Request.Path;
            var arg = invocation.Arguments[0] as sys_colval_replace;
            var sqlinfo = ColValReplaceHelper.Get_Replace_Exp(arg);
            using (var db = new OracleConnection(ConString))
            {
                var list = db.Query(sqlinfo.sql_all_cols, sqlinfo.select_param);
                updatelog.olddata = JsonConvert.SerializeObject(list);
                updatelog.path = url;
                updatelog.name= sqlinfo.tablename;
                updatelog.lx = "列批量替换";
                updatelog.czr = _userservice.CurrentUser.name;
                updatelog.czrq = DateTime.Now;
                updatelog.czrid = _userservice.CurrentUser.id;
            }
        }
        private void After_ColVal(IInvocation invocation)
        {
            string url = HttpContext.Current.Request.Path;
            var arg = invocation.Arguments[0] as sys_colval_replace;
            var sqlinfo = ColValReplaceHelper.Get_Replace_Exp(arg);
            using (var db = new OracleConnection(ConString))
            {
                var list = db.Query(sqlinfo.sql_all_cols, sqlinfo.select_param);
                DynamicParameters p = new DynamicParameters();
                p.Add(":name", updatelog.name, DbType.String, ParameterDirection.Input);
                p.Add(":lx", updatelog.lx, DbType.String, ParameterDirection.Input);
                p.Add(":olddata", updatelog.olddata, DbType.String, ParameterDirection.Input);
                p.Add(":newdata", JsonConvert.SerializeObject(list), DbType.String, ParameterDirection.Input);
                p.Add(":czrq", updatelog.czrq, DbType.DateTime, ParameterDirection.Input);
                p.Add(":czr", updatelog.czr, DbType.String, ParameterDirection.Input);
                p.Add(":path", updatelog.path, DbType.String, ParameterDirection.Input);
                p.Add(":czrid", updatelog.czrid, DbType.Int32, ParameterDirection.Input);
                db.Execute(sqllog.ToString(), p);
            }
        }
    }
}

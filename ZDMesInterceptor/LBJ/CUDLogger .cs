using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Dapper;
using System.Data;
using Newtonsoft.Json;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;
using ZDToolHelper;
using ZDMesInterfaces.Common;
using DapperExtensions;
using DapperExtensions.Mapper;
using System.Reflection;
using DapperExtensions.Sql;
using ZDMesModels;
using System.Text;
namespace ZDMesInterceptor.LBJ
{
    /// <summary>
    /// 基础增、删、改日志
    /// </summary>
    public class CUDLogger : IInterceptor
    {
        private mes_oper_log updatelog = new mes_oper_log();
        private string _oracleconstr = string.Empty;
        private mes_user_entity userinfo;
        private SqlGeneratorImpl sqlGenerator;
        public CUDLogger(string constr)
        {
            _oracleconstr = ConfigurationManager.ConnectionStrings[constr]?.ToString();
            var config = new DapperExtensionsConfiguration(typeof(AutoClassMapper<>), new List<Assembly>(), new OracleDialect());
            sqlGenerator = new SqlGeneratorImpl(config);
        }
        public void Intercept(IInvocation invocation)
        {
            switch (invocation.Method.Name.ToLower())
            {
                case "add":
                    BeforeAddLog(invocation);
                    break;
                case "del":
                    BeforeDelLog(invocation);
                    break;
                case "modify":
                     BeforeUpdate(invocation);
                    break;
                default:
                    break;
            }

            invocation.Proceed();

            switch (invocation.Method.Name.ToLower())
            {
                case "add":
                    AfterAddLog(invocation);
                    break;
                case "del":
                    AfterDelLog(invocation);
                    break;
                case "modify":
                    AfterUpdate(invocation);
                    break;
                default:
                    break;
            }
        }

        private void BeforeUpdate(IInvocation invocation)
        {
            try
            {
                string url = HttpContext.Current.Request.Path;
                StringBuilder sql = new StringBuilder();
                sql.Append("select cu.* ");
                sql.Append(" from user_cons_columns cu, user_constraints au ");
                sql.Append(" where  cu.constraint_name = au.constraint_name ");
                sql.Append(" and    au.constraint_type = 'P'");
                sql.Append(" and    au.table_name = :tbname ");
                StringBuilder sql1 = new StringBuilder();
                sql1.Append("select * from :tbname where 1=1 ");
                using (var db = new OracleConnection(_oracleconstr))
                {
                    if (invocation.Arguments.Length > 0)
                    {
                        userinfo = db.Query<mes_user_entity>("select id,code,name from mes_user_entity where token = :token", new { token = ZDToolHelper.TokenHelper.GetToken }).FirstOrDefault();
                        updatelog.czr = userinfo.name;
                        updatelog.czrid = userinfo.id;
                        updatelog.czrq = DateTime.Now;
                        updatelog.lx = "update";
                        updatelog.path = url;
                        DynamicParameters p = new DynamicParameters();
                        if (invocation.Arguments[0].GetType().Name.Contains("List"))
                        {
                            var parlist = invocation.Arguments[0] as IEnumerable<object>;
                            var t = parlist.First().GetType();
                            var list = db.Query(sql.ToString(), new { tbname = t.Name.ToUpper() });
                            foreach (var item in list)
                            {
                                sql1.Append($" and {item.COLUMN_NAME} in :{item.COLUMN_NAME} ");
                                PropertyInfo pi = t.GetProperty(item.COLUMN_NAME.ToLower().Replace("_",""));
                                List<object> values = new List<object>();
                                foreach (var sitem in parlist)
                                {
                                    values.Add(pi.GetValue(sitem));
                                }
                                p.Add($":{item.COLUMN_NAME}", values);
                            }
                            sql1.Replace(":tbname", t.Name);
                            var olddata = db.Query(sql1.ToString(), p);
                            updatelog.name = t.Name;
                            updatelog.olddata = JsonConvert.SerializeObject(olddata).ToLower();
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void BeforeDelLog(IInvocation invocation)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void AfterUpdate(IInvocation invocation)
        {
            try
            {
                if (Convert.ToBoolean(invocation.ReturnValue))
                {
                    if (invocation.Arguments.Length > 0)
                    {
                        using (var db = new OracleConnection(_oracleconstr))
                        {
                            IDatabase Db = new Database(db, sqlGenerator);
                            try
                            {
                                updatelog.newdata = JsonConvert.SerializeObject(invocation.Arguments[0]);
                                Db.Insert<mes_oper_log>(updatelog);
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                            finally
                            {
                                db.Close();
                                Db.Dispose();
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

        private void AfterDelLog(IInvocation invocation)
        {
            try
            {
                if (Convert.ToBoolean(invocation.ReturnValue))
                {
                    string url = HttpContext.Current.Request.Path;
                    if (invocation.Arguments.Length > 0)
                    {
                        string tbname = string.Empty;
                        if (invocation.Arguments[0].GetType().Name.Contains("List"))
                        {
                            var t = (invocation.Arguments[0] as IEnumerable<object>).First().GetType();
                            tbname = t.Name;
                        }
                        using (var db = new OracleConnection(_oracleconstr))
                        {
                            userinfo = db.Query<mes_user_entity>("select id,code,name from mes_user_entity where token = :token", new { token = ZDToolHelper.TokenHelper.GetToken }).FirstOrDefault();
                            IDatabase Db = new Database(db, sqlGenerator);
                            try
                            {
                                Db.Insert<mes_oper_log>(new mes_oper_log()
                                {
                                    name = tbname,
                                    czr = userinfo.name,
                                    czrid = userinfo.id,
                                    lx = "del",
                                    czrq = DateTime.Now,
                                    path = url,
                                    newdata = JsonConvert.SerializeObject(invocation.Arguments[0])
                                });
                            }
                            catch(Exception)
                            {
                                throw;
                            }
                            finally
                            {
                                db.Close();
                                Db.Dispose();
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

        private void BeforeAddLog(IInvocation invocation)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void AfterAddLog(IInvocation invocation)
        {
            try
            {
                if (Convert.ToInt32(invocation.ReturnValue) > 0)
                {
                    string url = HttpContext.Current.Request.Path;
                    if (invocation.Arguments.Length > 0)
                    {
                        string tbname = string.Empty;
                        if (invocation.Arguments[0].GetType().Name.Contains("List"))
                        {
                            var t = (invocation.Arguments[0] as IEnumerable<object>).First().GetType();
                            tbname = t.Name;
                        }
                        using (var db = new OracleConnection(_oracleconstr))
                        {
                            userinfo = db.Query<mes_user_entity>("select id,code,name from mes_user_entity where token = :token", new { token = ZDToolHelper.TokenHelper.GetToken }).FirstOrDefault();
                            IDatabase Db = new Database(db, sqlGenerator);
                            try
                            {
                                Db.Insert<mes_oper_log>(new mes_oper_log()
                                {
                                    name = tbname,
                                    czr = userinfo.name,
                                    czrid = userinfo.id,
                                    lx = "add",
                                    czrq = DateTime.Now,
                                    path = url,
                                    newdata = JsonConvert.SerializeObject(invocation.Arguments[0])
                                });
                            }
                            catch (Exception)
                            {

                                throw;
                            }
                            finally
                            {
                                db.Close();
                                Db.Dispose();
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
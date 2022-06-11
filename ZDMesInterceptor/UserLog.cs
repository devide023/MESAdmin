using Castle.DynamicProxy;
using Dapper;
using DapperExtensions;
using DapperExtensions.Mapper;
using DapperExtensions.Sql;
using Newtonsoft.Json;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ZDMesModels;

namespace ZDMesInterceptor
{
    /// <summary>
    /// 用户登录、登出日志
    /// </summary>
    public class UserLog : IInterceptor
    {
        private mes_oper_log updatelog = new mes_oper_log();
        private string _oracleconstr = string.Empty;
        private SqlGeneratorImpl sqlGenerator;
        private mes_user_entity _userinfo;
        public UserLog(string constr)
        {
            _oracleconstr = ConfigurationManager.ConnectionStrings[constr]?.ToString();
            var config = new DapperExtensionsConfiguration(typeof(AutoClassMapper<>), new List<Assembly>(), new OracleDialect());
            sqlGenerator = new SqlGeneratorImpl(config);
        }
        public void Intercept(IInvocation invocation)
        {
            switch (invocation.Method.Name.ToString().ToLower())
            {
                case "logout":
                    Before_Logout_Log(invocation);
                    break;
                default:
                    break;
            }
            invocation.Proceed();

            switch (invocation.Method.Name.ToString().ToLower())
            {
                case "login":
                    Login_Log(invocation);
                    break;
                case "logout":
                    Logout_Log(invocation);
                    break;
                case "changepwd":
                    ChangePwd_Log(invocation);
                    break;
                case "resetpwd":
                    ResetPwd(invocation);
                    break;
                default:
                    break;
            }
        }

        void Before_Logout_Log(IInvocation invocation)
        {
            try
            {
                using (var db = new OracleConnection(_oracleconstr))
                {
                    _userinfo = db.Query<mes_user_entity>("select id,code,name from mes_user_entity where token = :token", new { token = ZDToolHelper.TokenHelper.GetToken }).FirstOrDefault();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void ResetPwd(IInvocation invocation)
        {
            try
            {
                string url = HttpContext.Current.Request.Path;
                if (Convert.ToBoolean(invocation.ReturnValue))
                {
                    using (var db = new OracleConnection(_oracleconstr))
                    {
                        IDatabase Db = new Database(db, sqlGenerator);
                        try
                        {
                            var userinfo = db.Query<mes_user_entity>("select id,code,name from mes_user_entity where token = :token", new { token = ZDToolHelper.TokenHelper.GetToken }).FirstOrDefault();
                            Db.Insert<mes_oper_log>(new mes_oper_log()
                            {
                                name = "mes_user_entity",
                                czr = userinfo.name,
                                czrid = userinfo.id,
                                lx = "重置密码",
                                czrq = DateTime.Now,
                                path = url,
                                newdata = JsonConvert.SerializeObject(new { msg = "重置密码成功" })
                            });
                        }
                        finally
                        {
                            db.Close();
                            Db.Dispose();
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void ChangePwd_Log(IInvocation invocation)
        {
            try
            {
                string url = HttpContext.Current.Request.Path;
                if (Convert.ToBoolean(invocation.ReturnValue))
                {
                    using (var db = new OracleConnection(_oracleconstr))
                    {
                        IDatabase Db = new Database(db, sqlGenerator);
                        try
                        {
                            var userinfo = db.Query<mes_user_entity>("select id,code,name from mes_user_entity where token = :token", new { token = ZDToolHelper.TokenHelper.GetToken }).FirstOrDefault();
                            Db.Insert<mes_oper_log>(new mes_oper_log()
                            {
                                name = "mes_user_entity",
                                czr = userinfo.name,
                                czrid = userinfo.id,
                                lx = "修改密码",
                                czrq = DateTime.Now,
                                path = url,
                                newdata = JsonConvert.SerializeObject(new { msg = "修改密码成功" })
                            });

                        }
                        finally
                        {
                            db.Close();
                            Db.Dispose();
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Logout_Log(IInvocation invocation)
        {
            try
            {
                string url = HttpContext.Current.Request.Path;
                if (Convert.ToBoolean(invocation.ReturnValue))
                {
                    using (var db = new OracleConnection(_oracleconstr))
                    {
                        IDatabase Db = new Database(db, sqlGenerator);
                        try
                        {
                            Db.Insert<mes_oper_log>(new mes_oper_log()
                            {
                                name = "mes_user_entity",
                                czr = _userinfo.name,
                                czrid = _userinfo.id,
                                lx = "退出",
                                czrq = DateTime.Now,
                                path = url,
                                newdata = JsonConvert.SerializeObject(new {msg="成功退出" })
                            });

                        }
                        finally
                        {
                            db.Close();
                            Db.Dispose();
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Login_Log(IInvocation invocation)
        {
            try
            {
                sys_login_result result = invocation.ReturnValue as sys_login_result;
                string url = HttpContext.Current.Request.Path;
                if (result.code == 1)
                {
                    using (var db = new OracleConnection(_oracleconstr))
                    {
                        IDatabase Db = new Database(db, sqlGenerator);
                        try
                        {
                            var userinfo = db.Query<mes_user_entity>("select id,code,name from mes_user_entity where token = :token", new { token = result.token }).FirstOrDefault();
                            Db.Insert<mes_oper_log>(new mes_oper_log()
                            {
                                name = "mes_user_entity",
                                czr = userinfo.name,
                                czrid = userinfo.id,
                                lx = "登录",
                                czrq = DateTime.Now,
                                path = url,
                                newdata = JsonConvert.SerializeObject(new {result.code,result.msg })
                            });
                        }
                        finally
                        {
                            db.Close();
                            Db.Dispose();
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

using Castle.DynamicProxy;
using DapperExtensions;
using DapperExtensions.Mapper;
using DapperExtensions.Sql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using Oracle.ManagedDataAccess.Client;
using Dapper;
using System.Web;
using ZDMesModels.LBJ;
using Newtonsoft.Json;

namespace ZDMesInterceptor.LBJ
{
    /// <summary>
    /// 刀具日志
    /// </summary>
    public class DaoJuLog : IInterceptor
    {
        private mes_oper_log updatelog = new mes_oper_log();
        private string _oracleconstr = string.Empty;
        private SqlGeneratorImpl sqlGenerator;
        private List<base_dbrjzx> olddata_list = new List<base_dbrjzx>();
        private List<base_dbrjzx> olddata_rjzx_list = new List<base_dbrjzx>();
        public DaoJuLog(string constr)
        {
            _oracleconstr = ConfigurationManager.ConnectionStrings[constr]?.ToString();
            var config = new DapperExtensionsConfiguration(typeof(AutoClassMapper<>), new List<Assembly>(), new OracleDialect());
            sqlGenerator = new SqlGeneratorImpl(config);
        }
        public void Intercept(IInvocation invocation)
        {
            switch (invocation.Method.Name.ToString().ToLower())
            {
                //刃具刃磨
                case "setrjsm":
                    Before_Rjrm_Log(invocation);
                    break;
                case "gxsm":
                    Before_GxSm(invocation);
                    break;
                default:
                    break;
            }

            invocation.Proceed();

            switch (invocation.Method.Name.ToString().ToLower())
            {
                //卸载刃具信息
                case "uninstallrjxx":
                    UnInstall_RjxxLog(invocation);
                    break;
                //刀柄刃具领用
                case "daobinrenjuly":
                    RenJu_Ly_Log(invocation);
                    break;
                //刀柄刃具更换
                case "save_dbrjzx_change":
                    Dbrjgh_Log(invocation);
                    break;
                //刃具刃磨
                case "setrjsm":
                    After_Rjrm_Log(invocation);
                    break;
                case "gxsm":
                    After_Gxsm(invocation);
                    break;
                default:
                    break;
            }

        }

        private void Before_GxSm(IInvocation invocation)
        {
            try
            {
                var parm = invocation.Arguments[0] as List<base_dbrjzx>;
                StringBuilder sql = new StringBuilder();
                sql.Append("select * from base_dbrjzx where id in :id ");
                using (var db = new OracleConnection(_oracleconstr))
                {
                    var ids = parm.Select(t => t.id).ToList();
                    olddata_rjzx_list = db.Query<base_dbrjzx>(sql.ToString(), new { id = ids }).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void After_Gxsm(IInvocation invocation) {
            try
            {
                string url = HttpContext.Current.Request.Path;
                var parm = invocation.Arguments[0] as List<base_dbrjzx>;
                StringBuilder sql = new StringBuilder();
                sql.Append("select * from base_dbrjzx where id in :id ");
                using (var db = new OracleConnection(_oracleconstr))
                {
                    IDatabase Db = new Database(db, sqlGenerator);
                    try
                    {
                        var ids = parm.Select(t => t.id).ToList();
                        var newlist = db.Query<base_dbrjzx>(sql.ToString(), new { id = ids }).ToList();
                        var userinfo = db.Query<mes_user_entity>("select id,code,name from mes_user_entity where token = :token", new { token = ZDToolHelper.TokenHelper.GetToken }).FirstOrDefault();
                        if (Convert.ToBoolean(invocation.ReturnValue) && olddata_rjzx_list.Count > 0)
                        {
                            Db.Insert<mes_oper_log>(new mes_oper_log()
                            {
                                name = "base_dbrjzx",
                                czr = userinfo.name,
                                czrid = userinfo.id,
                                lx = "刀具标准寿命更新",
                                czrq = DateTime.Now,
                                path = url,
                                olddata = JsonConvert.SerializeObject(olddata_rjzx_list),
                                newdata = JsonConvert.SerializeObject(newlist)
                            });
                        }
                    }
                    finally
                    {
                        db.Close();
                        Db?.Dispose();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void After_Rjrm_Log(IInvocation invocation)
        {
            try
            {
                string url = HttpContext.Current.Request.Path;
                IEnumerable<int> rjzxids = new List<int>();
                if (invocation.Arguments.Length > 0)
                {
                    var parm = invocation.Arguments[0];
                    var typname = parm.GetType().Name;
                    if (typname.Contains("List"))
                    {
                        rjzxids = parm as IEnumerable<int>;
                    }
                }
                StringBuilder sql = new StringBuilder();
                sql.Append("select * from base_dbrjzx where id in :id ");
                using (var db = new OracleConnection(_oracleconstr))
                {
                    IDatabase Db = new Database(db, sqlGenerator);
                    var userinfo = db.Query<mes_user_entity>("select id,code,name from mes_user_entity where token = :token", new { token = ZDToolHelper.TokenHelper.GetToken }).FirstOrDefault();
                    try
                    {
                        if(Convert.ToBoolean(invocation.ReturnValue) && olddata_list.Count>0)
                        {
                            var q = db.Query<base_dbrjzx>(sql.ToString(), new { id = rjzxids });
                            Db.Insert<mes_oper_log>(new mes_oper_log()
                            {
                                name = "base_dbrjzx",
                                czr = userinfo.name,
                                czrid = userinfo.id,
                                lx = "刀具刃磨",
                                czrq = DateTime.Now,
                                path = url,
                                olddata = JsonConvert.SerializeObject(olddata_list),
                                newdata = JsonConvert.SerializeObject(q)
                            });
                        }
                    }
                    finally
                    {
                        db.Close();
                        Db.Dispose();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Before_Rjrm_Log(IInvocation invocation)
        {
            try
            {
                IEnumerable<int> rjzxids = new List<int>();
                if (invocation.Arguments.Length > 0)
                {
                    var parm = invocation.Arguments[0];
                    var typname = parm.GetType().Name;
                    if (typname.Contains("List"))
                    {
                        rjzxids = parm as IEnumerable<int>;
                    }
                }
                StringBuilder sql = new StringBuilder();
                sql.Append("select * from base_dbrjzx where id in :id ");
                using (var db = new OracleConnection(_oracleconstr))
                {
                    IDatabase Db = new Database(db, sqlGenerator);
                    try
                    {
                        var q = db.Query<base_dbrjzx>(sql.ToString(), new { id = rjzxids });
                        olddata_list.AddRange(q);
                    }
                    finally
                    {
                        db.Close();
                        Db.Dispose();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Dbrjgh_Log(IInvocation invocation)
        {
            try
            {
                try
                {
                    string url = HttpContext.Current.Request.Path;
                    sys_dbrj_bgly_form form = new sys_dbrj_bgly_form();
                    if (invocation.Arguments.Length > 0)
                    {
                        form = invocation.Arguments[0] as sys_dbrj_bgly_form;
                    }
                    using (var db = new OracleConnection(_oracleconstr))
                    {
                        IDatabase Db = new Database(db, sqlGenerator);
                        var userinfo = db.Query<mes_user_entity>("select id,code,name from mes_user_entity where token = :token", new { token = ZDToolHelper.TokenHelper.GetToken }).FirstOrDefault();
                        StringBuilder sql = new StringBuilder();
                        sql.Append("select * from base_dbrjzx where scx = :scx and sbbh = :sbbh and rjid in :rjid ");
                        //刃具对应关系
                        StringBuilder sqldygx = new StringBuilder();
                        sqldygx.Append(" select *  FROM   base_dbrjgx where id in :id ");
                        try
                        {
                            var rjids = db.Query<base_dbrjgx>(sqldygx.ToString(), new { id = form.dbrjgxid }).Select(t => t.rjid);
                            var q = db.Query<base_dbrjzx>(sql.ToString(), new { scx = form.scx, sbbh = form.sbbh, rjid = rjids });
                            Db.Insert<mes_oper_log>(new mes_oper_log()
                            {
                                name = "base_dbrjzx",
                                czr = userinfo.name,
                                czrid = userinfo.id,
                                lx = "刀具更换",
                                czrq = DateTime.Now,
                                path = url,
                                newdata = JsonConvert.SerializeObject(q)
                            });
                        }
                        finally
                        {
                            db.Close();
                            Db.Dispose();
                        }
                    }

                }
                catch (Exception)
                {

                    throw;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void RenJu_Ly_Log(IInvocation invocation)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select * from base_dbrjzx where scx = :scx and sbbh = :sbbh and rjid in :rjid ");
                //刃具对应关系
                StringBuilder sqldygx = new StringBuilder();
                sqldygx.Append(" select *  FROM   base_dbrjgx where id in :id ");
                string url = HttpContext.Current.Request.Path;
                dbrjlyform form = new dbrjlyform();
                if (invocation.Arguments.Length > 0)
                {
                    form = invocation.Arguments[0] as dbrjlyform;
                }
                if (Convert.ToBoolean(invocation.ReturnValue) == true)
                {
                    using (var db = new OracleConnection(_oracleconstr))
                    {
                        IDatabase Db = new Database(db, sqlGenerator);
                        try
                        {
                            var userinfo = db.Query<mes_user_entity>("select id,code,name from mes_user_entity where token = :token", new { token = ZDToolHelper.TokenHelper.GetToken }).FirstOrDefault();
                            var rjids = db.Query<base_dbrjgx>(sqldygx.ToString(), new { id = form.dbrjids }).Select(t => t.rjid);
                            var q = db.Query<base_dbrjzx>(sql.ToString(), new { scx = form.scx, sbbh = form.sbbh, rjid = rjids });
                            Db.Insert<mes_oper_log>(new mes_oper_log()
                            {
                                name = "base_dbrjzx",
                                czr = userinfo.name,
                                czrid = userinfo.id,
                                lx = "刀具领用",
                                czrq = DateTime.Now,
                                path = url,
                                newdata = JsonConvert.SerializeObject(q)
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

        private void UnInstall_RjxxLog(IInvocation invocation)
        {
            try
            {
                string url = HttpContext.Current.Request.Path;
                IEnumerable<int> rjzxids = new List<int>();
                if (invocation.Arguments.Length > 0)
                {
                    var parm = invocation.Arguments[0];
                    var typname = parm.GetType().Name;
                    if (typname.Contains("List"))
                    {
                        rjzxids = parm as IEnumerable<int>;
                    }
                }
                using (var db = new OracleConnection(_oracleconstr))
                {
                    IDatabase Db = new Database(db, sqlGenerator);
                    try
                    {
                        var userinfo = db.Query<mes_user_entity>("select id,code,name from mes_user_entity where token = :token", new { token = ZDToolHelper.TokenHelper.GetToken }).FirstOrDefault();
                        var q = Db.GetList<base_dbrjzx>(Predicates.Field<base_dbrjzx>(t => t.id, DapperExtensions.Predicate.Operator.Eq, rjzxids));
                        if(Convert.ToBoolean(invocation.ReturnValue) == true)
                        {
                            if (q.Count() > 0)
                            {
                                Db.Insert<mes_oper_log>(new mes_oper_log()
                                {
                                    name = "base_dbrjzx",
                                    czr = userinfo.name,
                                    czrid = userinfo.id,
                                    lx = "刃具卸载",
                                    czrq = DateTime.Now,
                                    path = url,
                                    newdata = JsonConvert.SerializeObject(q)
                                });
                            }
                        }
                    }
                    finally
                    {
                        db.Close();
                        Db.Dispose();
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

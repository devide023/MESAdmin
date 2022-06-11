using Castle.DynamicProxy;
using DapperExtensions;
using DapperExtensions.Mapper;
using DapperExtensions.Sql;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ZDMesModels;
using Oracle.ManagedDataAccess.Client;
using Dapper;
using ZDToolHelper;

namespace ZDMesInterceptor.LBJ
{
    /// <summary>
    /// 数据导入日志
    /// </summary>
    public class ImportLog: IInterceptor
    {
        private mes_oper_log updatelog = new mes_oper_log();
        private string _oracleconstr = string.Empty;
        private SqlGeneratorImpl sqlGenerator;
        public ImportLog(string constr)
        {
            _oracleconstr = ConfigurationManager.ConnectionStrings[constr]?.ToString();
            var config = new DapperExtensionsConfiguration(typeof(AutoClassMapper<>), new List<Assembly>(), new OracleDialect());
            sqlGenerator = new SqlGeneratorImpl(config);
        }

        public void Intercept(IInvocation invocation)
        {
            switch (invocation.Method.Name.ToLower())
            {
                case "newimportdata":
                    BeforeImport_New(invocation);//新增导入
                    break;
                case "reaplaceimportdata":
                    BeforeImport_Replace(invocation);//替换导入
                    break;
                case "zhimportdata":
                    BeforeImport_ZH(invocation);//综合导入
                    break;
                default:
                    break;
            }

            invocation.Proceed();

            switch (invocation.Method.Name.ToLower())
            {
                case "newimportdata":
                    AfterImport_New(invocation);//新增导入
                    break;
                case "reaplaceimportdata":
                    AfterImport_Replace(invocation);//替换导入
                    break;
                case "zhimportdata":
                    AfterImport_ZH(invocation);//综合导入
                    break;
                default:
                    break;
            }
        }


        private void BeforeImport_New(IInvocation invocation)
        {
            
        }
        private void BeforeImport_Replace(IInvocation invocation) { }
        private void BeforeImport_ZH(IInvocation invocation) { }
        private void AfterImport_New(IInvocation invocation)
        {
            Save_Log(invocation, ImportType.新增导入);
        }
        private void AfterImport_Replace(IInvocation invocation) {
            Save_Log(invocation, ImportType.替换导入);
        }
        private void AfterImport_ZH(IInvocation invocation) {
            Save_Log(invocation, ImportType.综合导入);
        }

        private enum ImportType
        {
            新增导入=1,
            替换导入=2,
            综合导入 = 4,
        }

        private void Save_Log(IInvocation invocation, ImportType czlx)
        {
            try
            {
                string url = HttpContext.Current.Request.Path;
                string tbname = string.Empty;
                if (invocation.Arguments.Length > 0)
                {
                    var parm = invocation.Arguments[0];
                    var typname = parm.GetType().Name;
                    if (typname.Contains("List"))
                    {
                        var l = parm as IEnumerable<object>;
                        tbname = l.FirstOrDefault().GetType().Name;
                    }
                }
                Type T = typeof(sys_import_result<>);
                //泛型类型名称
                var fxtyname = invocation.ReturnValue.GetType().GetGenericArguments()[0].FullName + ",ZDMesModels";
                //返回类型
                var returntype = T.MakeGenericType(new[] { Type.GetType(fxtyname) });
                PropertyInfo[] pros = returntype.GetProperties();

                using (var db = new OracleConnection(_oracleconstr))
                {
                    var userinfo = db.Query<mes_user_entity>("select id,code,name from mes_user_entity where token = :token", new { token = ZDToolHelper.TokenHelper.GetToken }).FirstOrDefault();
                    IDatabase Db = new Database(db, sqlGenerator);
                    try
                    {
                        IEnumerable<object> insert_list = new List<object>();
                        IEnumerable<object> del_list = new List<object>();
                        IEnumerable<object> update_list = new List<object>();
                        //是否存在orginallist属性
                        var sfcz_gxjl = pros.Where(t => t.Name == "orginallist");
                        if (sfcz_gxjl.Count() > 0)
                        {
                            //获取更新之前原始记录
                            var gxjl = sfcz_gxjl.First().GetValue(invocation.ReturnValue) as IEnumerable<object>;
                            if (gxjl.Count() > 0)
                            {
                                update_list = gxjl;
                            }
                        }
                        //是否存在dellist属性
                        var sfcz_scjl = pros.Where(t => t.Name == "dellist");
                        if (sfcz_scjl.Count() > 0)
                        {
                            //获取删除之前原始记录
                            var scjl = sfcz_scjl.First().GetValue(invocation.ReturnValue) as IEnumerable<object>;
                            if (scjl.Count() > 0)
                            {
                                del_list = scjl;
                            }
                        }
                        //是否存在oklist属性
                        var sfczok = pros.Where(t => t.Name == "oklist");
                        if (sfczok.Count() > 0)
                        {
                            //获取新增记录
                            var xzjl = sfczok.First().GetValue(invocation.ReturnValue) as IEnumerable<object>;
                            if (xzjl.Count() > 0)
                            {
                                insert_list = xzjl;
                            }
                        }
                        //替换操作
                        if (del_list.Count() > 0 && insert_list.Count() > 0)
                        {
                            Db.Insert<mes_oper_log>(new mes_oper_log()
                            {
                                name = tbname,
                                czr = userinfo.name,
                                czrid = userinfo.id,
                                lx = czlx.ToString(),
                                czrq = DateTime.Now,
                                path = url,
                                newdata = JsonConvert.SerializeObject(del_list)
                            });
                            Db.Insert<mes_oper_log>(new mes_oper_log()
                            {
                                name = tbname,
                                czr = userinfo.name,
                                czrid = userinfo.id,
                                lx = czlx.ToString(),
                                czrq = DateTime.Now,
                                path = url,
                                newdata = JsonConvert.SerializeObject(insert_list)
                            });
                        }
                        //综合导入
                        else if (update_list.Count() > 0 && insert_list.Count() > 0) {
                            Db.Insert<mes_oper_log>(new mes_oper_log()
                            {
                                name = tbname,
                                czr = userinfo.name,
                                czrid = userinfo.id,
                                lx = czlx.ToString(),
                                czrq = DateTime.Now,
                                path = url,
                                olddata = JsonConvert.SerializeObject(update_list)
                            });
                            Db.Insert<mes_oper_log>(new mes_oper_log()
                            {
                                name = tbname,
                                czr = userinfo.name,
                                czrid = userinfo.id,
                                lx = czlx.ToString(),
                                czrq = DateTime.Now,
                                path = url,
                                newdata = JsonConvert.SerializeObject(insert_list)
                            });


                        }
                        //新增导入
                        else if(insert_list.Count()>0 && update_list.Count() == 0 && del_list.Count() == 0)
                        {
                            Db.Insert<mes_oper_log>(new mes_oper_log()
                            {
                                name = tbname,
                                czr = userinfo.name,
                                czrid = userinfo.id,
                                lx = czlx.ToString(),
                                czrq = DateTime.Now,
                                path = url,
                                newdata = JsonConvert.SerializeObject(insert_list)
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
    }
}

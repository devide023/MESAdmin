using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.LBJ.ImportData;
using ZDMesModels;
using Oracle.ManagedDataAccess.Client;
using DapperExtensions;
using DapperExtensions.Predicate;
using System.Web;
using ZDToolHelper;
using System.Reflection;
using Dapper;

namespace ZDMesServices.LBJ.ImportData
{
    public class ImportDataService<T> : OracleBaseFixture, IImportData<T> where T : class, new()
    {
        public ImportDataService(string constr) : base(constr)
        {

        }
        public virtual sys_import_result<T> NewImportData(List<T> data)
        {
            try
            {
                sys_import_result<T> ret = new sys_import_result<T>();
                List<T> oklist = new List<T>();
                List<T> repeatlist = new List<T>();
                string configpath = HttpContext.Current.Server.MapPath("~/Import_Config.json");
                ConfigHelper confighelper = new ConfigHelper();
                confighelper.SetConfigPath = configpath;
                var configlist = confighelper.Read_Import_LogConfig();
                string tbname = string.Empty;
                tbname = typeof(T).Name;
                var rules = configlist.Where(t => t.tablename == tbname);
                if (rules.Count() > 0)
                {
                    var xzruleslist = rules.FirstOrDefault().xinzeng;
                    Type p = Type.GetType(typeof(T).FullName + ",ZDMesModels");
                    PropertyInfo[] pi = p.GetProperties();
                    StringBuilder sql = new StringBuilder();
                    sql.Append($"select count(*) from {tbname} where 1=1 ");
                    foreach (var item in xzruleslist)
                    {
                        sql.Append($" and {item} =  :{item} ");
                    }
                    using (var db = new OracleConnection(ConString))
                    {
                        try
                        {
                            InitDB(db);
                            foreach (var item in data)
                            {
                                DynamicParameters dyp = new DynamicParameters();
                                foreach (var col in xzruleslist)
                                {
                                    var cz = pi.Where(t => t.Name == col);
                                    if (cz.Count() > 0)
                                    {
                                        var colval = cz.First().GetValue(item);
                                        dyp.Add($":{col}", colval);
                                    }
                                    else
                                    {
                                        dyp.Add($":{col}", null);
                                    }
                                }
                                var q = db.ExecuteScalar<int>(sql.ToString(), dyp);
                                if (q == 0)
                                {
                                    Db.Insert<T>(item);
                                    oklist.Add(item);
                                }
                                else
                                {
                                    repeatlist.Add(item);
                                }
                            }
                            ret.oklist = oklist;
                            ret.repeatlist = repeatlist;
                            return ret;
                        }
                        finally
                        {
                            db.Close();
                        }
                    }
                }
                else
                {
                    return ret;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Db?.Dispose();
            }
        }

        public virtual sys_import_result<T> ReaplaceImportData(List<T> data)
        {
            try
            {
                sys_import_result<T> ret = new sys_import_result<T>();
                List<T> oklist = new List<T>();
                List<T> dellist = new List<T>();
                List<T> uplist = new List<T>();
                string configpath = HttpContext.Current.Server.MapPath("~/Import_Config.json");
                ConfigHelper confighelper = new ConfigHelper();
                confighelper.SetConfigPath = configpath;
                var configlist = confighelper.Read_Import_LogConfig();
                string tbname = string.Empty;
                tbname = typeof(T).Name;
                var replacecnf = configlist.Where(t => t.tablename == tbname).FirstOrDefault();
                var rules = configlist.Where(t => t.tablename == tbname).FirstOrDefault().replace;
                var upcols = configlist.Where(t => t.tablename == tbname).FirstOrDefault().updatecol;
                if (rules.Count > 0)
                {
                    Type p = Type.GetType(typeof(T).FullName + ",ZDMesModels");
                    PropertyInfo[] pi = p.GetProperties();
                    StringBuilder sql = new StringBuilder();
                    sql.Append($"delete from {tbname} where 1=1 ");
                    foreach (var item in rules)
                    {
                        sql.Append($" and {item} =  :{item} ");
                    }
                    StringBuilder delsql = new StringBuilder();
                    delsql.Append($"select * from {tbname} where 1=1 ");
                    foreach (var item in rules)
                    {
                        delsql.Append($" and {item} =  :{item} ");
                    }
                    StringBuilder updatesql = new StringBuilder();
                    updatesql.Append($"update {tbname} set ");
                    foreach (var item in upcols)
                    {
                        updatesql.Append($" {item} =  :{item},");
                    }
                    updatesql.Remove(sql.Length - 1, 1);
                    updatesql.Append(" where 1=1 ");
                    foreach (var item in rules)
                    {
                        sql.Append($" and {item} =  :{item} ");
                    }
                    using (var db = new OracleConnection(ConString))
                    {
                        try
                        {
                            InitDB(db);
                            //先删后加模式
                            if (replacecnf.replacetype == 1)
                            {
                                //删除数据
                                foreach (var item in data)
                                {
                                    DynamicParameters dyp = new DynamicParameters();
                                    foreach (var col in rules)
                                    {
                                        var cz = pi.Where(t => t.Name == col);
                                        if (cz.Count() > 0)
                                        {
                                            var colval = cz.First().GetValue(item);
                                            dyp.Add($":{col}", colval);
                                        }
                                        else
                                        {
                                            dyp.Add($":{col}", null);
                                        }
                                    }
                                    dellist.AddRange(db.Query<T>(delsql.ToString(), dyp));
                                    db.Execute(sql.ToString(), dyp);
                                }
                                Db.Insert<T>(data);
                                ret.oklist = data;
                                ret.dellist = dellist;
                                return ret;
                            }//更新模式
                            else if (replacecnf.replacetype == 2)
                            {
                                //修改数据
                                foreach (var item in data)
                                {
                                    DynamicParameters dyp = new DynamicParameters();
                                    //更新字段
                                    foreach (var col in upcols)
                                    {
                                        var cz = pi.Where(t => t.Name == col);
                                        if (cz.Count() > 0)
                                        {
                                            var colval = cz.First().GetValue(item);
                                            dyp.Add($":{col}", colval);
                                        }
                                        else
                                        {
                                            dyp.Add($":{col}", null);
                                        }
                                    }
                                    //查询条件
                                    foreach (var col in rules)
                                    {
                                        var cz = pi.Where(t => t.Name == col);
                                        if (cz.Count() > 0)
                                        {
                                            var colval = cz.First().GetValue(item);
                                            dyp.Add($":{col}", colval);
                                        }
                                        else
                                        {
                                            dyp.Add($":{col}", null);
                                        }
                                    }
                                    var r = db.Execute(updatesql.ToString(), dyp);
                                    //存在记录
                                    if (r > 0)
                                    {
                                        uplist.Add(item);
                                    }
                                }
                                ret.oklist = data;
                                ret.dellist = uplist;
                            }
                            return ret;
                        }
                        finally
                        {
                            db.Close();
                        }
                    }
                }
                else
                {
                    return ret;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Db?.Dispose();
            }
        }

        public virtual sys_import_result<T> ZhImportData(List<T> data)
        {
            try
            {
                sys_import_result<T> ret = new sys_import_result<T>();
                string configpath = HttpContext.Current.Server.MapPath("~/Import_Config.json");
                ConfigHelper confighelper = new ConfigHelper();
                confighelper.SetConfigPath = configpath;
                var configlist = confighelper.Read_Import_LogConfig();
                string tbname = string.Empty;
                tbname = typeof(T).Name;
                var rules = configlist.Where(t => t.tablename == tbname).FirstOrDefault().updatecol;
                var where = configlist.Where(t => t.tablename == tbname).FirstOrDefault().zhonghe;
                if (where.Count > 0 && rules.Count > 0)
                {
                    Type p = Type.GetType(typeof(T).FullName + ",ZDMesModels");
                    PropertyInfo[] pi = p.GetProperties();
                    StringBuilder sql = new StringBuilder();
                    sql.Append($"update {tbname} set ");
                    foreach (var item in rules)
                    {
                        sql.Append($" {item} =  :{item},");
                    }
                    sql.Remove(sql.Length - 1, 1);
                    sql.Append(" where 1=1 ");
                    foreach (var item in where)
                    {
                        sql.Append($" and {item} =  :{item} ");
                    }
                    StringBuilder updatesql = new StringBuilder();
                    updatesql.Append($"select * from {tbname} where 1=1 ");
                    foreach (var item in where)
                    {
                        updatesql.Append($" and {item} =  :{item} ");
                    }
                    using (var db = new OracleConnection(ConString))
                    {
                        try
                        {
                            InitDB(db);
                            foreach (var item in data)
                            {
                                DynamicParameters dyp = new DynamicParameters();
                                //更新字段
                                foreach (var col in rules)
                                {
                                    var cz = pi.Where(t => t.Name == col);
                                    if (cz.Count() > 0)
                                    {
                                        var colval = cz.First().GetValue(item);
                                        dyp.Add($":{col}", colval);
                                    }
                                    else
                                    {
                                        dyp.Add($":{col}", null);
                                    }
                                }
                                //查询条件
                                foreach (var col in where)
                                {
                                    var cz = pi.Where(t => t.Name == col);
                                    if (cz.Count() > 0)
                                    {
                                        var colval = cz.First().GetValue(item);
                                        dyp.Add($":{col}", colval);
                                    }
                                    else
                                    {
                                        dyp.Add($":{col}", null);
                                    }
                                }
                                var sfcz = db.Query<T>(updatesql.ToString(), dyp);
                                //存在记录
                                if (sfcz.Count() > 0)
                                {
                                    var q = db.Execute(sql.ToString(), dyp);
                                    if (q > 0)
                                    {
                                        ret.orginallist.AddRange(sfcz);
                                    }
                                }//新增记录
                                else
                                {
                                    Db.Insert<T>(item);
                                    ret.oklist.Add(item);
                                }

                            }
                            return ret;
                        }
                        finally
                        {
                            db.Close();
                        }
                    }
                }
                else
                {
                    return ret;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Db?.Dispose();
            }
        }
    }
}

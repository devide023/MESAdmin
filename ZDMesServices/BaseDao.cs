using Autofac.Extras.DynamicProxy;
using Dapper;
using DapperExtensions.Mapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterceptor.LBJ;
using ZDMesInterfaces.Common;
using ZDMesModels;

namespace ZDMesServices
{
    public class BaseDao<T> : OracleBaseFixture, IDbOperate<T> where T : class, new()
    {
        public BaseDao(string constr) : base(constr)
        {

        }
        public virtual int Add(T entity)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        InitDB(db);
                        var ret = Db.Insert<T>(entity);
                        return ret;
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
            finally
            {
                Db.Dispose();
            }
        }

        public virtual int Add(IEnumerable<T> entitys)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        db.Open();
                        InitDB(db);
                        List<dynamic> list = new List<dynamic>();
                        using (var transaction = db.BeginTransaction())
                        {
                            try
                            {
                                foreach (var item in entitys)
                                {
                                    var ret = Db.Insert<T>(item, transaction);
                                    list.Add(ret);
                                }
                                transaction.Commit();
                                return list.Count();
                            }
                            catch (Exception)
                            {
                                transaction.Rollback();
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
            finally
            {
                Db.Dispose();
            }
        }

        public virtual int Add(IEnumerable<T> entitys, out IEnumerable<T> noklist)
        {
            throw new NotImplementedException();
        }

        public virtual bool Del(T entity)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        InitDB(db);
                        return Db.Delete<T>(entity);
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
            finally
            {
                Db.Dispose();
            }
        }

        public virtual bool Del(IEnumerable<T> entitys)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        InitDB(db);
                        List<bool> list = new List<bool>();
                        foreach (var item in entitys)
                        {
                            list.Add(Db.Delete<T>(item));
                        }
                        if (entitys.Count() == list.Count())
                        {
                            return true;
                        }
                        else
                        {
                            return false;
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
            finally
            {
                Db.Dispose();
            }
        }
        public virtual IEnumerable<T> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        InitDB(db);
                        resultcount = 0;
                        StringBuilder sql = new StringBuilder();
                        StringBuilder sql_cnt = new StringBuilder();
                        //读取配置文件
                        if (parm.sqlconfig != null)
                        {
                            sql.Append(parm.sqlconfig.sql);
                            sql_cnt.Append(parm.sqlconfig.sql_cnt);
                            if (parm.sqlexp != null && !string.IsNullOrWhiteSpace(parm.sqlexp))
                            {
                                sql.Append(" and " + parm.sqlexp);
                                sql_cnt.Append(" and " + parm.sqlexp);
                            }
                            //前端排序
                            if (parm.orderbyexp != null && !string.IsNullOrWhiteSpace(parm.orderbyexp))
                            {
                                sql.Append(parm.orderbyexp);
                            }
                            else
                            {
                                if (!string.IsNullOrWhiteSpace(parm.sqlconfig.sql_orderby))
                                {
                                    sql.Append(parm.sqlconfig.sql_orderby);
                                }
                            }
                        }
                        else
                        {
                            var colnames = string.Empty;
                            IClassMapper mapper = Db.GetMap<T>();
                            var tablename = mapper.TableName;
                            var cols = mapper.Properties;
                            foreach (var item in cols)
                            {
                                if (!item.Ignored)
                                {
                                    if (item.ColumnName == item.Name)
                                    {
                                        colnames += item.ColumnName + ",";
                                    }
                                    else
                                    {
                                        colnames += item.ColumnName + $" as {item.Name},";
                                    }
                                }
                            }
                            if (!string.IsNullOrEmpty(colnames))
                            {
                                colnames = colnames.Remove(colnames.Length - 1);
                            }
                            else
                            {
                                colnames = "*";
                            }
                            sql.Append($"select {colnames} from {tablename} where 1=1 ");
                            sql_cnt.Append($"select count(*) from {tablename} where 1=1 ");

                            if (parm.sqlexp != null && !string.IsNullOrWhiteSpace(parm.sqlexp))
                            {
                                sql.Append(" and " + parm.sqlexp);
                                sql_cnt.Append(" and " + parm.sqlexp);
                            }
                            //前端排序
                            if (parm.orderbyexp != null && !string.IsNullOrWhiteSpace(parm.orderbyexp))
                            {
                                sql.Append(parm.orderbyexp);
                            }
                            else
                            {
                                if (parm.default_order_colname != null && !string.IsNullOrEmpty(parm.default_order_colname))
                                {
                                    sql.Append($" order by {parm.default_order_colname} desc ");
                                }
                            }
                        }
                        var q = db.Query<T>(OraPager(sql.ToString()), parm.sqlparam);
                        resultcount = db.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                        return q;
                    }
                    catch(Exception)
                    {
                        throw;
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
            finally
            {
                Db.Dispose();
            }
        }

        public virtual bool Modify(T entity)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        InitDB(db);
                        return Db.Update<T>(entity);
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
            finally
            {
                Db.Dispose();
            }
        }

        public virtual bool Modify(IEnumerable<T> entitys)
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        InitDB(db);
                        List<bool> list = new List<bool>();
                        foreach (var item in entitys)
                        {
                            list.Add(Db.Update<T>(item));
                        }
                        if (entitys.Count() == list.Count())
                        {
                            return true;
                        }
                        else
                        {
                            return false;
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
            finally
            {
                Db.Dispose();
            }
        }
    }
}

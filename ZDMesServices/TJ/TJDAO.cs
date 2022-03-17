﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.Common;
using DapperExtensions;
using DapperExtensions.Mapper;
using ZDMesModels;
using Dapper;

namespace ZDMesServices.TJ
{
    public class TJDAO<T> : OracleBaseFixture, IDbOperate<T> where T : class, new()
    {
        public TJDAO() : base("testmes")
        {

        }

        public virtual int Add(T entity)
        {
            try
            {
                var ret = Db.Insert<T>(entity);
                return ret;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual int Add(IEnumerable<T> entitys)
        {
            try
            {
                return Db.Insert<IEnumerable<T>>(entitys);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual bool Del(T entity)
        {
            try
            {
                return Db.Delete<T>(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual bool Del(IEnumerable<T> entitys)
        {
            try
            {
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
            catch (Exception)
            {
                throw;
            }
        }

        //public T Find(string colname, dynamic value)
        //{
        //    try
        //    {
        //        System.Data.DbType coltype = System.Data.DbType.String;
        //        var colnames = string.Empty;
        //        IClassMapper mapper = Db.GetMap<T>();
        //        var tablename = mapper.TableName;
        //        var cols = mapper.Properties;
        //        foreach (var item in cols)
        //        {
        //            if (!item.Ignored)
        //            {
        //                if (item.ColumnName == item.Name)
        //                {
        //                    colnames += item.ColumnName + ",";
        //                }
        //                else
        //                {
        //                    colnames += item.ColumnName + $" as {item.Name},";
        //                }
        //            }
        //            if(item.ColumnName.ToLower() == colname.ToLower())
        //            {
        //                coltype = (System.Data.DbType)item.DbType;
        //            }
        //        }
        //        if (!string.IsNullOrEmpty(colnames))
        //        {
        //            colnames = colnames.Remove(colnames.Length - 1);
        //        }
        //        else
        //        {
        //            colnames = "*";
        //        }
        //        StringBuilder sql = new StringBuilder();
        //        sql.Append($"select {colnames} from {tablename} where {colname} = :value ");
        //        DynamicParameters p = new DynamicParameters();
        //        p.Add(":value", value, coltype, System.Data.ParameterDirection.Input);
        //        var q = Db.Connection.Query<T>(sql.ToString(), p);
        //        if (q.Count() > 0)
        //        {
        //            return q.FirstOrDefault();
        //        }
        //        else
        //        {
        //            return new T();
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        public virtual IEnumerable<T> GetList(sys_page parm, out int resultcount)
        {
            try
            {
                resultcount = 0;
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
                StringBuilder sql = new StringBuilder();
                sql.Append($"select {colnames} from {tablename} where 1=1 ");
                StringBuilder sql_cnt = new StringBuilder();
                sql_cnt.Append($"select count(*) from {tablename} where 1=1 ");
                if (!string.IsNullOrWhiteSpace(parm.sqlexp))
                {
                    sql.Append(" and " + parm.sqlexp);
                    sql_cnt.Append(" and " + parm.sqlexp);
                }
                if (!string.IsNullOrWhiteSpace(parm.orderbyexp))
                {
                    sql.Append(parm.orderbyexp);
                }
                else
                {
                    if (!string.IsNullOrEmpty(parm.default_order_colname))
                    {
                        sql.Append($" order by {parm.default_order_colname} desc ");
                    }
                }
                var q = Db.Connection.Query<T>(OraPager(sql.ToString()), parm.sqlparam);
                resultcount = Db.Connection.ExecuteScalar<int>(sql_cnt.ToString(), parm.sqlparam);
                return q;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public virtual bool Modify(T entity)
        {
            try
            {
                return Db.Update<T>(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual bool Modify(IEnumerable<T> entitys)
        {
            try
            {
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
            catch (Exception)
            {

                throw;
            }
        }
    }
}

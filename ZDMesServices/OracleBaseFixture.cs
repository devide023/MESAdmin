using DapperExtensions;
using DapperExtensions.Mapper;
using DapperExtensions.Sql;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace ZDMesServices
{
    public class OracleBaseFixture :IDisposable
    {
        private string connstr = string.Empty;
        protected IDatabase Db;
        public string ConString
        {
            get
            {
                return connstr;
            }
        }
        public OracleBaseFixture(string connstr)
        {
            this.connstr = ConfigurationManager.ConnectionStrings[connstr]?.ToString();
        }
        public void Dispose()
        {
            Db?.Dispose();
        }

        public void InitDB(OracleConnection con)
        {
            try
            {
                var config = new DapperExtensionsConfiguration(typeof(AutoClassMapper<>), new List<Assembly>(), new OracleDialect());
                var sqlGenerator = new SqlGeneratorImpl(config);
                Db = new Database(con, sqlGenerator);
            }
            catch (Exception)
            {
                con.Close();
                Db?.Dispose();
                throw;
            }
        }

        protected string OraPager(string sql)
        {
            StringBuilder tsql = new StringBuilder();
            tsql.Append(" SELECT * ");
            tsql.Append(" FROM   (SELECT ROWNUM RN, XX.*");
            tsql.Append("          FROM   ( ");
            tsql.Append(sql);
            tsql.Append("         ) XX");
            tsql.Append("          WHERE  ROWNUM <= :pagesize * :pageindex)");
            tsql.Append(" WHERE  RN > (:pageindex - 1) * :pagesize");
            return tsql.ToString();
        }
    }
}
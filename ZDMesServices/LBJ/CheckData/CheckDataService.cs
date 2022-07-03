using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.LBJ;
using Oracle.ManagedDataAccess.Client;
using Dapper;
namespace ZDMesServices.LBJ.CheckData
{
    public class CheckDataService : OracleBaseFixture, ICheckData
    {
        public CheckDataService(string constr):base(constr)
        {

        }
        public bool Valid<T>(string colname, object data)
        {
            try
            {
                string tbname = typeof(T).Name;
                StringBuilder sql = new StringBuilder();
                sql.Append($"select count(*) from {tbname} where {colname} = :colval ");
                DynamicParameters p = new DynamicParameters();
                p.Add(":colval", data);
                using (var db = new OracleConnection(ConString))
                {
                    var cnt = db.ExecuteScalar<int>(sql.ToString(), p);
                    if (cnt > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
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

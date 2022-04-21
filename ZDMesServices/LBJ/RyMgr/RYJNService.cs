using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.Common;
using ZDMesModels.LBJ;
using ZDMesInterfaces.LBJ.RyMgr;
using Dapper;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace ZDMesServices.LBJ.RyMgr
{
    public class RYJNService:BaseDao<zxjc_ryxx_jn>,IRyJn
    {
        public RYJNService(string constr):base(constr)
        {

        }

        public int MaxJnNo()
        {
            try
            {
                using (IDbConnection db = new OracleConnection(ConString))
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("select count(jnbh) from ZXJC_RYXX_JN");
                    return db.ExecuteScalar<int>(sql.ToString());
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool IsExistJnNo(string jnno)
        {
            try
            {
                using (IDbConnection db = new OracleConnection(ConString))
                {
                    StringBuilder sql = new StringBuilder();
                    sql.Append("select count(jnbh) from ZXJC_RYXX_JN where jnbh = :jnno");
                    var ret = db.ExecuteScalar<int>(sql.ToString(), new { jnno = jnno });
                    return ret > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

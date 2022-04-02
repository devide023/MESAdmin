using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.Common;
using ZDMesModels.LBJ;
using ZDMesInterfaces.LBJ.RyMgr;
using Dapper;
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
                StringBuilder sql = new StringBuilder();
                sql.Append("select count(jnbh) from ZXJC_RYXX_JN");
                return Db.Connection.ExecuteScalar<int>(sql.ToString());
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
                StringBuilder sql = new StringBuilder();
                sql.Append("select count(jnbh) from ZXJC_RYXX_JN where jnbh = :jnno");
                var ret = Db.Connection.ExecuteScalar<int>(sql.ToString(), new { jnno = jnno });
                return ret > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

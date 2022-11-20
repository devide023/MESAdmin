using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.TJ.A1;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using ZDMesInterfaces.TJ;

namespace ZDMesServices.TJ.A1.JTGL
{
    public class A1JsTzService:BaseDao<zxjc_t_jstc>, IJTFPSCX
    {
        public A1JsTzService(string constr):base(constr)
        {

        }

        public bool CanRemove(string jcbh)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select count(jtid) from zxjc_t_jstcfp where jtid = :jtid ");
                using (var db = new OracleConnection(ConString))
                {
                    return db.ExecuteScalar<int>(sql.ToString(), new { jtid = jcbh }) == 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

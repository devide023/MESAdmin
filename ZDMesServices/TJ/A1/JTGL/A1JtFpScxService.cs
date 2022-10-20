using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.TJ.A1;
using Oracle.ManagedDataAccess.Client;
using Dapper;
using ZDMesInterfaces.TJ;

namespace ZDMesServices.TJ.A1.JTGL
{
    public class A1JtFpScxService:BaseDao<zxjc_t_jstc_scx>,IJTFPSCX
    {
        public A1JtFpScxService(string constr):base(constr)
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

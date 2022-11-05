using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.TJ;
using ZDMesModels;
using ZDMesModels.TJ.A1;
namespace ZDMesServices.TJ.A1.RYGL
{
    public class A1RyJnService:BaseDao<zxjc_ryxx_jn>,IRYJN,IBatAtachValue<zxjc_ryxx_jn>
    {
        public A1RyJnService(string constr):base(constr)
        {

        }
        public string CreateJnCode()
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select seq_mes_jnbh.nextval FROM dual");
                using (var db = new OracleConnection(ConString))
                {
                    var no = db.ExecuteScalar<int>(sql.ToString());
                    return "JN" + no.ToString().PadLeft(5, '0');
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<zxjc_ryxx_jn> BatSetValue(List<zxjc_ryxx_jn> list)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select seq_mes_jnbh.nextval FROM dual");
                using (var db = new OracleConnection(ConString))
                {
                    foreach (var item in list)
                    {
                        var no = db.ExecuteScalar<int>(sql.ToString());
                        var jnbh = "JN" + no.ToString().PadLeft(5, '0');
                        item.jnbh = jnbh;
                    }
                    return list;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

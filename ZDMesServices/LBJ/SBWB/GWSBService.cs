using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.LBJ.SBWB;
using Oracle.ManagedDataAccess.Client;
using Dapper;

namespace ZDMesServices.LBJ.SBWB
{
    public class GWSBService: OracleBaseFixture, ISBGW
    {
        public GWSBService(string constr) : base(constr)
        {

        }

        public string GetGWH_By_Sbbh(string sbbh)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select gwh FROM base_sbxx where  sbbh = :sbbh and    rownum < 2");
                using (var db = new OracleConnection(ConString))
                {
                    var q = db.Query<string>(sql.ToString(), new { sbbh = sbbh });
                    if (q.Count() > 0)
                    {
                        return q.First();
                    }
                    else
                    {
                        return string.Empty;
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

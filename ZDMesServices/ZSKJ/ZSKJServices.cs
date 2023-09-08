using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.ZSKJ;

namespace ZDMesServices.ZSKJ
{
    public class ZSKJServices : OracleBaseFixture, IZSKJ
    {
        public ZSKJServices(string constr) : base(constr)
        {
        }

        public IEnumerable<string> Get_CXDH_List()
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select distinct cxdh FROM zl_szb_cs where cxdh is not null order by cxdh asc");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<string>(sql.ToString());
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<string> Get_Jcsx_By_CXDH(string cxdh)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select jcsx FROM zl_szb_cs where cxdh = :cxdh order by jcsx asc");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<string>(sql.ToString(), new {cxdh = cxdh});
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

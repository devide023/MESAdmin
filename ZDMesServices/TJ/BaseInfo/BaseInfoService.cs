using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.TJ;
using ZDMesModels.TJ;

namespace ZDMesServices.TJ.BaseInfo
{
    public class BaseInfoService:OracleBaseFixture,ITJBaseInfo
    {
        public BaseInfoService(string constr) : base(constr)
        {
            
        }

        public IEnumerable<tj_base_scxxx> GetScxXx()
        {
            using (var db = new OracleConnection(ConString))
            {
                InitDB(db);
                try
                {
                    var list = Db.GetList<tj_base_scxxx>();
                    return list;
                }
                finally
                {
                    Db.Dispose();
                }
            }
        }
        public IEnumerable<zxjc_gxzd> GetGWZD()
        {
            using (var db = new OracleConnection(ConString))
            {
                InitDB(db);
                try
                {
                    var list = Db.GetList<zxjc_gxzd>();
                    return list;
                }
                finally
                {
                    Db.Dispose();
                }
            }
        }
    }
}

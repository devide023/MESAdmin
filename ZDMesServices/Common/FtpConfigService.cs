using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.Common;
using ZDMesModels;
using Oracle.ManagedDataAccess.Client;
namespace ZDMesServices.Common
{
    public class FtpConfigService : OracleBaseFixture, IFtpConfig
    {
        public FtpConfigService(string constr):base(constr)
        {

        }
        public IEnumerable<base_ftpfilepath> FtpConfig()
        {
            try
            {
                using (var db = new OracleConnection(ConString))
                {
                    InitDB(db);
                    return Db.GetList<base_ftpfilepath>();
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Db?.Dispose();
            }
        }
    }
}

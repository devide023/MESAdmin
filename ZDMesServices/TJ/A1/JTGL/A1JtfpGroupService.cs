using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.TJ;
using ZDMesModels.TJ.A1;

namespace ZDMesServices.TJ.A1.JTGL
{
    public class A1JtfpGroupService : BaseDao<zxjc_t_jstcfp_group>, IJTFPRY
    {
        public A1JtfpGroupService(string constr) : base(constr)
        {
        }

        public IEnumerable<zxjc_t_jstcfp_group> Get_All_Group()
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select zbid,zbmc,istogwh FROM  zxjc_t_jstcfp_group ");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<zxjc_t_jstcfp_group>(sql.ToString());
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<zxjc_t_jstcfp> Get_User_JSTZFp(IUser user)
        {
            try
            {
                string current_user_code = user.CurrentUser().code;
                StringBuilder sql = new StringBuilder();
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<zxjc_t_jstcfp>(sql.ToString());
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

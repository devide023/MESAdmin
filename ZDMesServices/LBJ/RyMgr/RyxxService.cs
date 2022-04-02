using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ.RyMgr;
using ZDMesModels;
using ZDMesModels.LBJ;
using Dapper;
namespace ZDMesServices.LBJ.RyMgr
{
    public class RyxxService : BaseDao<zxjc_ryxx>, IRyXx
    {
        public RyxxService(string constr) : base(constr)
        {
        }

        public bool IsExistUserCode(string usercode)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select count(user_code) from ZXJC_RYXX where user_code = :usercode");
                var ret = Db.Connection.ExecuteScalar<int>(sql.ToString(), new { usercode = usercode });
                return ret > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int MaxUserCode()
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select count(user_code) from ZXJC_RYXX");
                return Db.Connection.ExecuteScalar<int>(sql.ToString());
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

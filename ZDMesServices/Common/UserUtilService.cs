using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using ZDToolHelper;
using Dapper;
using Oracle.ManagedDataAccess.Client;
namespace ZDMesServices.Common
{
    public class UserUtilService: OracleBaseFixture
    {
        public UserUtilService(string constr):base(constr)
        {

        }
        public mes_user_entity CurrentUser
        {
            get
            {
                var token = TokenHelper.GetToken;
                using (var db = new OracleConnection(ConString))
                {
                    var list = db.Query<mes_user_entity>("select id,code,name FROM mes_user_entity where token = :token", new { token = token });
                    if (list.Count() > 0)
                    {
                        return list.First();
                    }
                    else
                    {
                        return new mes_user_entity();
                    }
                }
            }
        }
    }
}

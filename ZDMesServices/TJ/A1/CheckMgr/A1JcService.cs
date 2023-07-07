using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Text;
using ZDMesInterfaces.TJ;
using ZDMesModels.TJ.A1;

namespace ZDMesServices.TJ.A1.CheckMgr
{
    /// <summary>
    /// 检测服务
    /// </summary>
    public class A1JcService : OracleBaseFixture, IA1JC
    {
        public A1JcService(string constr) : base(constr)
        {
        }

        public IEnumerable<zxjc_jclx> Get_All_JCLX()
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select id, jclx FROM zxjc_jclx where scbz = 'N'");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<zxjc_jclx>(sql.ToString());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

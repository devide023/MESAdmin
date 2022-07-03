using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.LBJ.DZWD;
using ZDMesModels.LBJ;
using Oracle.ManagedDataAccess.Client;
using Dapper;
using DapperExtensions;
using DapperExtensions.Predicate;
using ZDMesModels;
namespace ZDMesServices.LBJ.DZWD
{
    public class AuditService : OracleBaseFixture, IAudit<zxjc_t_jstc>
    {
        public AuditService(string constr) : base(constr)
        {

        }
        public bool AuditBill(List<string> jtids)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("update zxjc_t_jstc set shbz = 'Y',shr=:shr,shsj=sysdate where jtid in :jtid and shbz='N' ");
            try
            {
                var token = ZDToolHelper.TokenHelper.GetToken;
                using (var db = new OracleConnection(ConString))
                {
                    InitDB(db);
                    try
                    {
                        var u = new mes_user_entity();
                       var q = Db.GetList<mes_user_entity>(Predicates.Field<mes_user_entity>(t => t.token, Operator.Eq, token));
                        if (q.Count() > 0)
                        {
                            u = q.First();
                        }
                        var ids = jtids.ToArray();
                        var ret = db.Execute(sql.ToString(), new { jtid = ids, shr = u.name });
                        return ret > 0;
                    }
                    finally
                    {
                        Db.Dispose();
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

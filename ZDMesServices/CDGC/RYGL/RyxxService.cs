using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.CDGC;
using Oracle.ManagedDataAccess.Client;
using Dapper;
namespace ZDMesServices.CDGC.RYGL
{
    public class RyxxService:BaseDao<zxjc_ryxx>
    {
        public RyxxService(string constr) : base(constr)
        {

        }

        public override int Add(IEnumerable<zxjc_ryxx> entitys)
        {
            try
            {
                int ret = 0;
                StringBuilder sql = new StringBuilder();
                sql.Append("insert into zxjc_ryxx ");
                sql.Append(" (gcdm, scx, user_code, user_name, pass_word, rylx, bzxx, hgsg, csrq, rsrq, ryxb, xpmc, scbz)");
                sql.Append(" values");
                sql.Append(" (:gcdm, :scx, :usercode, :username, :password, :rylx, :bzxx, :hgsg, :csrq, :rsrq, :ryxb, :xpmc, :scbz)");
                using (var db = new OracleConnection(ConString))
                {
                    foreach (var item in entitys)
                    {
                        var usercode = db.ExecuteScalar<string>("select lpad(to_char(seq_mes_rybh.nextval), 6, '0') FROM dual");
                        item.usercode = usercode;
                        int cnt = db.Execute(sql.ToString(), item);
                        ret = ret + cnt;
                    }
                    return ret;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.TJ;
using ZDMesModels.Ducar;

namespace ZDMesServices.Ducar.RyMgr
{
    public class DuCarRyxxService : BaseDao<zxjc_ryxx>, IBatAtachValue<zxjc_ryxx>
    {
        public DuCarRyxxService(string constr) : base(constr)
        {
        }

        public List<zxjc_ryxx> BatSetValue(List<zxjc_ryxx> list)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select seq_mes_rybh.nextval FROM dual");
                using (var db = new OracleConnection(ConString))
                {
                    foreach (var item in list)
                    {
                        var no = db.ExecuteScalar<int>(sql.ToString());
                        var ucode = item.scx.PadLeft(2, '0') + no.ToString().PadLeft(4, '0');
                        item.usercode = ucode;
                    }
                }
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

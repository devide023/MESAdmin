using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.TJ;
using ZDMesModels.TJ.A1;
using Oracle.ManagedDataAccess.Client;
using Dapper;
namespace ZDMesServices.TJ.A1.ZLGL
{
    public class A1DjXxService:BaseDao<zxjc_djgw>,IDJGL
    {
        public A1DjXxService(string constr):base(constr)
        {

        }

        public string Create_DjNo()
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select seq_mes_djno.nextval from dual");
                using (var db = new OracleConnection(ConString))
                {
                    var id = db.ExecuteScalar<int>(sql.ToString());
                    return "DJ"+id.ToString().PadLeft(5, '0');
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override int Add(IEnumerable<zxjc_djgw> entitys)
        {
            foreach (var item in entitys)
            {
                item.djno = Create_DjNo();
            }
            return base.Add(entitys);
        }
    }
}

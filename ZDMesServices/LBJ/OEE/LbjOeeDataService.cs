using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.LBJ;

namespace ZDMesServices.LBJ.OEE
{
    public class LbjOeeDataService : BaseDao<base_template_scx_oee>
    {
        public LbjOeeDataService(string constr) : base(constr)
        {
        }

        public override int Add(IEnumerable<base_template_scx_oee> entitys, out IEnumerable<base_template_scx_oee> noklist)
        {
            try
            {
                List<base_template_scx_oee> postdata = new List<base_template_scx_oee>();
                List<base_template_scx_oee> repeatlist = new List<base_template_scx_oee>();
                StringBuilder sql = new StringBuilder();
                sql.Append("select count(*) FROM base_template_scx_oee where scx = :scx  ");
                using (var db = new OracleConnection(ConString))
                {
                    foreach (var item in entitys)
                    {
                        var cnt = db.ExecuteScalar<int>(sql.ToString(), new { scx = item.scx });
                        if (cnt > 0)
                        {
                            repeatlist.Add(item);
                        }
                        else
                        {
                            postdata.Add(item);
                        }
                    }
                    noklist = repeatlist;
                    return base.Add(postdata);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.LBJ;
using static Dapper.SqlMapper;

namespace ZDMesServices.LBJ.OEE
{
    public class LbjScxOeeService : BaseDao<zxjc_scx_oee>
    {
        public LbjScxOeeService(string constr) : base(constr)
        {
        }

        public override int Add(IEnumerable<zxjc_scx_oee> entitys, out IEnumerable<zxjc_scx_oee> noklist)
        {
            try
            {
                List<zxjc_scx_oee> postdata = new List<zxjc_scx_oee>(); 
                List<zxjc_scx_oee> repeatlist = new List<zxjc_scx_oee>();
                StringBuilder sql = new StringBuilder();
                sql.Append("select count(*) FROM zxjc_scx_oee where scx = :scx and rq between trunc(:rq1) and trunc(:rq2) ");
                using (var db = new OracleConnection(ConString))
                {
                    foreach (var item in entitys)
                    {
                        var d1 = Convert.ToDateTime(item.rq.ToString("yyyy-MM-dd"));
                        var d2 = d1.AddDays(1);
                        var cnt = db.ExecuteScalar<int>(sql.ToString(), new { scx = item.scx, rq1 = d1, rq2 = d2 });
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

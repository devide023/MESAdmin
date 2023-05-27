using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.Ducar;
using ZDMesModels.TJ.A1;

namespace ZDMesServices.Ducar.GyMgr
{
    public class DuCarGylxService : BaseDao<zxjc_gylx>
    {
        public DuCarGylxService(string constr) : base(constr)
        {
        }

        public override int Add(IEnumerable<zxjc_gylx> entitys, out IEnumerable<zxjc_gylx> noklist)
        {
            try
            {
                List<zxjc_gylx> postdata = new List<zxjc_gylx>();
                List<zxjc_gylx> repeatdata = new List<zxjc_gylx>();
                StringBuilder sql = new StringBuilder();
                sql.Append("select count(*) FROM  zxjc_gylx where scx = :scx and jx_no = :jxno and gwh = :gwh and nvl(status_no,'_') = :statusno ");
                using (var db = new OracleConnection(ConString))
                {
                    foreach (var item in entitys)
                    {
                        var cnt = db.ExecuteScalar<int>(sql.ToString(), new { scx = item.scx, jxno = item.jxno, gwh = item.gwh, statusno = string.IsNullOrEmpty(item.statusno) ? "_" : item.statusno });
                        if (cnt == 0)
                        {
                            postdata.Add(item);
                        }
                        else
                        {
                            repeatdata.Add(item);
                        }
                    }
                }
                noklist = repeatdata;
                return base.Add(postdata);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

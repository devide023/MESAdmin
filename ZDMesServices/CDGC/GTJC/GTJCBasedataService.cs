using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.CDGC;
using ZDMesModels.CDGC;
using Oracle.ManagedDataAccess.Client;
using Dapper;
namespace ZDMesServices.CDGC.GTJC
{
    public class GTJCBasedataService: BaseDao<zxjc_base_gtjc>, IGtjc
    {
        public GTJCBasedataService(string constr) : base(constr)
        {

        }

        public IEnumerable<zxjc_base_gtjc> Get_CPLX_List()
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select distinct cplx, mh, th FROM ZXJC_BASE_GTJC");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<zxjc_base_gtjc>(sql.ToString()).OrderBy(t => t.cplx);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<zxjc_base_gtjc> Get_Gtjc_By_LX(string lx)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select id, cplx, mh, cpfw, th, kxmc, kjcc, kjcc_sx as kjccsx, kjcc_xx as kjccxx, sdmj, sdmj_sx as sdmjsx, sdmj_xx sdmjxx, lrr, lrsj, kjtype, sdtype, kjzsz, sdzsz,seq FROM ZXJC_BASE_GTJC where  cplx = :cplx ");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<zxjc_base_gtjc>(sql.ToString(), new { cplx = lx });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

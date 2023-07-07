using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Text;
using ZDMesInterfaces.DuCar;
using ZDMesModels.Ducar;

namespace ZDMesServices.Ducar.GyMgr
{
    public class DuCarKhGwService : OracleBaseFixture, IDuCarKhGw
    {
        public DuCarKhGwService(string constr) : base(constr)
        {
        }

        public IEnumerable<base_gwzd> Get_Khgw_List(string scx,string gwh)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select t1.gcdm, t1.scx, t1.gwh, t1.gwmc, t1.gwlx, t1.gwfl, t1.shbz, t1.gzty, t1.bz, t1.lrr, t1.lrsj, t1.shr, t1.shsj, t1.ip, t1.khgw, t1.dlsj, t1.dlbbh, t1.bdfdj, t1.jbfdj, t1.bdjj, t1.jbjj, t1.hjkagv, t1.hjkjj, t1.fxgwh, t1.dqjx, t1.jjfx, t1.ishcjy, t1.smgw, t1.atlasmodel, t1.iszdhg, t1.jzpdgw ");
                sql.Append(" FROM   base_gwzd t1, (select scx, gwh2 ");
                sql.Append("          from zxjc_gwzd_khxx ");
                sql.Append("          where scx = :scx ");
                sql.Append("          and    gwh = :gwh ) t2 ");
                sql.Append(" where  t1.gwh = t2.gwh2 ");
                sql.Append(" and t1.scx = t2.scx ");
                using (var db = new OracleConnection(ConString))
                {
                    return db.Query<base_gwzd>(sql.ToString(), new { scx = scx, gwh = gwh });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

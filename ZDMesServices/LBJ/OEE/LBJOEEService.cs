using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.LBJ.OEE;
using ZDMesModels.Ducar;
using ZDMesModels.LBJ;

namespace ZDMesServices.LBJ.OEE
{
    public class LBJOEEService : OracleBaseFixture, ILBJOEE
    {
        public LBJOEEService(string constr) : base(constr)
        {
        }
        public dynamic Get_ProInfo(string scx, DateTime? rq)
        {
            try
            {
                StringBuilder sqlhgs = new StringBuilder();
                sqlhgs.Append(" SELECT count(distinct(vin_jj)) as cnt ");
                sqlhgs.Append("          from ZXJC_DATA_LIST  ");
                sqlhgs.Append("          WHERE  SCX = :scx  ");
                sqlhgs.Append("          AND    JCSJ between trunc(:rq1) and trunc(:rq2)   ");
                sqlhgs.Append("          AND    GWH = '05'  ");
                sqlhgs.Append("          AND    JCJG = '合格'  ");
                //
                StringBuilder sqlbhg = new StringBuilder();
                sqlbhg.Append(" SELECT count(distinct(vin_jj)) as cnt ");
                sqlbhg.Append("          from ZXJC_DATA_LIST  ");
                sqlbhg.Append("          WHERE  SCX = :scx  ");
                sqlbhg.Append("          AND    JCSJ between trunc(:rq1) and trunc(:rq2)  ");
                sqlbhg.Append("          AND    GWH = '05'  ");
                sqlbhg.Append("          AND    JCJG = '不合格'  ");
                var rq1 = rq?.Date ?? DateTime.Now.Date.AddDays(-1);
                var rq2 = rq1.AddDays(1);
                using (var db = new OracleConnection(ConString))
                {
                    var hgs = db.ExecuteScalar<int>(sqlhgs.ToString(), new { scx = scx, rq1 = rq1, rq2 = rq2 });
                    var bhgs = db.ExecuteScalar<int>(sqlbhg.ToString(), new { scx = scx,rq1=rq1,rq2=rq2 });
                    return new { hgpsl = hgs, bhgpsl = bhgs };
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public zxjc_scx_oee Get_OEEDataByScx(string scx)
        {
            try
            {
                zxjc_scx_oee retobj = new zxjc_scx_oee();
                StringBuilder sql = new StringBuilder();
                sql.Append("select gcdm,scx,jhzxsj, zzwbqh, zzwcf, zzwbzxx, zzwsbby, px, xx, dlsj_jam as dlsjjam, dlsj_wait as dlsjwait, hdsj, hxsj, gzsj, qttjsj, lljp, oee_target as oeetarget ");
                sql.Append(" from base_template_scx_oee where scx = :scx ");
                
                
                using (var db = new OracleConnection(ConString))
                {
                    var oeetemplate = db.Query<base_template_scx_oee>(sql.ToString(), new { scx = scx });
                    var info = Get_ProInfo(scx, null);
                    var hgpsl = info.hgpsl;
                    var bhgpsl = info.bhgpsl;
                    if (oeetemplate.Count() > 0)
                    {
                        var entity = oeetemplate.First();
                        retobj.rq = DateTime.Now.AddDays(-1);
                        retobj.gcdm = entity.gcdm;
                        retobj.scx = entity.scx;
                        retobj.jhzxsj = entity.jhzxsj;
                        retobj.oeetarget = entity.oeetarget;
                        retobj.px = entity.px;
                        retobj.hdsj = entity.hdsj;
                        retobj.lljp = entity.lljp;
                        retobj.dlsjjam = entity.dlsjjam;
                        retobj.dlsjwait = entity.dlsjwait;
                        retobj.gzsj = entity.gzsj;
                        retobj.hxsj = entity.hxsj;
                        retobj.xx = entity.xx;
                        retobj.qttjsj = entity.qttjsj;
                        retobj.hgpsl = hgpsl;
                        retobj.bhgpsl = bhgpsl;
                        retobj.zzwbqh = entity.zzwbqh;
                        retobj.zzwbzxx = entity.zzwbzxx;
                        retobj.zzwsbby = entity.zzwsbby;
                        retobj.zzwcf = entity.zzwcf;
                        retobj.oeetarget = entity.oeetarget;
                    }
                    return retobj;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool IsOEETemplateExist(base_template_scx_oee entity)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select count(*) FROM base_template_scx_oee where scx = :scx ");
                using (var db = new OracleConnection(ConString))
                {
                    return db.ExecuteScalar<int>(sql.ToString(), new { scx = entity.scx }) > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool IsScxOEEExist(zxjc_scx_oee entity)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select count(*) FROM zxjc_scx_oee where scx = :scx and rq between trunc(:rq1) and trunc(:rq2) ");
                using (var db = new OracleConnection(ConString))
                {
                    var d1 = Convert.ToDateTime(entity.rq.ToString("yyyy-MM-dd"));
                    var d2 = d1.AddDays(1);
                    return db.ExecuteScalar<int>(sql.ToString(), new { scx = entity.scx,rq1= d1,rq2=d2 }) > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

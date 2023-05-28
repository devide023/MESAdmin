using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.LBJ.OEE;
using ZDMesModels.LBJ;

namespace ZDMesServices.LBJ.OEE
{
    public class LBJOEEService : OracleBaseFixture, ILBJOEE
    {
        public LBJOEEService(string constr) : base(constr)
        {
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
                        retobj.hgpsl = 0;
                        retobj.bhgpsl = 0;
                        retobj.lljp = 150;
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

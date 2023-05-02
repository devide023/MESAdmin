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
    public class A1DjXxService:BaseDao<zxjc_djgw>,IDJGL, IBatAtachValue<zxjc_djgw>
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

        public override int Add(IEnumerable<zxjc_djgw> entitys, out IEnumerable<zxjc_djgw> noklist)
        {
            try
            {
                List<zxjc_djgw> postdata = new List<zxjc_djgw>();
                List<zxjc_djgw> repeatdata = new List<zxjc_djgw>();
                StringBuilder sql = new StringBuilder();
                sql.Append("select count(*) FROM  zxjc_djgw where scx = :scx and jx_no = :jxno and gwh = :gwh and trim(replace(replace(replace(djxx,' ',''),'\r',''),'\n','')) = :djxx ");
                using (var db = new OracleConnection(ConString))
                {
                    foreach (var item in entitys)
                    {
                        item.djxx = item.djxx.Replace("\r", "").Replace("\n", "").Replace(" ", "").Trim();
                        var cnt = db.ExecuteScalar<int>(sql.ToString(), new { scx = item.scx, jxno = item.jxno, gwh = item.gwh, djxx = item.djxx });
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
                return this.Add(postdata);
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

        public List<zxjc_djgw> BatSetValue(List<zxjc_djgw> list)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select seq_mes_djno.nextval from dual");
                using (var db = new OracleConnection(ConString))
                {
                    foreach (var item in list)
                    {
                        var id = db.ExecuteScalar<int>(sql.ToString());
                        var djno = "DJ" + id.ToString().PadLeft(5, '0');
                        item.djno = djno;
                    }
                    return list; 
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

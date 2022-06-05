using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.LBJ.ImportData;
using ZDMesModels;
using ZDMesModels.LBJ;
using Oracle.ManagedDataAccess.Client;
using Dapper;
using Autofac.Extras.DynamicProxy;
using ZDMesInterceptor.LBJ;

namespace ZDMesServices.LBJ.SBWB
{
    public class WbXxService:BaseDao<base_sbwb>
    {
        public WbXxService(string constr):base(constr)
        {

        }

        //public sys_import_result<base_sbwb> NewImportData(List<base_sbwb> data)
        //{
        //    try
        //    {
        //        sys_import_result<base_sbwb> ret = new sys_import_result<base_sbwb>();
        //        List<base_sbwb> oklist = new List<base_sbwb>();
        //        List<base_sbwb> repeatlist = new List<base_sbwb>();
        //        StringBuilder sql = new StringBuilder();
        //        sql.Append("select * FROM base_sbwb where gcdm = :gcdm and scx = :scx and gwh = :gwh and wbxx = :wbxx");
        //        using (var db = new OracleConnection(ConString))
        //        {
        //            try
        //            {
        //                InitDB(db);
        //                foreach (var item in data)
        //                {
        //                    var q = db.Query<base_sbwb>(sql.ToString(), item);
        //                    if (q.Count() == 0)
        //                    {
        //                        var r = Db.Insert<base_sbwb>(item);
        //                        oklist.Add(item);
        //                    }
        //                    else
        //                    {
        //                        repeatlist.AddRange(q);
        //                    }
        //                }
        //            }
        //            finally
        //            {
        //                db.Close();
        //            }
        //        }
        //        ret.oklist = oklist;
        //        ret.repeatlist = repeatlist;
        //        return ret;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //public sys_import_result<base_sbwb> ReaplaceImportData(List<base_sbwb> data)
        //{
        //    throw new NotImplementedException();
        //}

        //public sys_import_result<base_sbwb> ZhImportData(List<base_sbwb> data)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

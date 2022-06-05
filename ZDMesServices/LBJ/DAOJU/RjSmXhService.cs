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
using ZDMesServices.Common;
using Autofac.Extras.DynamicProxy;
using ZDMesInterceptor.LBJ;
using ZDMesServices.LBJ.ImportData;

namespace ZDMesServices.LBJ.DAOJU
{
    public class RjSmXhService: BaseDao<base_rjsmxh>
    {
        public RjSmXhService(string constr) : base(constr)
        {
            
        }        

        //public sys_import_result<base_rjsmxh> NewImportData(List<base_rjsmxh> data)
        //{
        //    try
        //    {
        //        sys_import_result<base_rjsmxh> ret = new sys_import_result<base_rjsmxh>();
        //        List<base_rjsmxh> oklist = new List<base_rjsmxh>();
        //        List<base_rjsmxh> repeatlist = new List<base_rjsmxh>();
        //        StringBuilder sql = new StringBuilder();
        //        sql.Append("select count(*) from base_rjsmxh where scx = :scx and sbbh = :sbbh and rjlx =:rjlx and cpzt = :cpzt");
        //        using (var db = new OracleConnection(ConString))
        //        {
        //            try
        //            {
        //                InitDB(db);
        //                foreach (var item in data)
        //                {
        //                    int cnt = db.ExecuteScalar<int>(sql.ToString(), new { scx = item.scx, sbbh = item.sbbh, rjlx = item.rjlx, cpzt = item.cpzt });
        //                    if (cnt == 0)
        //                    {
        //                        Db.Insert<base_rjsmxh>(item);
        //                        oklist.Add(item);
        //                    }
        //                    else
        //                    {
        //                        repeatlist.Add(item);
        //                    }
        //                }
        //                ret.oklist = oklist;
        //                ret.repeatlist = repeatlist;
        //            }
        //            finally
        //            {
        //                db.Close();
        //            }

        //        }
        //        return ret;
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        Db.Dispose();
        //    }
        //}

        //public sys_import_result<base_rjsmxh> ReaplaceImportData(List<base_rjsmxh> data)
        //{
        //    throw new NotImplementedException();
        //}

        //public sys_import_result<base_rjsmxh> ZhImportData(List<base_rjsmxh> data)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

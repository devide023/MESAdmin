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
using ZDMesServices.LBJ.ImportData;

namespace ZDMesServices.LBJ.DAOJU
{
    public class DbRjGxService:BaseDao<base_dbrjgx>
    {
        public DbRjGxService(string constr) :base(constr)
        {
        }

        //public sys_import_result<base_dbrjgx> NewImportData(List<base_dbrjgx> data)
        //{
        //    try
        //    {
        //        sys_import_result<base_dbrjgx> ret = new sys_import_result<base_dbrjgx>();
        //        List<base_dbrjgx> oklist = new List<base_dbrjgx>();
        //        List<base_dbrjgx> repeatlist = new List<base_dbrjgx>();
        //        StringBuilder sql = new StringBuilder();
        //        sql.Append("select count(*) ");
        //        sql.Append(" FROM   base_dbrjgx ");
        //        sql.Append(" where  dbh = :dbh ");
        //        sql.Append(" and    djlx = :djlx ");
        //        StringBuilder dbxxsql = new StringBuilder();
        //        dbxxsql.Append("select * FROM base_dbxx where dbh = :dbh");
        //        StringBuilder rjxxsql = new StringBuilder();
        //        rjxxsql.Append("select * FROM base_rjxx where rjlx = :rjlx ");
        //        using (var db = new OracleConnection(ConString))
        //        {
        //            InitDB(db);
        //            try
        //            {
        //                foreach (var item in data)
        //                {
        //                    var qty = db.ExecuteScalar<int>(sql.ToString(), new { dbh = item.dbh, djlx = item.djlx });
        //                    if (qty == 0)
        //                    {
        //                        var dbxxobj = db.Query<base_dbxx>(dbxxsql.ToString(), new { dbh = item.dbh }).FirstOrDefault();
        //                        var rjxxobj = db.Query<base_rjxx>(rjxxsql.ToString(), new { rjlx = item.djlx }).FirstOrDefault();
        //                        item.dblx = dbxxobj?.dblx;
        //                        item.djlx = rjxxobj!=null?rjxxobj.rjlx:string.Empty;
        //                        item.rjid = rjxxobj!=null? rjxxobj.id:0;
        //                        Db.Insert<base_dbrjgx>(item);
        //                        oklist.Add(item);
        //                    }
        //                    else
        //                    {
        //                        repeatlist.Add(item);
        //                    }
        //                }
        //                ret.oklist = oklist;
        //                ret.repeatlist = repeatlist;
        //                return ret;
        //            }
        //            finally
        //            {
        //                db.Close();
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        throw;
        //    }
        //}

        //public sys_import_result<base_dbrjgx> ReaplaceImportData(List<base_dbrjgx> data)
        //{
        //    try
        //    {
        //        sys_import_result<base_dbrjgx> ret = new sys_import_result<base_dbrjgx>();


        //        return ret;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        //public sys_import_result<base_dbrjgx> ZhImportData(List<base_dbrjgx> data)
        //{
        //    try
        //    {
        //        sys_import_result<base_dbrjgx> ret = new sys_import_result<base_dbrjgx>();


        //        return ret;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
    }
}

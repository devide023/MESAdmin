using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.TJ;
using ZDMesModels.Ducar;

namespace ZDMesServices.Ducar.ZlMgr
{
    public class DuCarGzjcxxService : BaseDao<zxjc_fault>,IBatAtachValue<zxjc_fault>
    {
        public DuCarGzjcxxService(string constr) : base(constr)
        {
        }
        public string Create_FaultNo(string faultname)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select fault_no from zxjc_fault where fault_name = :faultname");
                using (var db = new OracleConnection(ConString))
                {
                    var sfcz = db.Query<string>(sql.ToString(), new { faultname = faultname });
                    if (sfcz.Count() == 0)
                    {
                        sql.Clear();
                        sql.Append("select SEQ_MES_FAULTNO.nextval FROM dual");
                        var no = db.ExecuteScalar<int>(sql.ToString());
                        return "GZ" + no.ToString().PadLeft(4, '0');
                    }
                    else
                    {
                        return sfcz.First().ToString();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<zxjc_fault> Create_FaultNo_List(List<zxjc_fault> list)
        {
            try
            {
                var names = list.Select(t => t.faultname).Distinct();
                foreach (var item in names)
                {
                    var defaultno = Create_FaultNo(item);
                    list.Where(t => t.faultname == item).ToList().ForEach(t => t.faultno = defaultno);
                }
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<zxjc_fault> BatSetValue(List<zxjc_fault> list)
        {
            try
            {
                return Create_FaultNo_List(list);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

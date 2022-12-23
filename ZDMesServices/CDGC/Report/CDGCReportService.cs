using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.CDGC;
using ZDMesModels.CDGC;
using Dapper;
using Oracle.ManagedDataAccess.Client;
using System.Data;
namespace ZDMesServices.CDGC.Report
{
    public class CDGCReportService : OracleBaseFixture, IReport
    {
        public CDGCReportService(string constr) : base(constr)
        {

        }

        public IEnumerable<report_gtjjb> Get_GTJJB_Report(form_gtjjb form, out int resultcount)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select ta.rq, ta.bc, tb.cpmc,tb.trjgs,tb.hgsl,tb.gfsl,tb.lfsl from zxjc_gtjjb_bill ta, ZXJC_GTJJB_BILL_DETAIL tb where  ta.id = tb.billid");
                DynamicParameters p = new DynamicParameters();
                using (var db = new OracleConnection(ConString))
                {

                    if (form.rq.Count == 2)
                    {
                        sql.Append(" and ta.rq between to_date(:rq1,'yyyy-mm-dd') and to_date(:rq2,'yyyy-mm-dd') ");
                        p.Add(":rq1", form.rq[0], DbType.String, ParameterDirection.Input);
                        p.Add(":rq2", form.rq[1], DbType.String, ParameterDirection.Input);
                    }
                    if (form.bc.Count > 0)
                    {
                        sql.Append(" and ta.bc in :bc ");
                        p.Add(":bc", form.bc.ToArray());
                    }
                    if (form.cpmc.Count > 0)
                    {
                        sql.Append(" and tb.cpmc in :cpmc ");
                        p.Add(":cpmc", form.cpmc.ToArray());
                    }
                    var list = db.Query<report_gtjjb>(sql.ToString(), p);
                    var resultlist = list.GroupBy(t => new { rq = t.rq, bc = t.bc, cpmc = t.cpmc }).Select(t => new report_gtjjb()
                    {
                        rq = t.Key.rq,
                        bc = t.Key.bc,
                        cpmc = t.Key.cpmc,
                        trjgs = t.Sum(a => a.trjgs),
                        gfsl = t.Sum(a => a.gfsl),
                        lfsl = t.Sum(a => a.lfsl),
                        hgsl = t.Sum(a => a.hgsl)
                    });
                    resultcount = resultlist.Count();
                    return resultlist.OrderBy(t => t.rq).ThenBy(t => t.bc);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

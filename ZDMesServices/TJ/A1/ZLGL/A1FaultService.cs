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
    public class A1FaultService : BaseDao<zxjc_fault>, IFault
    {
        public A1FaultService(string constr) : base(constr)
        {

        }
        public override int Add(IEnumerable<zxjc_fault> entitys)
        {
            var list = Create_FaultNo_List(entitys.ToList());
            return base.Add(list);
        }
        public override bool Modify(IEnumerable<zxjc_fault> entitys)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("update zxjc_fault set gwh=:gwh,fault_no =:faultno,fault_name =:faultname,fault_fl= :faultfl,jx_no = :jxno,status_no =:statusno,gwh_bz = :gwhbz,bz =:bz  WHERE rowid = :rid");
                using (var db = new OracleConnection(ConString))
                {
                    try
                    {
                        db.Open();
                        using (var trans = db.BeginTransaction())
                        {
                            try
                            {
                                foreach (var item in entitys)
                                {
                                    item.faultno = Create_FaultNo(item.faultname);
                                    db.Execute(sql.ToString(), item, trans);
                                }   
                                trans.Commit();
                                return true;
                            }
                            catch (Exception)
                            {
                                trans.Rollback();
                                throw;
                            }
                        }
                    }
                    finally
                    {
                        db.Close();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public override bool Del(IEnumerable<zxjc_fault> entitys)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("delete from zxjc_fault where rowid in :rid ");
                var rids = entitys.Select(t => t.rid).ToList();
                using (var db = new OracleConnection(ConString))
                {
                    return db.Execute(sql.ToString(), new { rid = rids }) > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
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
    }
}

using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.CDGC;
using ZDMesModels.CDGC;

namespace ZDMesServices.CDGC.BHDGL
{
    public class BHDCFService: BaseDao<lbj_qms_4mbhd>, I4MBHD
    {
        public BHDCFService(string constr) : base(constr)
        {

        }

        public bool BHBHD(IEnumerable<lbj_qms_4mbhd> list)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("update lbj_qms_4mbhd set rwzt=1 where rwzt=0 and id in :id ");
                var ids = list.Select(t => t.id).Distinct().ToList();
                using (var db = new OracleConnection(ConString))
                {
                    return db.Execute(sql.ToString(), new { id = ids }) > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override bool Del(IEnumerable<lbj_qms_4mbhd> entitys)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("delete from lbj_qms_4mbhd where rwzt=0 and id in :id ");
                var ids = entitys.Select(t => t.id).Distinct().ToList();
                using (var db = new OracleConnection(ConString))
                {
                    return db.Execute(sql.ToString(), new { id = ids }) > 0;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public override bool Modify(IEnumerable<lbj_qms_4mbhd> entitys)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select rwzt from lbj_qms_4mbhd where id = :id");
                using (var db = new OracleConnection(ConString))
                {
                    InitDB(db);
                    foreach (var item in entitys)
                    {
                        var q = db.Query<int>(sql.ToString(), new { id = item.id });
                        if(q.Count()>0)
                        {
                            if (q.First() == 0)
                            {
                                Db.Update<lbj_qms_4mbhd>(item);
                            }
                        }
                    }
                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Db?.Dispose();
            }
        }
    }
}

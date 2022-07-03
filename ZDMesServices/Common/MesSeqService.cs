using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.LBJ;

namespace ZDMesServices.Common
{
    public class MesSeqService : OracleBaseFixture, IDbSeq
    {
        public MesSeqService(string constr):base(constr)
        {

        }
        public long Get_Seq_Number(string seqname)
        {
            using (var db = new OracleConnection(ConString))
            {
                StringBuilder sql = new StringBuilder();
                return db.ExecuteScalar<long>($"select {seqname}.nextval FROM dual");
            }
        }
    }
}

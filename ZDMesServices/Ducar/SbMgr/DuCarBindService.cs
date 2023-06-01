using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.DuCar;
using ZDMesModels;
using ZDMesModels.Ducar;

namespace ZDMesServices.Ducar.SbMgr
{
    public class DuCarBindService : OracleBaseFixture, IDuCarJjGxb
    {
        public DuCarBindService(string constr) : base(constr)
        {
        }

        public sys_result BindJjGxb(sys_bind_parm parm)
        {
            try
            {
                string rtn = string.Empty;
                string msg = string.Empty;
                DynamicParameters p = new DynamicParameters();
                p.Add("arg_jjh", parm.jjh, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                p.Add("arg_fdj", parm.jjh, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                p.Add("arg_retn", rtn, System.Data.DbType.String, System.Data.ParameterDirection.Output);
                p.Add("arg_mess", msg, System.Data.DbType.String, System.Data.ParameterDirection.Output);
                using (var db = new OracleConnection(ConString))
                {
                    int retn = db.Execute("P_BindJJHFDJ_DJ",p,commandType: System.Data.CommandType.StoredProcedure);
                    rtn = p.Get<string>("arg_retn");
                    msg = p.Get<string>("arg_mess");
                    if (rtn != "1")
                    {
                        return new sys_result() { code = 0, msg = msg };
                    }
                    else
                    {
                        return new sys_result() { code = 1, msg = msg };

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public sys_result UnbindJjGxb(sys_bind_parm parm)
        {
            try
            {
                string rtn = string.Empty;
                string msg = string.Empty;
                DynamicParameters p = new DynamicParameters();
                p.Add("arg_jjh", parm.jjh, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                p.Add("arg_fdj", parm.jjh, System.Data.DbType.String, System.Data.ParameterDirection.Input);
                p.Add("arg_retn", rtn, System.Data.DbType.String, System.Data.ParameterDirection.Output);
                p.Add("arg_mess", msg, System.Data.DbType.String, System.Data.ParameterDirection.Output);
                using (var db = new OracleConnection(ConString))
                {
                    int retn = db.Execute("P_UnBindJJHFDJ", p, commandType: System.Data.CommandType.StoredProcedure);
                    rtn = p.Get<string>("arg_retn");
                    msg = p.Get<string>("arg_mess");
                    if (rtn !="1")
                    {
                        return new sys_result() { code = 0, msg = msg };
                    }
                    else
                    {
                        return new sys_result() { code = 1, msg = msg };

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.DuCar;
using ZDMesInterfaces.TJ;
using ZDMesModels.Ducar;
using ZDMesServices.Common;

namespace ZDMesServices.Ducar.ZlMgr
{
    public class DuCarDjxxService : BaseDao<zxjc_djgw>,IDuCarDjgw, IBatAtachValue<zxjc_djgw>
    {
        private UserUtilService _uservice;
        public DuCarDjxxService(string constr) : base(constr)
        {
            _uservice= new UserUtilService(constr);
        }

        public List<zxjc_djgw> BatSetValue(List<zxjc_djgw> list)
        {
            try
            {
                list.ForEach(t =>
                {
                    t.djno = Create_DJNo();
                    t.lrr = _uservice.CurrentUser.name;
                });
                return list;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string Create_DJNo()
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select seq_mes_djno.nextval FROM dual");
                using (var db = new OracleConnection(ConString))
                {
                    var no = db.ExecuteScalar<int>(sql.ToString());
                    return "DJ" + no.ToString().PadLeft(5, '0');
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

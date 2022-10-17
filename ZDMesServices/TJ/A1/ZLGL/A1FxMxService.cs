using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using Dapper;
using ZDMesModels.TJ.A1;
namespace ZDMesServices.TJ.A1.ZLGL
{
    public class A1FxMxService:BaseDao<zxjc_gwzd_fxmx>
    {
        public A1FxMxService(string constr):base(constr)
        {

        }
    }
}

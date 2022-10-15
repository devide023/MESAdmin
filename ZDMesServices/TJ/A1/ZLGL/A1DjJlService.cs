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
    public class A1DjJlService:BaseDao<zxjc_djxx>
    {
        public A1DjJlService(string constr):base(constr)
        {

        }
    }
}

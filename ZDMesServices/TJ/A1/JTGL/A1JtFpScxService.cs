using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.TJ.A1;
using Oracle.ManagedDataAccess.Client;
using Dapper;
using ZDMesInterfaces.TJ;
using ZDMesModels;

namespace ZDMesServices.TJ.A1.JTGL
{
    public class A1JtFpScxService:BaseDao<zxjc_t_jstc_scx>
    {
        public A1JtFpScxService(string constr):base(constr)
        {

        }
        
    }
}

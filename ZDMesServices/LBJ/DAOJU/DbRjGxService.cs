using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.LBJ;
namespace ZDMesServices.LBJ.DAOJU
{
    public class DbRjGxService:BaseDao<base_dbrjgx>
    {
        public DbRjGxService(string constr) :base(constr)
        {

        }
    }
}

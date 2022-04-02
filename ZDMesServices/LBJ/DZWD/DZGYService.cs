using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.Common;
using ZDMesModels.LBJ;
namespace ZDMesServices.LBJ.DZWD
{
    public class DZGYService:BaseDao<zxjc_t_dzgy>
    {
        public DZGYService(string constr):base(constr)
        {

        }
    }
}

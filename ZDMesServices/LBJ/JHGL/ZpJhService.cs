using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using ZDMesModels.LBJ;
namespace ZDMesServices.LBJ.JHGL
{
    public class ZpJhService :BaseDao<pp_zpjh>
    {
        public ZpJhService(string constr):base(constr)
        {

        }
    }
}

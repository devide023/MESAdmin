using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.CDGC;

namespace ZDMesServices.CDGC.JCXX
{
    public class JCXXService:BaseDao<base_sbxx>
    {
        public JCXXService(string constr) : base(constr)
        {

        }
    }
}

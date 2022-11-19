using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.CDGC;
namespace ZDMesServices.CDGC.SCJH
{
    public class CdGcScjhService:BaseDao<zxjc_scjh>
    {
        public CdGcScjhService(string constr) :base(constr)
        {

        }
    }
}

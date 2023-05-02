using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.Ducar;

namespace ZDMesServices.Ducar.ZlMgr
{
    public class DuCarDjjlService : BaseDao<zxjc_djxx>
    {
        public DuCarDjjlService(string constr) : base(constr)
        {
        }
    }
}

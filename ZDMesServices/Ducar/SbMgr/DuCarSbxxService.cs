using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.Ducar;

namespace ZDMesServices.Ducar.SbMgr
{
    public class DuCarSbxxService : BaseDao<base_sbxx>
    {
        public DuCarSbxxService(string constr) : base(constr)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.Ducar;

namespace ZDMesServices.Ducar.GyMgr
{
    public class DuCarGycsService : BaseDao<base_gycs>
    {
        public DuCarGycsService(string constr) : base(constr)
        {
        }
    }
}

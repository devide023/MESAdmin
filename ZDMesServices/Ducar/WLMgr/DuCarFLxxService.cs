using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.Ducar;

namespace ZDMesServices.Ducar.WLMgr
{
    public class DuCarFLxxService : BaseDao<flxxb>
    {
        public DuCarFLxxService(string constr) : base(constr)
        {
        }
    }
}

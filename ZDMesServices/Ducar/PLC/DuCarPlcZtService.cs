using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.Ducar;

namespace ZDMesServices.Ducar.PLC
{
    public class DuCarPlcZtService : BaseDao<pcs_plcdata_state>
    {
        public DuCarPlcZtService(string constr) : base(constr)
        {
        }
    }
}

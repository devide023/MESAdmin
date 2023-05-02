using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.Ducar;

namespace ZDMesServices.Ducar.PLC
{
    public class DuCarIOztService : BaseDao<pcs_stationinout_state>
    {
        public DuCarIOztService(string constr) : base(constr)
        {
        }
    }
}

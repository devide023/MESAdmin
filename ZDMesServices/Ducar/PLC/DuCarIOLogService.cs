using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.Ducar;

namespace ZDMesServices.Ducar.PLC
{
    public class DuCarIOLogService : BaseDao<pcs_stationinout_log>
    {
        public DuCarIOLogService(string constr) : base(constr)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.Ducar;

namespace ZDMesServices.Ducar.PLC
{
    public class DuCarPlcLogService : BaseDao<pcs_plcdata_log>
    {
        public DuCarPlcLogService(string constr) : base(constr)
        {
        }
    }
}

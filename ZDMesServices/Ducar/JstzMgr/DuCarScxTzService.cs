using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.Ducar;

namespace ZDMesServices.Ducar.JstzMgr
{
    public class DuCarScxTzService : BaseDao<zxjc_scx_tz>
    {
        public DuCarScxTzService(string constr) : base(constr)
        {
        }
    }
}

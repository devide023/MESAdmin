using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.TJ.A1;

namespace ZDMesServices.TJ.A1.CheckMgr
{
    public class A1JclxService : BaseDao<zxjc_jclx>
    {
        public A1JclxService(string constr) : base(constr)
        {
        }
    }
}

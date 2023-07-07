using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.TJ.A1;

namespace ZDMesServices.TJ.A1.CheckMgr
{
    public class A1JcsbxxService : BaseDao<zxjc_jcsbxx>
    {
        public A1JcsbxxService(string constr) : base(constr)
        {
        }
    }
}

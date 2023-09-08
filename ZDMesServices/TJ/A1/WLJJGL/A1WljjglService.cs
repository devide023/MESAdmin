using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using ZDMesModels.TJ.A1;

namespace ZDMesServices.TJ.A1.WLJJGL
{
    public class A1WljjglService : BaseDao<base_wljjgxb>
    {
        public A1WljjglService(string constr) : base(constr)
        {
        }
    }
}

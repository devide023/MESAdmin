using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using ZDMesModels.Ducar;

namespace ZDMesServices.Ducar.Test
{
    public class TestDucarService : BaseDao<base_fxwl>
    {
        public TestDucarService(string constr) : base(constr)
        {
        }

        public override IEnumerable<base_fxwl> GetList(sys_page parm, out int resultcount)
        {
            return base.GetList(parm, out resultcount);
        }
    }
}

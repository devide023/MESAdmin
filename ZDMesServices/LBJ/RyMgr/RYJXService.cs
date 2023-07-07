using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.LBJ.RyMgr;
using ZDMesModels;
using ZDMesModels.LBJ;

namespace ZDMesServices.LBJ.RyMgr
{
    public class RYJXService : OracleBaseFixture, IRyJx
    {
        public RYJXService(string constr):base(constr)
        {

        }

        public IEnumerable<sys_ryjx> Get_RyJxList(sys_page parm, out int resultcount)
        {
            try
            {
                resultcount = 20;
                return new List<sys_ryjx>();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}

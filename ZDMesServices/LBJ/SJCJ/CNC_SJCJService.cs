using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.LBJ;
namespace ZDMesServices.LBJ.SJCJ
{
    public class CNC_SJCJService : BaseDao<zxjc_sbxx_ls_cnc>
    {
        public CNC_SJCJService(string constr) :base(constr)
        {

        }
    }
}

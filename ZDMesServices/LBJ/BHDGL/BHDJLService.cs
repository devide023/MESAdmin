using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.LBJ;
namespace ZDMesServices.LBJ.BHDGL
{
    public class BHDJLService:BaseDao<lbj_qms_4mbhd>
    {
        public BHDJLService(string constr):base(constr)
        {

        }
    }
}

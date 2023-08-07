using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.LBJ;

namespace ZDMesServices.LBJ.BatGL
{
    public class LbjBatVinService : BaseDao<barcode_print>
    {
        public LbjBatVinService(string constr) : base(constr)
        {
        }
    }
}

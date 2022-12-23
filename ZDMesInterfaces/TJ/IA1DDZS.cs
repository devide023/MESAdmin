using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using ZDMesModels.TJ.A1;

namespace ZDMesInterfaces.TJ
{
    public interface IA1DDZS
    {
        IEnumerable<barcode_print> Get_VinList(sys_page parm, out int resultcount);
    }
}

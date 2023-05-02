using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using ZDMesModels.LBJ;

namespace ZDMesInterfaces.LBJ.SJBL
{
    public interface IZxjcData
    {
        IEnumerable<zxjc_data_list> Get_Lbj_Zxjc_Data_List(sys_page parm);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;

namespace ZDMesInterfaces.LBJ
{
    public interface IFormCheck
    {
        bool Check_Form_Data(List<object> entitys, out sys_result msg);
    }
}

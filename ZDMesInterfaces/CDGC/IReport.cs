using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.CDGC;

namespace ZDMesInterfaces.CDGC
{
    public interface IReport
    {
        IEnumerable<report_gtjjb> Get_GTJJB_Report(form_gtjjb form,out int resultcount);
    }
}

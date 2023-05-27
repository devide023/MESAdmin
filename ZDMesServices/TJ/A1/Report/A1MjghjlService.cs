using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.TJ.A1;

namespace ZDMesServices.TJ.A1.Report
{
    /// <summary>
    /// 毛巾更换记录
    /// </summary>
    public class A1MjghjlService : BaseDao<zxjc_mjghjl>
    {
        public A1MjghjlService(string constr) : base(constr)
        {
        }
    }
}

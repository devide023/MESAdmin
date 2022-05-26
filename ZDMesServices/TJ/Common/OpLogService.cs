using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.Common;
using ZDMesServices.Common;

namespace ZDMesServices.TJ.Common
{
    public class OpLogService: OperateLogService
    {
        private string _constr = string.Empty;
        public OpLogService(string constr, IUser user) : base(constr, user)
        {

        }
    }
}

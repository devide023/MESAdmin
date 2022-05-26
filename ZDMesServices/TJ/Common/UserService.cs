using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesServices.Common;

namespace ZDMesServices.TJ.Common
{
    public class UserService: MesUserService
    {
        public UserService(string constr) : base(constr)
        {

        }
    }
}

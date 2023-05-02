using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.TJ;
using ZDMesModels.Ducar;
using ZDMesServices.Common;

namespace ZDMesServices.Ducar.GyMgr
{
    public class DuCarDzgyService : BaseDao<zxjc_t_dzgy>,IBatAtachValue<zxjc_t_dzgy>
    {
        private UserUtilService _userservice;
        public DuCarDzgyService(string constr) : base(constr)
        {
            _userservice = new UserUtilService(constr);
        }

        public List<zxjc_t_dzgy> BatSetValue(List<zxjc_t_dzgy> list)
        {
            try
            {
                list.ForEach(t => t.scry = _userservice.CurrentUser.name);
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

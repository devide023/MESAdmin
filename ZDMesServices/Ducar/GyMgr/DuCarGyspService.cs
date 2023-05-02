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
    public class DuCarGyspService : BaseDao<zxjc_t_dzgy_sp>, IBatAtachValue<zxjc_t_dzgy_sp>
    {
        private UserUtilService _uservice;
        public DuCarGyspService(string constr) : base(constr)
        {
            _uservice = new UserUtilService(constr);
        }

        public List<zxjc_t_dzgy_sp> BatSetValue(List<zxjc_t_dzgy_sp> list)
        {
            try
            {
                list.ForEach(t => t.scry = _uservice.CurrentUser.name);
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

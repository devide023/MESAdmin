using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.TJ;
using ZDMesModels.LBJ;
using ZDMesServices.Common;

namespace ZDMesServices.LBJ.ZLGL
{
    public class LBJ_BaseCheckService : BaseDao<zxjc_base_check>,IBatAtachValue<zxjc_base_check>
    {
        private UserUtilService u;
        public LBJ_BaseCheckService(string constr) : base(constr)
        {
            u = new UserUtilService(constr);
        }

        public List<zxjc_base_check> BatSetValue(List<zxjc_base_check> list)
        {
            try
            {
                int i = 1;
                list.ForEach(t =>
                {
                    if (t.seq == 0)
                    {
                        t.seq = i++;
                    }
                    t.lrr = u.CurrentUser.name;
                });
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

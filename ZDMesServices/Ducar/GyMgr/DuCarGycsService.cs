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
    public class DuCarGycsService : BaseDao<base_gycs>,IBatAtachValue<base_gycs>
    {
        private UserUtilService _uservice;
        public DuCarGycsService(string constr) : base(constr)
        {
            _uservice = new UserUtilService(constr);
        }

        public List<base_gycs> BatSetValue(List<base_gycs> list)
        {
            try
            {
                list.ForEach(t =>
                {
                    t.lrr = _uservice.CurrentUser.name;
                    t.shr= _uservice.CurrentUser.name;
                    t.shsj = DateTime.Now;
                    t.shbz = "Y";
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

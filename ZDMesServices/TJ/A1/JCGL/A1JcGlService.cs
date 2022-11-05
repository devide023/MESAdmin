using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using ZDMesModels.TJ.A1;
using Oracle.ManagedDataAccess.Client;
using Dapper;
using ZDMesInterfaces.TJ;

namespace ZDMesServices.TJ.A1.JCGL
{
    public class A1JcGlService:BaseDao<zxjc_jcgl>, IBatAtachValue<zxjc_jcgl>
    {
        public A1JcGlService(string constr):base(constr)
        {

        }

        public List<zxjc_jcgl> BatSetValue(List<zxjc_jcgl> list)
        {
            try
            {
                foreach (var item in list)
                {
                    if (item.lx == "奖励")
                    {
                        item.jcje = Math.Abs(item.jcje);
                    }
                    else if (item.lx == "惩罚")
                    {
                        item.jcje = -1 * Math.Abs(item.jcje);
                    }
                }
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

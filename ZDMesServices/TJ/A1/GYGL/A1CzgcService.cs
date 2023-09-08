﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.TJ;
using ZDMesModels.TJ.A1.CZGC;

namespace ZDMesServices.TJ.A1.GYGL
{
    public class A1CzgcService : BaseDao<zxjc_t_dzgy>, IBatAtachValue<zxjc_t_dzgy>
    {
        public A1CzgcService(string constr) : base(constr)
        {
        }

        public List<zxjc_t_dzgy> BatSetValue(List<zxjc_t_dzgy> list)
        {
            try
            {
                foreach (var item in list)
                {
                    item.scry = item.lrr;
                    item.scsj = DateTime.Now;
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

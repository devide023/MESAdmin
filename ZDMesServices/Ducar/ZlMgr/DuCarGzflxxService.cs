using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.Ducar;

namespace ZDMesServices.Ducar.ZlMgr
{
    public class DuCarGzflxxService : BaseDao<zxjc_fault_flxx>
    {
        public DuCarGzflxxService(string constr) : base(constr)
        {
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.TJ.A1;

namespace ZDMesServices.TJ.A1.CheckMgr
{
    /// <summary>
    /// 检测结果
    /// </summary>
    public class A1JcjgService : BaseDao<zxjc_jcjg>
    {
        public A1JcjgService(string constr) : base(constr)
        {
        }
    }
}

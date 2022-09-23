using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.CDGC;

namespace ZDMesServices.CDGC.YCGL
{
    /// <summary>
    /// 异常管理流水
    /// </summary>
    public class YCGLLSService:BaseDao<ad_bjxxls>
    {
        public YCGLLSService(string constr) : base(constr)
        {

        }
    }
}

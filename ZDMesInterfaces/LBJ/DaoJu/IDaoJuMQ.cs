using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesInterfaces.LBJ.DaoJu
{
    public interface IDaoJuMQ
    {
        /// <summary>
        /// 刀具刃磨,推送恢复信息到MQ
        /// </summary>
        /// <param name="zxids"></param>
        /// <returns></returns>
        bool DjRmMq(List<int> zxids);
    }
}

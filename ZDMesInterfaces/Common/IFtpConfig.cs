using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;

namespace ZDMesInterfaces.Common
{
    public interface IFtpConfig
    {
        /// <summary>
        /// ftp配置信息
        /// </summary>
        /// <returns></returns>
        IEnumerable<base_ftpfilepath> FtpConfig();
    }
}

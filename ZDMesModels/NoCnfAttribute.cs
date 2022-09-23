using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels
{
    /// <summary>
    /// 不生成配置文件字段属性
    /// </summary>
    public class NoCnfAttribute:Attribute
    {
        public NoCnfAttribute()
        {

        }
    }
}

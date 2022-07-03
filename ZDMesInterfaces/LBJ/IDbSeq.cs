using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesInterfaces.LBJ
{
    /// <summary>
    /// 数据库序列
    /// </summary>
    public interface IDbSeq
    {
        /// <summary>
        /// 获取序列值
        /// </summary>
        /// <returns></returns>
        long Get_Seq_Number(string seqname);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ZDMesInterfaces.Common
{
    public interface IDbOperate
    {
        int Add<T>(T entity);
        int Add<T>(List<T> entitys);

        int Del<T>(T entity);
        int Del<T>(List<T> entitys);

        int Modify<T>(T entity);
        int Modify<T>(List<T> entitys);

    }
}

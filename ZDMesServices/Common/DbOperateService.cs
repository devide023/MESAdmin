using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.DB;
namespace ZDMesServices.Common
{
    public class DbOperateService : IDbOperate,IDbConn
    {
        public string DbConnStr { get; set; }

        public int Add<T>(T entity)
        {
            throw new NotImplementedException();
        }

        public int Add<T>(List<T> entitys)
        {
            throw new NotImplementedException();
        }

        public int Del<T>(T entity)
        {
            throw new NotImplementedException();
        }

        public int Del<T>(List<T> entitys)
        {
            throw new NotImplementedException();
        }

        public void InitDbConn()
        {
            throw new NotImplementedException();
        }

        public void InitDbConn(string ConnStr)
        {
            throw new NotImplementedException();
        }

        public int Modify<T>(T entity)
        {
            throw new NotImplementedException();
        }

        public int Modify<T>(List<T> entitys)
        {
            throw new NotImplementedException();
        }
    }
}

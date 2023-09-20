using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesInterfaces.LBJ.DaoJu;

namespace ZDMesServices.LBJ.DAOJU
{
    public class DaoJuMqService :OracleBaseFixture, IDaoJuMQ
    {
        private ZDToolHelper.RabbitMQCreater _creater;
        private string _change_name = string.Empty;
        public DaoJuMqService(string constr) : base(constr)
        {
            _creater = new ZDToolHelper.RabbitMQCreater();
            _change_name = "";
        }

        public bool DjRmMq(List<int> zxids)
        {
            try
            {
                string error = string.Empty;
                List<string> jts = new List<string>();
                List<bool> retlist = new List<bool>();
                StringBuilder sql = new StringBuilder();
                sql.Append("select distinct sbbh FROM BASE_DBRJZX where rjdqsm > rjbzsm and id in :ids ");
                StringBuilder sql1= new StringBuilder();
                sql1.Append("select count(id) FROM BASE_DBRJZX where rjdqsm > rjbzsm and sbbh = :sbbh ");
                using (var db = new OracleConnection(ConString))
                {
                    jts = db.Query<string>(sql.ToString(), new { ids = zxids }).ToList();
                    foreach (var item in jts)
                    {
                        var cnt = db.ExecuteScalar<int>(sql1.ToString(), new { sbbh = item });
                        //当前机台无超限值,则推送该机台信息到mq恢复停机
                        if(cnt == 0)
                        {
                            var ret = _creater.Send(_change_name, "", out error);
                            retlist.Add(ret);
                        }
                    }
                }
                return retlist.Count > 0;   
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

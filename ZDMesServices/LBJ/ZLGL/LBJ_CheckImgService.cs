using Dapper;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Management;
using ZDMesInterfaces.LBJ.ZLGL;
using ZDMesInterfaces.TJ;
using ZDMesModels.LBJ;
using ZDMesServices.Common;

namespace ZDMesServices.LBJ.ZLGL
{
    public class LBJ_CheckImgService : BaseDao<zxjc_check_image>,ILBJCheckImage, IBatAtachValue<zxjc_check_image>
    {
        private UserUtilService _u;
        public LBJ_CheckImgService(string constr) : base(constr)
        {
            _u = new UserUtilService(constr);
        }

        public List<zxjc_check_image> BatSetValue(List<zxjc_check_image> list)
        {
            try
            {
                list.ForEach(t => { t.lrr = _u.CurrentUser.name; t.lrsj = DateTime.Now; });
                return list;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<string> GetCheckImages(zxjc_check_image parm)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select tplj,tpmc FROM zxjc_check_image where cpfw = :cpfw and cpxh = :cpxh");
                using (var db = new OracleConnection(ConString))
                {
                    List<string> resultlist = new List<string>();
                    string path = "http://172.16.201.125:7002/upload/image/";
                    var q = db.Query<zxjc_check_image>(sql.ToString(), new { cpfw = parm.cpfw, cpxh = parm.cpxh });
                    q.Select(t => t.tpmc).ToList().ForEach(t =>
                    {
                        resultlist.Add(path + t);
                    });
                    return resultlist;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

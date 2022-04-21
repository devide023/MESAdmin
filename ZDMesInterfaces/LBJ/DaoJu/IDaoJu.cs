using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
using ZDMesModels.LBJ;
namespace ZDMesInterfaces.LBJ.DaoJu
{
    public interface IDaoJu
    {
        /// <summary>
        /// 刀柄刃具领用
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        bool DbRjLy(dbrjly_form from);
        /// <summary>
        /// 刀柄刃具关系
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        IEnumerable<sys_dbrjly> DbRjGxList(string dbh);
        /// <summary>
        /// 刀柄号查询刀柄刃具在线
        /// </summary>
        /// <param name="dbh"></param>
        /// <returns></returns>
        IEnumerable<base_dbrjzx> DbRjZxList(string dbh);
        /// <summary>
        /// 刀柄报废
        /// </summary>
        /// <param name="dbh"></param>
        /// <returns></returns>
        bool SetDbBF(string dbh);
        /// <summary>
        /// 在线刃具刃磨
        /// </summary>
        /// <param name="rjlx"></param>
        /// <returns></returns>
        bool SetRjSm(List<int> ids);
        /// <summary>
        /// 生产线查机加设备列表
        /// </summary>
        /// <param name="scx"></param>
        /// <returns></returns>
        IEnumerable<base_cnc> Get_CnC_By_Scx(string scx);
        /// <summary>
        /// 设备编号查询刀柄
        /// </summary>
        /// <param name="sbbh"></param>
        /// <returns></returns>
        IEnumerable<base_dbxx> GetDbxxBySbbh(string sbbh);


        
    }
}

﻿using System;
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
        /// 刀柄刃具关系
        /// </summary>
        /// <param name="dbh"></param>
        /// <returns></returns>
        IEnumerable<sys_db_rj_gx> DbRjGxList(List<string> dbh);
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
        /// 设备编号查询刀柄
        /// </summary>
        /// <param name="sbbh"></param>
        /// <returns></returns>
        IEnumerable<base_dbxx> GetDbxxBySbbh(string sbbh);
        /// <summary>
        /// 刀柄领用
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        bool DaoBinRenJuLy(dbrjlyform form);
        /// <summary>
        /// 刀柄号获取刃具信息
        /// </summary>
        /// <param name="dbbh"></param>
        /// <returns></returns>
        IEnumerable<base_dbrjgx> GetRjxxByDbBh(List<string> dbh);
        /// <summary>
        /// 安装刃具
        /// </summary>
        /// <param name="zxlist"></param>
        /// <returns></returns>
        bool InstallRjXx(List<base_dbrjzx> zxlist);
        /// <summary>
        /// 卸载刃具
        /// </summary>
        /// <param name="zxids"></param>
        /// <returns></returns>
        bool UnInstallRjXx(List<int> zxids);
        /// <summary>
        /// 刀柄号查刃具在线
        /// </summary>
        /// <param name="dbh"></param>
        /// <returns></returns>
        IEnumerable<base_dbrjzx> GetRjZxByDbh(string dbh);
        /// <summary>
        /// 根据刀柄号选择刃具类型
        /// </summary>
        /// <param name="dbh"></param>
        /// <returns></returns>
        IEnumerable<base_dbrjzx> ChooseRjlxByDbh(string dbh);
        /// <summary>
        /// 在线刃具安装
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        bool ZxRjInstall(List<base_dbrjzx> list);
        /// <summary>
        /// 在线刃具更换
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        bool ZxRjChange(List<base_dbrjzx> list);
        /// <summary>
        /// 更换刀柄刃具
        /// </summary>
        /// <param name="from"></param>
        /// <returns></returns>
        bool Save_DbRjZx_Change(sys_dbrj_bgly_form form);
    }
}

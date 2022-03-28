using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
namespace ZDMesInterfaces.Common
{
    public interface IMenu
    {
        /// <summary>
        /// 获取菜单对应的角色
        /// </summary>
        /// <param name="menuid"></param>
        /// <returns></returns>
        IEnumerable<mes_role_entity> Get_Menu_Roles(int menuid);
        /// <summary>
        /// 获取菜单对应的用户
        /// </summary>
        /// <param name="menuid"></param>
        /// <returns></returns>
        IEnumerable<mes_user_entity> Get_Menu_Users(int menuid);
        /// <summary>
        /// 获取菜单编码
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        int Get_MenuMaxCode(int pid);
        /// <summary>
        /// 获取菜单树
        /// </summary>
        /// <returns></returns>
        IEnumerable<mes_menu_entity> Get_MenuTree();
        /// <summary>
        /// 获取页面字段树
        /// </summary>
        /// <returns></returns>
        IEnumerable<mes_menu_entity> Get_ColsTree();
    }
}

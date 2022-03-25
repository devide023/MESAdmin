using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
namespace ZDMesInterfaces.Common
{
    public interface IRole
    {
        /// <summary>
        /// 获取角色菜单
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        IEnumerable<mes_menu_entity> Get_Role_Menus(int roleid);
        /// <summary>
        /// 获取角色用户列表
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        IEnumerable<mes_user_entity> Get_Role_Users(int roleid);
        /// <summary>
        /// 保存角色菜单
        /// </summary>
        /// <param name="roleid"></param>
        /// <param name="menus"></param>
        /// <returns></returns>
        bool Save_Role_Menus(int roleid, List<mes_menu_entity> menus);
        /// <summary>
        /// 保存角色用户
        /// </summary>
        /// <param name="roleid"></param>
        /// <param name="users"></param>
        /// <returns></returns>
        bool Save_Role_Users(int roleid, List<int> userids);
    }
}

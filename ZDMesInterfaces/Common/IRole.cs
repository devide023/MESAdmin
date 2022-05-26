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
        /// 获取角色可编辑字段
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        IEnumerable<mes_menu_entity> Get_Role_Edit_Fields(int roleid);
        /// <summary>
        /// 获取角色隐藏字段
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        IEnumerable<mes_menu_entity> Get_Role_Hide_Fields(int roleid);
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
        bool Save_Role_Menus(sys_role_form form);
        /// <summary>
        /// 修改角色菜单
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        bool Edit_Role_Menus(sys_role_form form);
        /// <summary>
        /// 保存角色用户
        /// </summary>
        /// <param name="roleid"></param>
        /// <param name="users"></param>
        /// <returns></returns>
        bool Save_Role_Users(sys_role_user_form from);
        /// <summary>
        /// 所有角色列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<mes_role_entity> All();
    }
}

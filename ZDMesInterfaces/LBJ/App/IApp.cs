using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels.LBJ;

namespace ZDMesInterfaces.LBJ.App
{
    public interface IApp
    {
        IEnumerable<app_user> Get_All_Users();
        IEnumerable<App_Menu> Get_App_Menus();
        /// <summary>
        /// 获取角色用户关系
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        IEnumerable<app_user> Get_Role_Users(int roleid);
        /// <summary>
        /// 获取角色菜单关系
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        IEnumerable<App_Menu> Get_Role_Menus(int roleid);
        /// <summary>
        /// 保存角色用户关系
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        bool Save_Role_Users(form_app_role_user form);
        /// <summary>
        /// 保存角色菜单关系
        /// </summary>
        /// <param name="form"></param>
        /// <returns></returns>
        bool Save_Role_Menus(form_app_role_menu form);
    }
}

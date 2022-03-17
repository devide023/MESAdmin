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
    }
}

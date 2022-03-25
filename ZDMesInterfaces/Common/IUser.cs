using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;
namespace ZDMesInterfaces.Common
{
    public interface IUser
    {
        /// <summary>
        /// 获取用户角色信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        IEnumerable<mes_role_entity> Get_User_Roles(string token);
        /// <summary>
        /// 获取用户菜单信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        IEnumerable<mes_menu_entity> Get_User_Menus(string token);
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        bool ChangePwd(string token,string newpwd);
        /// <summary>
        /// 重置Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        bool ReSetToken(string token);
        /// <summary>
        /// 保存用户角色
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        bool Save_User_Roles(int userid, List<int> roleids);
        /// <summary>
        /// 根据用户名、密码。获取用户Token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        sys_login_result Login(sys_user user);
        /// <summary>
        /// 系统退出
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        sys_result Logout(string token);
        /// <summary>
        /// 根据token获取用户信息、菜单权限
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        sys_userinfo_result GetUserInfo(string token);
        
    }
}

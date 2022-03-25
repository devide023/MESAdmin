using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZDMesModels;

namespace ZDMesInterfaces.Common
{
    /// <summary>
    /// 前端页面配置接口
    /// </summary>
    public interface IPageConfig
    {
        /// <summary>
        /// 获取页面配置信息
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        string GetPageConf(string route);
        /// <summary>
        /// 获取字段权限信息，包括可编辑字段，隐藏字段
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        sys_menu_permis GetPagePermis(string route, string token);
        /// <summary>
        /// 查询页面所有字段信息
        /// </summary>
        /// <param name="menuid"></param>
        /// <returns></returns>
        List<sys_field_info> GetPageFields(int menuid);
    }


}

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
        /// <summary>
        /// 获取页面功能按钮
        /// </summary>
        /// <param name="route"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        List<sys_pagefn_info> GetPageFnList(string route, string token);
        /// <summary>
        /// 获取页面批处理按钮
        /// </summary>
        /// <param name="route"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        List<sys_pagefn_info> GetPageBatList(string route, string token);
        /// <summary>
        /// 获取路由路径与组件对应关系
        /// </summary>
        /// <returns></returns>
        IEnumerable<sys_route_component> GetRouteComponent();
        /// <summary>
        /// 保存页面配置信息
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        bool Save_Page_Config(sys_page_config config);
    }


}

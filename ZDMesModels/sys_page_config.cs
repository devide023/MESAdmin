using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels
{
    public class sys_page_config
    {
        /// <summary>
        /// 菜单项
        /// </summary>
        public mes_menu_entity menu { get; set; }
        /// <summary>
        /// 页面基本配置
        /// </summary>
        public base_page_config baseconfig { get;set;}
        /// <summary>
        /// 页面字段配置
        /// </summary>
        public List<sys_page_field> fields { get; set; }
        /// <summary>
        /// 页面Api配置
        /// </summary>
        public List<sys_page_api> pageapis { get; set; }
        /// <summary>
        /// 页面函数配置
        /// </summary>
        public List<sys_page_fn> pagefn { get; set; }
        /// <summary>
        /// 页面表单配置
        /// </summary>
        public List<sys_page_form> pageform { get; set; }
        /// <summary>
        /// 操作列功能项目
        /// </summary>
        public List<sys_operate_item> operate_fnlist { get; set; }
        /// <summary>
        /// 批量操作（新增导入，替换导入，综合导入）
        /// </summary>
        public object batoperate { get; set; }
        /// <summary>
        /// 批量操作扩展菜单
        /// </summary>
        public List<sys_bat_btn_info> bat_btnlist { get; set; }
        /// <summary>
        /// 操作列菜单展示方式，默认是下拉展开。可设置为text文本平铺方式
        /// </summary>
        public string operate_type { get; set; }
    }
}

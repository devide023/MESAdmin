using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels
{
    public class mes_menu_entity
    {
        /// <summary>
        ///id
        ///</summary>
        public int id { get; set; }
        /// <summary>
        ///父id
        ///</summary>
        public int pid { get; set; }
        /// <summary>
        ///状态
        ///</summary>
        public int status { get; set; } = 1;
        /// <summary>
        ///菜单编码
        ///</summary>
        public string code { get; set; }
        /// <summary>
        ///菜单名称
        ///</summary>
        public string name { get; set; }
        /// <summary>
        ///菜单类型
        ///</summary>
        public string menutype { get; set; }
        /// <summary>
        ///菜单图标
        ///</summary>
        public string icon { get; set; }
        /// <summary>
        ///路由路径
        ///</summary>
        public string routepath { get; set; }
        /// <summary>
        ///视图路径
        ///</summary>
        public string viewpath { get; set; }
        /// <summary>
        ///排序
        ///</summary>
        public int seq { get; set; } = 10;
        /// <summary>
        ///配置路径
        ///</summary>
        public string configpath { get; set; }
        /// <summary>
        ///组件名称
        ///</summary>
        public string componentname { get; set; } = string.Empty;
        /// <summary>
        /// 函数名称
        /// </summary>
        public string fnname { get; set; } = string.Empty;
        /// <summary>
        /// 按钮类型
        /// </summary>
        public string btntype { get; set; }
        /// <summary>
        ///录入人
        ///</summary>
        public int adduser { get; set; }
        /// <summary>
        ///录入时间
        ///</summary>
        public DateTime addtime { get; set; } = DateTime.Now;
        /// <summary>
        /// 针对角色，赋予菜单下的子权限
        /// </summary>
        public sys_menu_permis menupermission { get; set; }
        /// <summary>
        /// 子菜单
        /// </summary>
        public List<mes_menu_entity> children { get; set; } = new List<mes_menu_entity>();
        /// <summary>
        /// 是否有子菜单
        /// </summary>
        public bool hasChildren { get; set; } = false;
        public bool isdb { get; set; } = true;
        public bool isedit { get; set; } = false;

    }

    public class mes_menu_entity_mapper : ClassMapper<mes_menu_entity>
    {
        public mes_menu_entity_mapper()
        {
            Map(t => t.id).Key(KeyType.TriggerIdentity);
            Map(t => t.menupermission).Ignore();
            Map(t => t.children).Ignore();
            Map(t => t.hasChildren).Ignore();
            Map(t => t.isdb).Ignore();
            Map(t => t.isedit).Ignore();
            AutoMap();
        }
    }
}

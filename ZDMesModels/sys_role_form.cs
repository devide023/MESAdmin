using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels
{
    public class sys_role_form
    {
        public mes_role_entity mes_role_entity { get; set; }
        /// <summary>
        /// 功能权限
        /// </summary>
        public List<mes_menu_entity> permission { get; set; }
        /// <summary>
        /// 可编辑字段
        /// </summary>
        public List<mes_menu_entity> editfields { get; set; }
        /// <summary>
        /// 隐藏字段
        /// </summary>
        public List<mes_menu_entity> hidefields { get; set; }
    }
}

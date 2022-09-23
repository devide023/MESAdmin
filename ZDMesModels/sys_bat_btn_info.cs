using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels
{
    /// <summary>
    /// 批量操作扩展菜单项
    /// </summary>
    public class sys_bat_btn_info
    {
        /// <summary>
        /// 按钮文本内容
        /// </summary>
        public string btntxt { get; set; }
        /// <summary>
        /// 按钮执行函数名称
        /// </summary>
        public string fnname { get; set; }
    }
}

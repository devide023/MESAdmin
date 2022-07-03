using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels
{
    public class sys_pagefn_info
    {
        public string name { get; set; }
        /// <summary>
        /// 按钮执行函数名称
        /// </summary>
        public string fnname { get; set; }
        /// <summary>
        /// 按钮文本
        /// </summary>
        public string btntxt { get; set; }
        /// <summary>
        /// 按钮图标
        /// </summary>
        public string icon { get; set; }
        /// <summary>
        /// 按钮类型
        /// </summary>
        public string btntype { get; set; }
    }
}

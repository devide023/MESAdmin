﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.LBJ
{
    public class sys_wbzq_form
    {
        public string kssj { get; set; }
        public string jssj { get; set; }
        /// <summary>
        /// 下次维保时间段
        /// </summary>
        public List<string> next_date { get; set; }
        /// <summary>
        /// 维保项目
        /// </summary>
        public List<base_sbwb_ls> sbwbls { get; set; }
    }
}

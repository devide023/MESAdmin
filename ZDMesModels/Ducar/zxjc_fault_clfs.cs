﻿using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.Ducar
{
    public class zxjc_fault_clfs
    {

        public string rid { get; set; }
        /// <summary>
        /// 工厂
        /// </summary>
        public string gcdm { get; set; } = "101";
        /// <summary>
        /// 生产线
        /// </summary>
        public string scx { get; set; }
        /// <summary>
        /// 岗位编码
        /// </summary>
        public string gwh { get; set; }
        /// <summary>
        /// 岗位名称
        /// </summary>
        public string gwmc { get; set; }
        /// <summary>
        /// 故障代码
        /// </summary>
        public string faultno { get; set; }
        /// <summary>
        /// 故障名称
        /// </summary>
        public string faultname { get; set; }
        /// <summary>
        /// 处理方式代码
        /// </summary>
        public string handno { get; set; }
        /// <summary>
        /// 处理方式名称
        /// </summary>
        public string handname { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 录入人
        /// </summary>
        public string lrr { get; set; } 
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime? lrsj { get; set; } = DateTime.Now;
        public List<sys_options_list> gwlist { get; set; }
        public List<sys_options_list> gwhs { get; set; }
    }

    public class sys_options_list
    {
        public string label { get; set; }
        public string value { get; set; }
    }

    public class zxjc_fault_clfs_mapper : ClassMapper<zxjc_fault_clfs>
    {
        public zxjc_fault_clfs_mapper()
        {
            Map(t => t.faultno).Column("fault_no");
            Map(t => t.handno).Column("hand_no");
            Map(t => t.handname).Column("hand_name");
            Map(t => t.gwmc).Ignore();
            Map(t => t.rid).Ignore();
            Map(t => t.gwlist).Ignore();
            Map(t => t.gwhs).Ignore();
            Map(t => t.faultname).Ignore();
            AutoMap();
        }
    }
}

﻿using DapperExtensions.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.Ducar
{
    public class zxjc_fault
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
        /// 故障分类
        /// </summary>
        public string faultfl { get; set; }
        /// <summary>
        /// 机型
        /// </summary>
        public string jxno { get; set; }
        /// <summary>
        /// 状态码
        /// </summary>
        public string statusno { get; set; }
        /// <summary>
        /// 默认处理岗位(一个)
        /// </summary>
        public string gwhbz { get; set; }
        public string clgwmc { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string bz { get; set; }
        /// <summary>
        /// 录入人
        /// </summary>
        public string lrr { get; set; } 
        /// <summary>
        /// 录入时间
        /// </summary>
        public DateTime? lrsj { get; set; } = DateTime.Now;
        /// <summary>
        /// 返修图片
        /// </summary>
        public string tpname { get; set; }
        /// <summary>
        /// 岗位号多选
        /// </summary>
        public List<sys_options_list> gwhs { get; set; }
        /// <summary>
        /// 机型
        /// </summary>
       // public List<string> jxnolist { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        //public List<string> statuslist { get; set; }
    }

    public class zxjc_fault_mapper : ClassMapper<zxjc_fault>
    {
        public zxjc_fault_mapper()
        {
            Map(t => t.gcdm).Key(KeyType.Assigned);
            Map(t => t.scx).Key(KeyType.Assigned);
            Map(t => t.gwh).Key(KeyType.Assigned);
            Map(t => t.faultno).Key(KeyType.Assigned);
            Map(t => t.faultno).Column("fault_no");
            Map(t => t.faultname).Column("fault_name");
            Map(t => t.faultfl).Column("fault_fl");
            Map(t => t.jxno).Column("jx_no");
            Map(t => t.statusno).Column("status_no");
            Map(t => t.gwhbz).Column("gwh_bz");
            Map(t => t.clgwmc).Ignore();
            Map(t => t.gwmc).Ignore();
            Map(t => t.rid).Ignore();
            //Map(t => t.jxnolist).Ignore();
            //Map(t => t.statuslist).Ignore();
            Map(t => t.gwhs).Ignore();
            AutoMap();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.CDGC
{
    public class base_sbxx
    {
        /// <summary>
        ///工厂
        ///</summary>
        public string gcdm { get; set; }
        /// <summary>
        ///生产线
        ///</summary>
        public string scx { get; set; }
        /// <summary>
        ///设备编号（资产号）
        ///</summary>
        public string sbbh { get; set; }
        /// <summary>
        /// 设备自编号
        /// </summary>
        public string sbzbh { get; set; }
        /// <summary>
        ///设备名称
        ///</summary>
        public string sbmc { get; set; }
        /// <summary>
        ///岗位号
        ///</summary>
        public string gwh { get; set; }
        /// <summary>
        ///设备类型（数控机床、刻字机、干检、spc、扭力枪）
        ///</summary>
        public string sblx { get; set; }
        /// <summary>
        ///连接类型（pcs直连、不连接、自连接、scada直连）
        ///</summary>
        public string ljlx { get; set; }
        /// <summary>
        ///IP地址
        ///</summary>
        public string ip { get; set; }
        /// <summary>
        ///端口号
        ///</summary>
        public string port { get; set; }
        /// <summary>
        ///串口号
        ///</summary>
        public string com { get; set; }
        /// <summary>
        ///是否可用
        ///</summary>
        public string sfky { get; set; }
        /// <summary>
        ///是否连接（系统是否管理）
        ///</summary>
        public string sflj { get; set; }
        /// <summary>
        ///备注
        ///</summary>
        public string bz { get; set; }
        /// <summary>
        ///录入人
        ///</summary>
        public string lrr { get; set; }
        /// <summary>
        ///录入时间
        ///</summary>
        public DateTime lrsj { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using DapperExtensions.Mapper;
namespace ZDMesModels.LBJ
{
    /// <summary>
    /// 设备参数信息流水表
    ///</summary>
    public class zxjc_sbxx_ls_cnc
    {
        /// <summary>
        ///设备编号（资产号）
        ///</summary>
        public string sbbh { get; set; }
        /// <summary>
        ///设备名称
        ///</summary>
        public string sbmc { get; set; }
        /// <summary>
        ///工厂
        ///</summary>
        public string gcdm { get; set; }
        /// <summary>
        ///生产线
        ///</summary>
        public string scx { get; set; }
        /// <summary>
        ///岗位号
        ///</summary>
        public string gwh { get; set; }
        /// <summary>
        ///加工数
        ///</summary>
        public string jgs { get; set; }
        /// <summary>
        ///运行状态
        ///</summary>
        public string yxzt { get; set; }
        /// <summary>
        ///循环时间
        ///</summary>
        public string xhsj { get; set; }
        /// <summary>
        ///切削时间
        ///</summary>
        public string qxsj { get; set; }
        /// <summary>
        ///操作时间
        ///</summary>
        public string czsj { get; set; }
        /// <summary>
        ///开机时间
        ///</summary>
        public string kjsj { get; set; }
        /// <summary>
        ///运行时间
        ///</summary>
        public string yxsj { get; set; }
        /// <summary>
        ///节拍
        ///</summary>
        public string jp { get; set; }
        /// <summary>
        ///进给速度
        ///</summary>
        public string jjsd { get; set; }
        /// <summary>
        ///主轴转速
        ///</summary>
        public string zzzs { get; set; }
        /// <summary>
        ///数据采集时间
        ///</summary>
        public DateTime lrsj { get; set; }
        /// <summary>
        ///ID
        ///</summary>
        public string id { get; set; }
        /// <summary>
        ///主轴刀具编号
        ///</summary>
        public string zzdjbh { get; set; }
        /// <summary>
        ///下一把刀具编号
        ///</summary>
        public string xybdjbh { get; set; }
        /// <summary>
        ///快速进给倍率
        ///</summary>
        public string ksjjbl { get; set; }
        /// <summary>
        ///进给速度超程倍率
        ///</summary>
        public string jjsdccbl { get; set; }
        /// <summary>
        ///主轴倍率
        ///</summary>
        public string zzbl { get; set; }
        /// <summary>
        ///当前程序名
        ///</summary>
        public string dqcxm { get; set; }
        /// <summary>
        ///报警编号
        ///</summary>
        public string bjbh { get; set; }
        /// <summary>
        ///主轴负载
        ///</summary>
        public string zzfz { get; set; }
        /// <summary>
        ///伺服负载1
        ///</summary>
        public string cffz1 { get; set; }
        /// <summary>
        ///伺服负载2
        ///</summary>
        public string cffz2 { get; set; }
        /// <summary>
        ///伺服负载3
        ///</summary>
        public string cffz3 { get; set; }
        /// <summary>
        ///伺服负载4
        ///</summary>
        public string cffz4 { get; set; }
    }

    public class zxjc_sbxx_ls_cnc_mapper:ClassMapper<zxjc_sbxx_ls_cnc>
    {
        public zxjc_sbxx_ls_cnc_mapper()
        {
            Map(t => t.id).Key(KeyType.Assigned);
            AutoMap();
        }
    }
}

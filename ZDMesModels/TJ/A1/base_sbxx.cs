using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.TJ.A1
{
    public class base_sbxx
    {
        /// <summary>
        /// 设备编号(资产号)
        /// </summary>
        public string sbbh { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string sbmc { get; set; }
        /// <summary>
        /// 工厂
        /// </summary>
        public string gcdm { get; set; }
        /// <summary>
        /// 生产线
        /// </summary>
        public string scx { get; set; }
        /// <summary>
        /// 岗位号
        /// </summary>
        public string gwh { get; set; }
        /// <summary>
        /// 设备类型(ATLAS拧紧枪 打印机 PCS一体机 压机  ATLAS拧紧轴   检漏机  加油机  性能测试台架 )
        /// </summary>
        public string sblx { get; set; }
        /// <summary>
        /// 连接类型(PCS客户端计算机  不连接)
        /// </summary>
        public string ljlx { get; set; }
        /// <summary>
        /// 没用
        /// </summary>
        public string txfs { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        public string ip { get; set; }
        /// <summary>
        /// 端口号
        /// </summary>
        public string port { get; set; }
        /// <summary>
        /// PLC序号
        /// </summary>
        public int plcxh { get; set; }
        /// <summary>
        /// 是否可用
        /// </summary>
        public string sfky { get; set; }
        /// <summary>
        /// 是否连接(系统是否管理)
        /// </summary>
        public string sflj { get; set; }
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
        public DateTime lrsj { get; set; }
        /// <summary>
        /// 串口号 没用
        /// </summary>
        public string com { get; set; }
        /// <summary>
        /// SCADA是否下发程序号（Y/N）
        /// </summary>
        public string iscxh { get; set; }
        /// <summary>
        /// SCADA是否为核心数据（Y/N）
        /// </summary>
        public string ishxsj { get; set; }
    }

    public class base_sbxx_mapper : ClassMapper<base_sbxx>
    {
        public base_sbxx_mapper()
        {
            AutoMap();
        }
    }
}

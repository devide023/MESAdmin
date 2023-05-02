using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DapperExtensions.Mapper;
namespace ZDMesModels.TJ.A1
{
    public class mes_pdm_jstz_yd
    {
        public string id { get; set; }
        /// <summary>
        /// pdm技通编号
        /// </summary>
        public string jcbh { get; set; }
        /// <summary>
        /// 阅读人姓名
        /// </summary>
        public string ydr { get; set; }
        /// <summary>
        /// 阅读时间
        /// </summary>
        public DateTime ydsj { get; set; } = DateTime.Now;
        /// <summary>
        /// 阅读人id
        /// </summary>
        public int ydrid { get; set; }
        /// <summary>
        /// 技通id
        /// </summary>
        public string jtid { get; set; }
        /// <summary>
        /// 组别id
        /// </summary>
        public string zbid { get; set; }
        /// <summary>
        /// 用户编码
        /// </summary>
        public string usercode { get; set; }
        /// <summary>
        /// 技术通知信息
        /// </summary>
        public zxjc_t_jstc jstcinfo { get; set; }
    }

    public class mes_pdm_jstz_yd_mapper:ClassMapper<mes_pdm_jstz_yd>
    {
        public mes_pdm_jstz_yd_mapper()
        {
            Map(t => t.id).Key(KeyType.Assigned);
            Map(t => t.jstcinfo).Ignore();
            AutoMap();
        }
    }
}

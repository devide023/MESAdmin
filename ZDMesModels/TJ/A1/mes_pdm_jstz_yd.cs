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
    }

    public class mes_pdm_jstz_yd_mapper:ClassMapper<mes_pdm_jstz_yd>
    {
        public mes_pdm_jstz_yd_mapper()
        {
            Map(t => t.id).Key(KeyType.Assigned);
            AutoMap();
        }
    }
}

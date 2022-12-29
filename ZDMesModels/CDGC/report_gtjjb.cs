using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels.CDGC
{
    public class report_gtjjb
    {
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime? rq { get; set; }
        /// <summary>
        /// 班次
        /// </summary>
        public string  bc { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string cpmc { get; set; }
        /// <summary>
        /// 投入数量
        /// </summary>
        public int  trjgs { get; set; }
        /// <summary>
        /// 工废数量
        /// </summary>
        public int  gfsl { get; set; }
        /// <summary>
        /// 料废数量
        /// </summary>
        public int  lfsl { get; set; }
        /// <summary>
        /// 合格数量
        /// </summary>
        public int  hgsl { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDMesModels
{
    public class sys_import_result<T> where T:class,new()
    {
        /// <summary>
        /// 成功数据
        /// </summary>
        public List<T> oklist { get; set; } = new List<T>();
        /// <summary>
        /// 重复数据
        /// </summary>
        public List<T> repeatlist { get; set; } = new List<T>();
        /// <summary>
        /// 删除数据
        /// </summary>
        public List<T> dellist { get; set; } = new List<T>();
        /// <summary>
        /// 更新前原始数据
        /// </summary>
        public List<T> orginallist { get; set; } = new List<T>();

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ZDMesModels.LBJ;
using Newtonsoft.Json;
namespace ZDToolHelper
{
    public class ConfigHelper
    {
        private string _configpath;
        public ConfigHelper()
        {

        }
        public ConfigHelper(string configpath)
        {
            _configpath = configpath;
        }

        public string SetConfigPath
        {
            set { this._configpath = value; }
        }

        public List<sys_import_log_config> Read_Import_LogConfig()
        {
            try
            {
                if (File.Exists(_configpath))
                {
                    string json = File.ReadAllText(_configpath, Encoding.UTF8);
                    return JsonConvert.DeserializeObject<List<sys_import_log_config>>(json);
                }
                else
                {
                    return new List<sys_import_log_config>();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ZDMesInterfaces.LBJ;
using ZDMesModels;

namespace ZDMesServices.LBJ.CheckData
{
    public class Form_Check_Service : IFormCheck
    {
        public bool Check_Form_Data(List<object> entitys, out sys_result result)
        {
            try
            {
                bool ret = true;
                result = new sys_result() { code = 0, msg = "" };
                var config_path = HttpContext.Current.Server.MapPath("~/Check_Form_Config.json");
                var config_json = File.ReadAllText(config_path, Encoding.UTF8);
                var config_list = JsonConvert.DeserializeObject<List<sys_form_check>>(config_json);
                var model = entitys.First().GetType().FullName + ",ZDMesModels";
                var sfczpz = config_list.Where(i => i.model == model);
                sys_form_check configobj = null;
                if (sfczpz.Count() > 0)
                {
                    configobj = sfczpz.First();
                }
                else
                {
                    result.msg = $"{model}表单数据检查未配置,联系管理员在Check_Form_Config文件中配置";
                    ret = false;
                }
                Type t = Type.GetType(model);
                var pis = t.GetProperties();
                foreach (var obj in entitys)
                {
                    string tipmsg = string.Empty;
                    foreach (var pi in pis)
                    {
                        var q = configobj.fields.Where(i => i.colname == pi.Name.ToLower());
                        if (q.Count() > 0)
                        {
                            var sxlx = pi.PropertyType.Name;
                            switch (sxlx)
                            {
                                case "String":
                                    var v = pi.GetValue(obj).ToString();
                                    if (string.IsNullOrEmpty(v))
                                    {
                                        tipmsg = tipmsg + q.First().collabel + "、";
                                    }
                                    break;
                                case "List`1":
                                    var l = pi.GetValue(obj) as IEnumerable<object>;
                                    if (l.Count() == 0)
                                    {
                                        tipmsg = tipmsg + q.First().collabel + "、";
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(tipmsg))
                    {
                        result.msg = tipmsg + "不能为空";
                        ret = false;
                        break;
                    }
                }
                return ret;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

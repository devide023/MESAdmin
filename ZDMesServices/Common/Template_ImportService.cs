using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ZDMesInterfaces.Common;
using ZDMesModels;
using Newtonsoft.Json;
using Aspose.Cells;
using System.Data;
using System.Net.Http;

namespace ZDMesServices.Common
{
    /// <summary>
    /// 模板文件导入实现类
    /// </summary>
    public class Template_ImportService : ITemplate_Import,IVerifyData
    {
        public string TemplateConfig { get; set; }
        public bool IsVerify { get; set; }
        public string VerifyConfigpath { get; set; }

        private bool CheckTemplate(DataTable source, List<config_item> config, out List<config_item> nofields)
        {
            try
            {
                if(source!=null && source.Rows.Count > 0)
                {
                    List<config_item> fieldcol = new List<config_item>();
                    List<config_item> nofield = new List<config_item>();
                    //读取第一行数据
                    DataRow dr = source.Rows[0];
                    for (int i = 0; i < source.Columns.Count; i++)
                    {
                        fieldcol.Add(new config_item()
                        {
                            fieldindex = i,
                            fieldname = dr[i].ToString()
                        });
                    }
                    foreach (var item in config)
                    {
                        var q = fieldcol.Where(t => item.fieldname == t.fieldname && item.fieldindex == t.fieldindex);
                        if (q.Count() == 0)
                        {
                            nofield.Add(item);
                        }
                    }
                    if (nofield.Count > 0)
                    {
                        nofields = nofield;
                        return false;
                    }
                    else
                    {
                        nofields = new List<config_item>();
                        return true;
                    }
                }
                else
                {
                    nofields = new List<config_item>();
                    return false;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public virtual IEnumerable<T> ReadData<T>(string filepath)
        {
            FileInfo finfo = new FileInfo(filepath);
            try
            {
                List<T> list = new List<T>();
                if (!string.IsNullOrEmpty(TemplateConfig))
                {
                    string configpath = HttpContext.Current.Server.MapPath(TemplateConfig);
                    string conf = File.ReadAllText(configpath, Encoding.UTF8);
                    List<sys_template_config> allconfig = JsonConvert.DeserializeObject<List<sys_template_config>>(conf);
                    List<config_item> config = allconfig.Where(t => t.tablename == typeof(T).Name.ToLower()).SelectMany(t => t.conf).ToList();

                    Workbook wk = new Workbook(filepath);
                    Cells cells = wk.Worksheets[0].Cells;
                    DataTable dataTable = cells.ExportDataTableAsString(0, 0, cells.MaxDataRow + 1, cells.MaxColumn + 1);
                    List<config_item> nofields = new List<config_item>();
                    var isok = CheckTemplate(dataTable, config, out nofields);
                    if (isok)
                    {
                        string objname = typeof(T).FullName + ",ZDMesModels";
                        var obj = Activator.CreateInstance<T>();
                        var pi = Type.GetType(objname).GetProperties();
                        for (int i = 1; i < dataTable.Rows.Count; i++)
                        {
                            for (int j = 0; j < dataTable.Columns.Count; j++)
                            {
                                var q = config.Where(t => t.fieldindex == j);
                                if (q.Count() > 0)
                                {
                                    var name = q.First().dbcolname;
                                    var val = dataTable.Rows[i][j].ToString();
                                    var pq = pi.Where(t => t.Name == name);
                                    if (pq.Count() > 0) {
                                       string sxlx = pq.First().PropertyType.Name.ToLower();
                                        switch (sxlx)
                                        {
                                            case "int32":
                                                if (pq.Count() > 0)
                                                {
                                                    pq.First().SetValue(obj, Convert.ToInt32(val));
                                                }
                                                break;
                                            case "decimal":
                                                if (pq.Count() > 0)
                                                {
                                                    pq.First().SetValue(obj, Convert.ToDecimal(val));
                                                }
                                                break;
                                            case "double":
                                                if (pq.Count() > 0)
                                                {
                                                    pq.First().SetValue(obj, Convert.ToDouble(val));
                                                }
                                                break;
                                            case "datetime":
                                                if (pq.Count() > 0 && !string.IsNullOrEmpty(val))
                                                {
                                                    pq.First().SetValue(obj, Convert.ToDateTime(val));
                                                }
                                                break;
                                            case "string":
                                                if (pq.Count() > 0)
                                                {
                                                    pq.First().SetValue(obj, val.ToString());
                                                }
                                                break;

                                            default:
                                                if (pq.Count() > 0)
                                                {
                                                    pq.First().SetValue(obj, val);
                                                }
                                                break;
                                        }
                                    }
                                }
                            }
                            list.Add(obj);
                        }
                        finfo.Delete();
                        return list;
                    }
                    else
                    {
                        finfo.Delete();
                        string qszd = string.Empty;
                        foreach (var item in nofields)
                        {
                            qszd = qszd + $"第{item.fieldindex + 1}列({item.fieldname}),";
                        }
                        qszd = qszd.Length > 0 ? qszd.Remove(qszd.Length - 1) : string.Empty;
                        throw new Exception($"模板格式不匹配：{qszd}");
                    }
                }
                else
                {
                    finfo.Delete();
                    throw new Exception("模板配置文件未找到");
                }
            }
            catch (Exception)
            {
                finfo.Delete();
                throw;
            }
        }

        public bool Verify_Data<T>(List<T> entitys, out sys_result msg)
        {
            if (IsVerify)
            {
                if (!string.IsNullOrEmpty(VerifyConfigpath))
                {
                    var config_path = HttpContext.Current.Server.MapPath(VerifyConfigpath);
                    var config_json = File.ReadAllText(config_path, Encoding.UTF8);
                    var config_list = JsonConvert.DeserializeObject<List<sys_form_check>>(config_json);
                    var model = entitys.First().GetType().FullName + ",ZDMesModels";
                    var modelshortname = entitys.First().GetType().Name;
                    Type t = Type.GetType(model);
                    var pis = t.GetProperties();
                    var sfczpz = config_list.Where(i => i.model == model);
                    sys_form_check configobj = null;
                    if (sfczpz.Count() > 0)
                    {
                        configobj = sfczpz.First();
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
                                            var v = pi.GetValue(obj)?.ToString();
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
                                msg = new sys_result()
                                {
                                    code = 0,
                                    msg = tipmsg + "不能为空"
                                };
                                return false;
                            }
                        }
                        msg = new sys_result();
                        return true;
                    }
                    else
                    {
                        msg = new sys_result()
                        {
                            code = 0,
                            msg = $"{modelshortname}未配置表单校验项"
                        };
                        return false;
                    }
                }
                else
                {
                    msg = new sys_result()
                    {
                        code = 0,
                        msg = "未检查到配置文件路径"
                    };
                    return false;
                }
            }
            else
            {
                msg = new sys_result();
                return true;
            }
        }
    }
}

using Aspose.Cells;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ZDMesInterfaces.Common;
using ZDMesModels;

namespace ZDMesServices.Common
{
    public class VerifyService : IRequireVerify, ITemplateVerify
    {
        private List<sys_verify_config> config_list = new List<sys_verify_config>();
        private string template_file_full_path = string.Empty;
        private IUser _user;
        public VerifyService(IUser user)
        {
            _user = user;
        }
        public string VerifyConfigPath { get; set; }
        private void InitConfig()
        {
            try
            {
                if (!string.IsNullOrEmpty(VerifyConfigPath))
                {
                    string configpath = HttpContext.Current.Server.MapPath(VerifyConfigPath);
                    string config_json = File.ReadAllText(configpath, Encoding.UTF8);
                    config_list = JsonConvert.DeserializeObject<List<sys_verify_config>>(config_json);
                }
                else
                {
                    throw new Exception("未设置数据校验配置文件地址");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 读取模板文件数据
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private DataTable ReadFileData(string filename)
        {
            try
            {
                template_file_full_path = HttpContext.Current.Server.MapPath($"~/Upload/Excel/{filename}");
                Workbook wk = new Workbook(template_file_full_path);
                Cells cells = wk.Worksheets[0].Cells;
                DataTable dataTable = cells.ExportDataTableAsString(0, 0, cells.MaxDataRow + 1, cells.MaxColumn + 1);
                return dataTable;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private bool CheckTemplate(Type type,string templatename,out DataTable filedata)
        {
            try
            {
                InitConfig();
                string modelfullname = type.FullName+ ",ZDMesModels";
                var modelpis = type.GetProperties();
                var conf_query = this.config_list.Where(t => t.model == modelfullname);
                DataTable file_data = new DataTable();
                //查询配置是否存在
                if (conf_query.Count() > 0)
                {
                    var configlist = conf_query.First().templateconf;
                    if (configlist.Count > 0)
                    {
                        //模板数据列
                        var templatecols = configlist.Select(t => t.colname.ToLower());
                        //模板数据列对应的实体属性
                        var templatepis = modelpis.Where(t => templatecols.Contains(t.Name.ToLower()));
                        file_data = ReadFileData(templatename);
                        if (file_data.Rows.Count > 0)
                        {
                            //取第一行列头数据,校验模板是否匹配
                            DataRow headerrow = file_data.Rows[0];
                            List<config_item> headerlist = new List<config_item>();
                            for (int i = 0; i < file_data.Columns.Count; i++)
                            {
                                //表格列名
                                string headertext = headerrow[i].ToString().Trim();
                                headerlist.Add(new config_item()
                                {
                                    colindex = i,
                                    coltxt = headertext
                                });
                            }
                            //模板字段匹配检查
                            foreach (var item in configlist)
                            {
                                var mathquery = headerlist.Where(t => t.colindex == item.colindex && t.coltxt == item.coltxt);
                                if (mathquery.Count() == 0)
                                {
                                    new FileInfo(template_file_full_path).Delete();
                                    throw new Exception($"第{item.colindex + 1}列应为{item.coltxt},请检查模板");
                                }
                            }
                            
                        }
                    }
                }
                filedata = file_data;
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ImportTemplateVerify<T>(string templatename)
        {
            try
            {
                DataTable filedata = new DataTable();
                CheckTemplate(typeof(T), templatename,out filedata);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ImportTemplateVerify(string entityname, string templatename, out List<object> list)
        {
            try
            {
                Type type = Type.GetType(entityname);
                DataTable filedata = new DataTable();
                var pis = type.GetProperties();
                List<object> outlist = new List<object>();
                CheckTemplate(type, templatename,out filedata);
                var conf_query = this.config_list.Where(t => t.model == type.FullName+ ",ZDMesModels");
                if (conf_query.Count() > 0)
                {
                    var configlist = conf_query.First().templateconf;
                    var collist = configlist.Select(t => t.colname).ToList();
                    var colpis = pis.Where(t => collist.Contains(t.Name.ToLower()));
                    var lrrpis = pis.Where(t => t.Name.ToLower() == "lrr" && t.PropertyType == typeof(string));
                    //读取数据并转List
                    if (filedata.Rows.Count > 0)
                    {
                        for (int i = 1; i < filedata.Rows.Count; i++)
                        {
                            var obj = Activator.CreateInstance(type);
                            if (lrrpis.Count() > 0)
                            {
                                foreach (var p in lrrpis)
                                {
                                    p.SetValue(obj, _user.CurrentUser().name);
                                }
                            }
                            foreach (var p in colpis)
                            {
                                //属性类型
                                var sxlx = p.PropertyType;
                                //属性是否存在
                                var sxsfcz = configlist.Where(t => t.colname == p.Name.ToLower());
                                if (sxsfcz.Count() > 0)
                                {
                                    var colindex = sxsfcz.First().colindex;
                                    var val = filedata.Rows[i][colindex];
                                    if (val != null)
                                    {
                                        if (sxlx == typeof(String))
                                        {
                                            p.SetValue(obj, val?.ToString());
                                        }
                                        else if(sxlx == typeof(DateTime?) || sxlx == typeof(DateTime))
                                        {
                                            if (!string.IsNullOrEmpty(val.ToString()))
                                            {
                                                p.SetValue(obj, Convert.ToDateTime(val));
                                            }
                                        }
                                        else if (sxlx == typeof(Int32?) || sxlx == typeof(Int32))
                                        {
                                            if (!string.IsNullOrEmpty(val.ToString()))
                                            {
                                                p.SetValue(obj, Convert.ToInt32(val));
                                            }
                                        }
                                        else if (sxlx == typeof(Decimal?) || sxlx == typeof(Decimal))
                                        {
                                            if (!string.IsNullOrEmpty(val.ToString()))
                                            {
                                                p.SetValue(obj, Convert.ToDecimal(val));
                                            }
                                        }
                                        else if (sxlx == typeof(Double?) || sxlx == typeof(Double))
                                        {
                                            if (!string.IsNullOrEmpty(val.ToString()))
                                            {
                                                p.SetValue(obj, Convert.ToDouble(val));
                                            }
                                        }
                                        //switch (sxlx)
                                        //{
                                        //    case "string":
                                        //        p.SetValue(obj, val?.ToString());
                                        //        break;
                                        //    case "datetime":
                                        //        p.SetValue(obj, Convert.ToDateTime(val));
                                        //        break;
                                        //    case "int32":
                                        //        p.SetValue(obj, Convert.ToInt32(val));
                                        //        break;
                                        //    case "decimal":
                                        //        p.SetValue(obj, Convert.ToDecimal(val));
                                        //        break;
                                        //    case "double":
                                        //        p.SetValue(obj, Convert.ToDouble(val));
                                        //        break;
                                        //    default:
                                        //        break;
                                        //}
                                    }
                                }
                            }
                            outlist.Add(obj);
                        }
                    }
                }                    
                list = outlist;
                return true;
            }
            catch (Exception)
            {
                new FileInfo(template_file_full_path).Delete();
                throw;
            }
        }

        private bool CheckRequire(Type type,List<object> entitys)
        {
            try
            {
                InitConfig();
                var entityname = type.FullName;
                var pis = type.GetProperties();
                //查找配置
                var q = config_list.Where(i => i.model == entityname+ ",ZDMesModels");
                if (q.Count() > 0)
                {
                    var configitem = q.First();
                    if (configitem.requireconf.Count > 0)
                    {
                        //必填项检查
                        var requireitems = configitem.requireconf.Select(t => t.colname);
                        var requirepis = pis.Where(t => requireitems.Contains(t.Name.ToLower()));
                        foreach (var entity in entitys)
                        {
                            foreach (var requirepi in requirepis)
                            {
                                string piname = requirepi.Name.ToString();
                                //属性类型
                                string pilx = requirepi.PropertyType.Name;
                                var entityval = requirepi.GetValue(entity);
                                string coltext = configitem.requireconf.Where(t => t.colname == piname.ToLower()).FirstOrDefault().coltxt;
                                switch (pilx)
                                {
                                    case "Nullable`1":
                                        if (entityval == null)
                                        {
                                            throw new Exception($"{coltext}不能为空");
                                        }
                                        break;
                                    case "Int32":
                                        if (Convert.ToInt32(entityval) == 0)
                                        {
                                            throw new Exception($"{coltext}不能为空");
                                        }
                                        break;
                                    case "Int64":
                                        if (Convert.ToInt64(entityval) == 0)
                                        {
                                            throw new Exception($"{coltext}不能为空");
                                        }
                                        break;
                                    case "String":
                                        if (string.IsNullOrEmpty(entityval?.ToString()))
                                        {
                                            throw new Exception($"{coltext}不能为空");
                                        }
                                        break;
                                    case "List`1":
                                        var l = entityval as IEnumerable<object>;
                                        if (l.Count() == 0)
                                        {
                                            throw new Exception($"{coltext}不能为空");
                                        }
                                        break;
                                    default:
                                        if (entityval == null)
                                        {
                                            throw new Exception($"{coltext}不能为空");
                                        }
                                        break;
                                }
                            }
                        }
                    }
                    return true;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool VerifyRequire<T>(List<T> entitys)
        {
            try
            {
                Type type = typeof(T);
                var list = entitys.ConvertAll(t => (object)t);
                return CheckRequire(type, list);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool VerifyRequire(Type type, List<object> entitys)
        {
            try
            {
                return CheckRequire(type, entitys);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

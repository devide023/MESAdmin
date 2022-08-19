using Autofac;
using Autofac.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Autofac.Extras.DynamicProxy;
using ZDMesInterceptor.LBJ;
using ZDMesInterceptor;
using ZDMesServices.LBJ.ImportData;
using ZDMesInterfaces.LBJ.ImportData;

namespace ZDMesModules.CD
{
    public class CDGCModule: Autofac.Module
    {
        /// <summary>
        /// 成都工厂
        /// </summary>
        public string CDGCConstr { get; set; }
        protected override void Load(ContainerBuilder builder)
        {
            var AssServices = Assembly.Load("ZDMesServices");
            builder.RegisterAssemblyTypes(AssServices).Where(t => t.FullName.StartsWith("ZDMesServices.Common")).WithParameter("constr", CDGCConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors();
            builder.RegisterAssemblyTypes(AssServices).Where(t => t.FullName.StartsWith("ZDMesServices.CDGC")).WithParameter("constr", CDGCConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors();
            var AssInterfaces = Assembly.Load("ZDMesInterfaces");
            builder.RegisterAssemblyTypes(AssInterfaces).Where(t => t.FullName.StartsWith("ZDMesInterfaces.CDGC"));
            builder.RegisterGeneric(typeof(ImportDataService<>)).As(typeof(IImportData<>)).WithParameter("constr", CDGCConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors();
            //注册拦截器
            builder.Register(c => new CUDLogger(CDGCConstr));
            builder.Register(c => new ImportLog(CDGCConstr));
            builder.Register(c => new UserLog(CDGCConstr));
            var module_config = new Microsoft.Extensions.Configuration.ConfigurationBuilder();
            module_config.AddJsonFile("CDGC_module_config.json");
            // Register the ConfigurationModule with Autofac.
            var module = new ConfigurationModule(module_config.Build());
            builder.RegisterModule(module);
        }
    }
}

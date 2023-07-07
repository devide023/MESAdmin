using Autofac;
using Autofac.Configuration;
using Autofac.Extras.DynamicProxy;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Reflection;
using ZDMesInterceptor;
using ZDMesInterceptor.CDGC;
using ZDMesInterceptor.LBJ;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ.ImportData;
using ZDMesServices.Common;
using ZDMesServices.LBJ.ImportData;

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
            builder.RegisterAssemblyTypes(AssInterfaces).Where(t => t.FullName.StartsWith("ZDMesInterfaces")).PreserveExistingDefaults();
            builder.RegisterGeneric(typeof(ImportDataService<>)).Named("cdgcimp", typeof(IImportData<>)).WithParameter("constr", CDGCConstr).PropertiesAutowired().WithProperty("Import_Config_File_Path", "~/CDGC_Import_Config.json").AsImplementedInterfaces().EnableInterfaceInterceptors();
            //builder.RegisterType<Form_Check_Service>().Named("cdgcformcheck",typeof(IFormCheck));
            builder.RegisterType<VerifyService>().As<ITemplateVerify>().As<IRequireVerify>()
                .PropertiesAutowired().WithProperty("VerifyConfigPath", "~/CDGCVerfyConfig.json")
                .AsImplementedInterfaces().EnableInterfaceInterceptors();
            //注册拦截器
            builder.Register(c => new CUDLogger(CDGCConstr));
            builder.Register(c => new ImportLog(CDGCConstr));
            builder.Register(c => new UserLog(CDGCConstr));
            builder.Register(c => new JJBLog(CDGCConstr));
            builder.Register(t => new GTJCLog(CDGCConstr));
            var module_config = new Microsoft.Extensions.Configuration.ConfigurationBuilder();
            module_config.AddJsonFile("CDGC_module_config.json");
            // Register the ConfigurationModule with Autofac.
            var module = new ConfigurationModule(module_config.Build());
            builder.RegisterModule(module);
        }
    }
}

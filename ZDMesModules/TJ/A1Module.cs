using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Configuration;
using Autofac.Extras.DynamicProxy;
using Microsoft.Extensions.Configuration;
using ZDMesInterceptor;
using ZDMesInterceptor.A1;
using ZDMesInterceptor.LBJ;
using ZDMesInterfaces.Common;
using ZDMesInterfaces.LBJ.ImportData;
using ZDMesServices.Common;
using ZDMesServices.LBJ.ImportData;

namespace ZDMesModules.TJ
{
    public class A1Module: Autofac.Module
    {
        public string TJConstr { get; set; }
        protected override void Load(ContainerBuilder builder)
        {
            var AssServices = Assembly.Load("ZDMesServices");
            builder.RegisterAssemblyTypes(AssServices).Where(t => t.FullName.StartsWith("ZDMesServices.Common")).WithParameter("constr", TJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors();
            builder.RegisterAssemblyTypes(AssServices).Where(t => t.FullName.StartsWith("ZDMesServices.TJ")).WithParameter("constr", TJConstr).PropertiesAutowired().AsImplementedInterfaces().EnableInterfaceInterceptors();
            var AssInterfaces = Assembly.Load("ZDMesInterfaces");
            builder.RegisterAssemblyTypes(AssInterfaces).Where(t => t.FullName.StartsWith("ZDMesInterfaces")).PreserveExistingDefaults();
            //builder.RegisterType<UserUtilService>().WithParameter("constr", TJConstr).SingleInstance();
            builder.RegisterGeneric(typeof(ImportDataService<>)).Named("tja1imp", typeof(IImportData<>)).WithParameter("constr", TJConstr).PropertiesAutowired().WithProperty("Import_Config_File_Path", "~/TJA1_Import_Config.json").AsImplementedInterfaces().EnableInterfaceInterceptors();
            //builder.RegisterType<Form_Check_Service>().Named("cdgcformcheck",typeof(IFormCheck));
            builder.RegisterType<VerifyService>().As<ITemplateVerify>().As<IRequireVerify>()
                .PropertiesAutowired().WithProperty("VerifyConfigPath", "~/TJA1VerfyConfig.json")
                .AsImplementedInterfaces().EnableInterfaceInterceptors();
            //注册拦截器
            builder.Register(c => new CUDLogger(TJConstr));
            builder.Register(c => new ImportLog(TJConstr));
            builder.Register(c => new UserLog(TJConstr));
            builder.Register(t => new JtFpLog(TJConstr));
            var module_config = new Microsoft.Extensions.Configuration.ConfigurationBuilder();
            module_config.AddJsonFile("TJA1_module_config.json");
            // Register the ConfigurationModule with Autofac.
            var module = new ConfigurationModule(module_config.Build());
            builder.RegisterModule(module);
        }
    }
}

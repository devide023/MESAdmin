using Autofac;
using Autofac.Configuration;
using Autofac.Integration.WebApi;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MesAdmin
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);            
            // Add the configuration to the ConfigurationBuilder.
            var autofac_config = new ConfigurationBuilder();
            autofac_config.AddJsonFile("autofac_config.json");
            // Register the ConfigurationModule with Autofac.
            var module = new ConfigurationModule(autofac_config.Build());
            var builder = new ContainerBuilder();
            var Ass = Assembly.Load("ZDMesModels");
            builder.RegisterAssemblyTypes(Ass);
            builder.RegisterModule(module);
            //
            var config = GlobalConfiguration.Configuration;
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            // OPTIONAL: Register the Autofac filter provider.
            builder.RegisterWebApiFilterProvider(config);
            // OPTIONAL: Register the Autofac model binder provider.
            builder.RegisterWebApiModelBinderProvider();
            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}

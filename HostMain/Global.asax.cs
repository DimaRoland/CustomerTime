
using System.Security;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using CustomerTimesTask.ApplicationServices;
using CustomerTimesTask.EntityFramework;
using CustomerTimesTask.Repositories;

[assembly: AllowPartiallyTrustedCallers]


namespace CustomerTimesTask
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public void Application_Start()
        {
            //AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var builder = new ContainerBuilder();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));
        }
    }

    public class AutofacWebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(ThisAssembly);
            builder.RegisterType<CustomTaskService>().As<ICustomTaskService>();
            builder.RegisterType<CustomTaskRepository>().As<ICustomTaskRepository>();
            builder.RegisterType<CustomerTimesTaskDbContext>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
using System.Net.Http.Formatting;
using System.Web.Http;
using Autofac;
using Autofac.Features.ResolveAnything;
using Autofac.Integration.WebApi;
using CustomerTimesTask.ApplicationServices;
using CustomerTimesTask.EntityFramework;
using CustomerTimesTask.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;

namespace CustomerTimesTask
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = ConfigureAutofac();
            ConfigureWebApi(app, container);
        }
    }

    public partial class Startup
    {
        private static void ConfigureWebApi(IAppBuilder app, IContainer container)
        {
            var configuration = new HttpConfiguration();
            app.UseWebApi(configuration);

            // Web API configuration and services
            configuration.Formatters.Clear();
            configuration.Formatters.Add(new JsonMediaTypeFormatter());
            configuration.Formatters.JsonFormatter.SerializerSettings.Formatting = Formatting.Indented;
            configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Web API routes
            configuration.MapHttpAttributeRoutes();

            configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }

    public partial class Startup
    {
        private static IContainer ConfigureAutofac()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<AutofacWebModule>();
            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());

            var container = builder.Build();
            return container;
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
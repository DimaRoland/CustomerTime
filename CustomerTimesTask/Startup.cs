using System.Net.Http.Formatting;
using System.Web.Http;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Features.ResolveAnything;
using Autofac.Integration.WebApi;
using CustomerTimesTask.ApplicationServices;
using CustomerTimesTask.EntityFramework;
using CustomerTimesTask.Repositories;
using MassTransit;
using MassTransit.Util;
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
            var bus = container.Resolve<IBusControl>();
            var busHandle = TaskUtil.Await(() => bus.StartAsync());
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

            builder.Register(c =>
            {
                return Bus.Factory.CreateUsingRabbitMq(sbc =>
                    sbc.Host("localhost", "/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    })
                );
            })
                .As<IBusControl>()
                .As<IBus>()
                .As<IPublishEndpoint>()
                .SingleInstance();
            base.Load(builder);
        }
    }
}
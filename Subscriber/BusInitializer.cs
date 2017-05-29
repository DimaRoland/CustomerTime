using System;
using System.ComponentModel;
using System.Configuration;
using Autofac;
using MassTransit;
using MassTransit.BusConfigurators;
using MassTransit.Log4NetIntegration.Logging;

namespace Subscriber
{
    public static class ServiceBusConfiguration
    {
        private static IBusControl ConfigureServiceBus(Container container)
        {
            var appSettings = ConfigurationManager.AppSettings;
            var queueUri = new Uri(appSettings["RabbitMQHost"]);
            var userName = appSettings["UserName"];
            var password = appSettings["Password"];

            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.UseSerilog();
                cfg.UseJsonSerializer();

                var host = cfg.Host(queueUri, h =>
                {
                    h.Heartbeat(5);
                    h.Username(userName);
                    h.Password(password);
                });

                cfg.ReceiveEndpoint(host, "internal", e => e.Consumer(() => new MainEventConsumer(container)));

                cfg.PurgeOnStartup = true;
            });

            return busControl;
        }

        public static void Configure(Container container)
        {
            var busControl = ConfigureServiceBus(container);
            busControl.Start();
        }
    }
}

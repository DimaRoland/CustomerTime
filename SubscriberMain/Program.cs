using System;
using MassTransit;

namespace SubscriberMain
{
    public class MyMessage
    {
        public string Value { get; set; }
    }

    class Program
    {
        static void Main()
        {
            var bus = BusInitializer.CreateBus();

            bus.Start();

                bus.Publish(new MyMessage { Value = "Hello, World." });

                Console.ReadLine();
        }
    }

    public class BusInitializer
    {
        public static IBusControl CreateBus()
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://localhost/"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                sbc.ReceiveEndpoint(host, "my_queue", endpoint =>
                {
                    endpoint.Handler<MyMessage>(async context =>
                    {
                        await Console.Out.WriteLineAsync($"Received: {context.Message.Value}");
                    });
                });
            });

            return bus;
        }
    }
}
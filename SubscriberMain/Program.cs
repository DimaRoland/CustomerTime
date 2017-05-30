using System;
using System.Threading.Tasks;
using MassTransit;

namespace SubscriberMain
{
    public class MyMessage
    {
        public string Value { get; set; }
    }

    public class AddUserConsumer : IConsumer<MyMessage>
    {
        public Task Consume(ConsumeContext<MyMessage> context)
        {
            Console.WriteLine($"User  {context.Message.Value} task");
            return Task.CompletedTask;
        }
    }

    class Program
    {
        static void Main()
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://localhost/"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
                cfg.ReceiveEndpoint(host, "AddUser1", e =>
                {
                    e.Consumer<AddUserConsumer>();
                });
            });

            busControl.StartAsync();
            Console.ReadKey();
        }
    }

}
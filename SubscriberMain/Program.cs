using System;
using CustomerTimesTask;
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
            var bus = BusInitializer.CreateBus("SubscriberMain", x =>
            {
                x.Subscribe(subs =>
                {
                    subs.Consumer<SomethingHappenedConsumer>().Permanent();
                });
            });

            Console.ReadKey();

            bus.Dispose();
        }
    }

    class SomethingHappenedConsumer : Consumes<SomethingHappened>.Context
    {
        public void Consume(IConsumeContext<SomethingHappened> message)
        {
            Console.Write("User" + message.Message.What);
        }
    }

    public interface SomethingHappened
    {
        string What { get; }
    }
}
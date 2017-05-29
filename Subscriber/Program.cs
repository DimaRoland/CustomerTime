using System;
using MassTransit;

namespace Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            Publish.Pub();
            var bus = BusInitializer.CreateBus("TestSubscriber", x =>
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

    public class Publish
    {
        public static void Pub()
        {
            var bus = BusInitializer.CreateBus("TestPublisher", x => { });
            string text = "Test";

            while (text != "quit")
            {
                Console.Write("Enter a message: ");
                text = Console.ReadLine();

                var message = new SomethingHappenedMessage() { What = text };
                bus.Publish<SomethingHappened>(message, x => { });
            }

            bus.Dispose();
        }
    }
}

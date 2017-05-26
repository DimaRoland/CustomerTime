using MassTransit;
using System;
using System.Security.Policy;
using Magnum.Parsers;

namespace Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {

            var bus2 = BusInitializer.CreateBus("TestPublisher", x => { });
            string text = "";

            while (text != "quit")
            {
                Console.Write("Enter a message: ");
                text = Console.ReadLine();

                var message = new SomethingHappenedMessage() { What = text, When = DateTime.Now };
                bus2.Publish<SomethingHappened>(message, x => { });
            }

            bus2.Dispose();

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
}
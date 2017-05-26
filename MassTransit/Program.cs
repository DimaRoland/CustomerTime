using System;
using Configuration;
using Contracts;

namespace MassTransit
{
    public class Program
    {
        static void Main(string[] args)
        {
            var bus = BusInitializer.CreateBus("TestPublisher", x => { });
            var text = "";

            while (text != "quit")
            {
                Console.Write("Enter a message: ");
                text = Console.ReadLine();

                var message = new SomethingHappenedMessage() { What = text, When = DateTime.Now };
                bus.Publish<SomethingHappened>(message, x => { x.SetDeliveryMode(MassTransit.DeliveryMode.Persistent); });
            }

            bus.Dispose();
        }
    }
}
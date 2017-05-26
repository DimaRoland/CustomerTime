using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Subscriber;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            var bus = BusInitializer.CreateBus("TestPublisher", x => { });
            string text = "";

            while (text != "quit")
            {
                Console.Write("Enter a message: ");
                text = Console.ReadLine();

                var message = new SomethingHappenedMessage() { What = "Add", Who = "vasya", Task = "Custom" };
                bus.Publish<SomethingHappened>(message, x => { x.SetDeliveryMode(MassTransit.DeliveryMode.Persistent); });
            }

            bus.Dispose();
        }
    }
}

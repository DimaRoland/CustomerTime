using System;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
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
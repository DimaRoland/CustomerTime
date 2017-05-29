using System;
using MassTransit;

namespace Subscriber
{
    class SomethingHappenedConsumer : Consumes<SomethingHappened>.Context
    {
        public void Consume(IConsumeContext<SomethingHappened> message)
        {
            Console.Write("");
        }
    }

    public interface SomethingHappened
    {
        string What { get; }
    }

    class SomethingHappenedMessage : SomethingHappened
    {
        public string What { get; set; }
    }
}

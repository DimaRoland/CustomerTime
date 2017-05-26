using System;
using Contracts;

namespace MassTransit
{
    public class SomethingHappenedMessage : SomethingHappened
    {
        public string What { get; set; }
        public DateTime When { get; set; }
    }
}
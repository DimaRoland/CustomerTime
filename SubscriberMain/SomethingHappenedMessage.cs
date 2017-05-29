using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerTimesTask
{
    public class SomethingHappenedMessage : SomethingHappened
    {
        public string What { get; set; }
    }

    public interface SomethingHappened
    {
        string What { get; }
    }
}
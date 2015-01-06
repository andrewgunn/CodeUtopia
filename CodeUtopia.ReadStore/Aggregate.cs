using System;

namespace CodeUtopia.ReadStore
{
    public class Aggregate
    {
        public Guid AggregateId { get; set; }

        public int AggregateVersionNumber { get; set; }
    }
}
using System;

namespace Library.Frontend.ReadStore.Aggregate
{
    public class AggregateEntity
    {
        public Guid AggregateId { get; set; }

        public int AggregateVersionNumber { get; set; }
    }
}
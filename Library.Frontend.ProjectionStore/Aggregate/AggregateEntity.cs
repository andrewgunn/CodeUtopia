using System;

namespace Library.Frontend.ProjectionStore.Aggregate
{
    public class AggregateEntity
    {
        public Guid AggregateId { get; set; }

        public int AggregateVersionNumber { get; set; }
    }
}
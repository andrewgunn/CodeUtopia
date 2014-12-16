using System;

namespace Library.Frontend.ProjectionStore.Aggregate
{
    public class AggregateEntity
    {
        public Guid AggregateId { get; set; }

        public int VersionNumber { get; set; }
    }
}
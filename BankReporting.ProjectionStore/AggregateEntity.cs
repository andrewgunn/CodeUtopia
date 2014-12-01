using System;

namespace BankReporting.ProjectionStore
{
    public class AggregateEntity
    {
        public Guid AggregateId { get; set; }

        public int VersionNumber { get; set; }
    }
}
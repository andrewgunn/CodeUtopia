using System;

namespace BankReporting.ProjectionStore.EntityFramework
{
    public class AggregateEntity 
    {
        public Guid AggregateId { get; set; }

        public int VersionNumber { get; set; }
    }
}
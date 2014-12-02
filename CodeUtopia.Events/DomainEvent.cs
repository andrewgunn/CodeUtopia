using System;

namespace CodeUtopia.Events
{
    [Serializable]
    public abstract class DomainEvent : IDomainEvent
    {
        protected DomainEvent(Guid aggregateId, int versionNumber)
        {
            AggregateId = aggregateId;
            VersionNumber = versionNumber;
        }

        public Guid AggregateId { get; set; }

        public int VersionNumber { get; set; }
    }
}
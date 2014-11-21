using System;

namespace CodeUtopia.Event
{
    public abstract class DomainEvent : IDomainEvent
    {
        protected DomainEvent(Guid aggregateId, int versionNumber)
        {
            _aggregateId = aggregateId;
            _versionNumber = versionNumber;
        }

        public Guid AggregateId
        {
            get
            {
                return _aggregateId;
            }
        }

        int IDomainEvent.VersionNumber
        {
            get
            {
                return _versionNumber;
            }
        }

        private readonly Guid _aggregateId;

        private readonly int _versionNumber;
    }
}
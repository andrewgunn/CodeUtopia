using System;

namespace CodeUtopia.Events
{
    [Serializable]
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

        public int VersionNumber
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
using System;

namespace CodeUtopia.Events
{
    [Serializable]
    public abstract class DomainEvent : IDomainEvent
    {
        protected DomainEvent(Guid aggregateId, int aggregateVersionNumber)
        {
            _aggregateId = aggregateId;
            _aggregateVersionNumber = aggregateVersionNumber;
        }

        public Guid AggregateId
        {
            get
            {
                return _aggregateId;
            }
        }

        public int AggregateVersionNumber
        {
            get
            {
                return _aggregateVersionNumber;
            }
        }

        private readonly Guid _aggregateId;

        private readonly int _aggregateVersionNumber;
    }
}
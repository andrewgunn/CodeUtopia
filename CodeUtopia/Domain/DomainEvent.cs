using System;

namespace CodeUtopia.Domain
{
    public abstract class DomainEvent : IDomainEvent
    {
        protected DomainEvent(Guid aggregateId, IVersionNumberProvider versionNumberProvider)
        {
            _aggregateId = aggregateId;
            _versionNumber = versionNumberProvider.GetNextVersionNumber();
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
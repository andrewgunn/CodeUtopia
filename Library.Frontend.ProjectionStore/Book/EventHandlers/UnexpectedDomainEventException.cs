using System;

namespace Library.Frontend.ProjectionStore.Book.EventHandlers
{
    public class UnexpectedDomainEventException : Exception
    {
        public UnexpectedDomainEventException(Guid aggregateId,
                                              int aggregateVersionNumber,
                                              int currentAggregateVersionNumber)
        {
            _aggregateId = aggregateId;
            _aggregateVersionNumber = aggregateVersionNumber;
            _currentAggregateVersionNumber = currentAggregateVersionNumber;
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

        public int CurrentAggregateVersionNumber
        {
            get
            {
                return _currentAggregateVersionNumber;
            }
        }

        private readonly Guid _aggregateId;

        private readonly int _aggregateVersionNumber;

        private readonly int _currentAggregateVersionNumber;
    }
}
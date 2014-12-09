using System;

namespace CodeUtopia.EventStore.EntityFramework
{
    public class AggregateConcurrencyViolationException : Exception
    {
        public AggregateConcurrencyViolationException(Guid aggregateId)
        {
            _aggregateId = aggregateId;
        }

        public Guid AggregateId
        {
            get
            {
                return _aggregateId;
            }
        }

        private readonly Guid _aggregateId;
    }
}
using System;

namespace CodeUtopia.Domain
{
    public class AggregateAlreadyInitialisedException : Exception
    {
        public AggregateAlreadyInitialisedException(Guid aggregateId)
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
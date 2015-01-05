using System;

namespace CodeUtopia.Domain
{
    public class AggregateAlreadyInitializedException : Exception
    {
        public AggregateAlreadyInitializedException(Guid aggregateId)
            : base("The aggregate has already been initialised.")
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
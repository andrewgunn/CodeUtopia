using System;

namespace CodeUtopia.EventStore.EntityFramework
{
    public class DomainEventsCannotBeDeletedException : Exception
    {
        public DomainEventsCannotBeDeletedException(DomainEventEntity domainEvent)
        {
            _domainEvent = domainEvent;
        }

        public DomainEventEntity DomainEvent
        {
            get
            {
                return _domainEvent;
            }
        }

        private readonly DomainEventEntity _domainEvent;
    }
}
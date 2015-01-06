using System;

namespace CodeUtopia.WriteStore.EntityFramework
{
    public class DomainEventsCannotBeUpdatedException : Exception
    {
        public DomainEventsCannotBeUpdatedException(DomainEventEntity domainEvent)
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
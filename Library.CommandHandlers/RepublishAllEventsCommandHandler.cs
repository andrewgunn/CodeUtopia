using System.Collections.Generic;
using System.Linq;
using CodeUtopia.Events;
using CodeUtopia.EventStore;
using Library.Commands;
using Library.Commands.v1;
using NServiceBus;

namespace Library.CommandHandlers
{
    public class RepublishAllEventsCommandHandler : IHandleMessages<RepublishAllEventsCommand>
    {
        public RepublishAllEventsCommandHandler(IEventStorage eventStorage, IBus bus)
        {
            _eventStorage = eventStorage;
            _bus = bus;
        }

        public void Handle(RepublishAllEventsCommand command)
        {
            var skip = 0;
            const int take = 10;

            IReadOnlyCollection<IDomainEvent> events;

            while ((events = _eventStorage.GetEvents(skip, take)).Any())
            {
                foreach (var domainEvent in events)
                {
                    _bus.Publish(domainEvent);
                }

                skip += take;
            }
        }

        private readonly IBus _bus;

        private readonly IEventStorage _eventStorage;
    }
}
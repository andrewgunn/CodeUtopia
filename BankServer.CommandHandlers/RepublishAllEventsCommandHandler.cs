using System.Collections.Generic;
using System.Linq;
using BankServer.Commands.v1;
using CodeUtopia;
using CodeUtopia.Events;
using CodeUtopia.EventStore;
using CodeUtopia.Messaging;

namespace BankServer.CommandHandlers
{
    public class RepublishAllEventsCommandHandler : ICommandHandler<RepublishAllEventsCommand>
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

            while ((events = _eventStorage.GetAll(skip, take)).Any())
            {
                foreach (var domainEvent in events)
                {
                    _bus.Publish(domainEvent);
                }

                _bus.Commit();

                skip += take;
            }
        }

        private readonly IBus _bus;

        private readonly IEventStorage _eventStorage;
    }
}
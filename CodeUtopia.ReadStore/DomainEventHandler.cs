using CodeUtopia.Messages;
using NServiceBus;

namespace CodeUtopia.ReadStore
{
    public class DomainEventHandler : IHandleMessages<IDomainEvent>
    {
        public DomainEventHandler(IReadStoreRepository readStoreRepository, IBus bus)
        {
            _readStoreRepository = readStoreRepository;
            _bus = bus;
        }

        public void Handle(IDomainEvent domainEvent)
        {
            var aggregate = _readStoreRepository.GetAggregate(domainEvent.AggregateId);

            if (aggregate == null)
            {
                aggregate = new Aggregate
                            {
                                AggregateId = domainEvent.AggregateId
                            };
                
                _readStoreRepository.SaveAggregate(aggregate);
            }
            else if (domainEvent.AggregateVersionNumber <= aggregate.AggregateVersionNumber)
            {
                // We've seen this message, ignore it.
                _bus.DoNotContinueDispatchingCurrentMessageToHandlers();

                return;
            }
            else if (domainEvent.AggregateVersionNumber != aggregate.AggregateVersionNumber + 1)
            {
                throw new UnexpectedDomainEventException(domainEvent.AggregateId,
                                                            domainEvent.AggregateVersionNumber,
                                                            aggregate.AggregateVersionNumber);
            }

            aggregate.AggregateVersionNumber = domainEvent.AggregateVersionNumber;

            _readStoreRepository.Commit();
        }

        private readonly IReadStoreRepository _readStoreRepository;

        private readonly IBus _bus;
    }
}
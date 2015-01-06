using System;
using System.Collections.Generic;
using System.Linq;
using CodeUtopia.Domain;
using NServiceBus;

namespace CodeUtopia.WriteStore
{
    public class WriteStoreAggregateRepository : IAggregateRepository
    {
        public WriteStoreAggregateRepository(IEventStorage eventStorage, IBus bus)
        {
            _eventStorage = eventStorage;
            _bus = bus;

            _aggregates = new List<IAggregate>();
        }

        public void Add<TAggregate>(TAggregate aggregate) where TAggregate : class, IAggregate, new()
        {
            _aggregates.Add(aggregate);
        }

        public void Commit()
        {
            foreach (var aggregate in _aggregates)
            {
                var domainEvents = aggregate.GetChanges();

                if (domainEvents != null && domainEvents.Any())
                {
                    _eventStorage.SaveEvents(domainEvents);
                }

                foreach (var domainEvent in aggregate.GetChanges())
                {
                    _bus.Publish(domainEvent);
                }

                aggregate.ClearChanges();
            }

            _aggregates.Clear();

            _eventStorage.Commit();
        }

        public TAggregate Get<TAggregate>(Guid aggregateId) where TAggregate : class, IAggregate, new()
        {
            var aggregate = LoadFromHistory<TAggregate>(aggregateId);

            Add(aggregate);

            return aggregate;
        }

        private TAggregate LoadFromHistory<TAggregate>(Guid aggregateId) where TAggregate : class, IAggregate, new()
        {
            var aggregate = new TAggregate();
            var originator = aggregate as IOriginator;

            if (originator == null)
            {
                aggregate.LoadFromHistory(_eventStorage.GetEventsForAggregate(aggregateId));
            }
            else
            {
                var snapshot = _eventStorage.GetLastSnapshotForAggregate(aggregateId);

                if (snapshot != null)
                {
                    originator.LoadFromMemento(snapshot.AggregateId, snapshot.AggregateVersionNumber, snapshot.Memento);
                }

                var domainEvents = _eventStorage.GetEventsForAggregateSinceLastSnapshot(aggregateId);

                aggregate.LoadFromHistory(_eventStorage.GetEventsForAggregate(aggregateId));

                if (domainEvents.Count == 10 /* TODO Make this value configurable. */)
                {
                    _eventStorage.SaveSnapshotForAggregate(aggregateId,
                                                           domainEvents.OrderBy(x => x.AggregateVersionNumber)
                                                                       .Last()
                                                                       .AggregateVersionNumber,
                                                           originator.CreateMemento());
                }
            }

            return aggregate;
        }

        public void Rollback()
        {
            _aggregates.Clear();

            _eventStorage.Rollback();

            // TODO: Rollback _bus
        }

        private readonly List<IAggregate> _aggregates;

        private readonly IBus _bus;

        private readonly IEventStorage _eventStorage;
    }
}
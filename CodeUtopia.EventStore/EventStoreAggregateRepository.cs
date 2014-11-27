using System;
using System.Collections.Generic;
using System.Linq;
using CodeUtopia.Domain;
using CodeUtopia.Messaging;

namespace CodeUtopia.EventStore
{
    public class EventStoreAggregateRepository : IAggregateRepository
    {
        public EventStoreAggregateRepository(IEventStorage eventStorage, IBus bus)
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
                _eventStorage.SaveChanges(aggregate);

                foreach (var domainEvent in aggregate.GetChanges())
                {
                    _bus.Publish(domainEvent);
                }

                aggregate.ClearChanges();
            }

            _aggregates.Clear();

            _eventStorage.Commit();

            _bus.Commit();
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

            if (originator != null)
            {
                LoadFromLastSnapshotIfExists(aggregateId, originator);
            }

            LoadFromHistorySinceLastSnapshot(aggregateId, aggregate);

            return aggregate;
        }

        private void LoadFromHistorySinceLastSnapshot(Guid aggregateId, IAggregate aggregate)
        {
            var domainEvents = _eventStorage.GetAllSinceLastSnapshot(aggregateId);

            if (!domainEvents.Any())
            {
                domainEvents = _eventStorage.GetAll(aggregateId);
            }

            aggregate.LoadFromHistory(domainEvents);
        }

        private void LoadFromLastSnapshotIfExists(Guid aggregateId, IOriginator aggregate)
        {
            var snapshot = _eventStorage.GetLastSnapshot(aggregateId);

            if (snapshot == null)
            {
                return;
            }

            aggregate.LoadFromMemento(snapshot.AggregateId, snapshot.VersionNumber, snapshot.Memento);
        }

        public void Rollback()
        {
            _aggregates.Clear();

            _eventStorage.Rollback();

            _bus.Rollback();
        }

        private readonly List<IAggregate> _aggregates;

        private readonly IBus _bus;

        private readonly IEventStorage _eventStorage;
    }
}
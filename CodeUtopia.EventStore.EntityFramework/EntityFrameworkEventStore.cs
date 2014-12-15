using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using CodeUtopia.Events;

namespace CodeUtopia.EventStore.EntityFramework
{
    public class EntityFrameworkEventStore : IEventStorage
    {
        public EntityFrameworkEventStore(string nameOrConnectionString, IFormatter formatter)
        {
            _nameOrConnectionString = nameOrConnectionString;
            _formatter = formatter;

            _databaseContext = new EventStoreContext(nameOrConnectionString);
        }

        public void Commit()
        {
            _databaseContext.SaveChanges();
        }

        private TType Deserialize<TType>(byte[] bytes)
        {
            using (var memoryStream = new MemoryStream(bytes))
            {
                return (TType)_formatter.Deserialize(memoryStream);
            }
        }

        public IReadOnlyCollection<IDomainEvent> GetEventsForAggregate(Guid aggregateId)
        {
            return _databaseContext.DomainEvents.Where(x => x.AggregateId == aggregateId)
                                   .OrderBy(x => x.AggregateVersionNumber)
                                   .ToList()
                                   .Select(x => Deserialize<IDomainEvent>(x.Data))
                                   .ToList();
        }

        public IReadOnlyCollection<IDomainEvent> GetEvents(int skip, int take)
        {
            return _databaseContext.DomainEvents.OrderBy(x => x.AggregateId)
                                   .ThenBy(x => x.AggregateVersionNumber)
                                   .Skip(skip)
                                   .Take(take)
                                   .ToList()
                                   .Select(x => Deserialize<IDomainEvent>(x.Data))
                                   .ToList();
        }

        public IReadOnlyCollection<IDomainEvent> GetEventsForAggregateSinceLastSnapshot(Guid aggregateId)
        {
            var snapshot = GetLastSnapshotForAggregate(aggregateId);

            var snapshotVersion = snapshot == null ? -1 : snapshot.AggregateVersionNumber;

            return
                _databaseContext.DomainEvents.Where(
                                                    x =>
                                                    x.AggregateId == aggregateId &&
                                                    x.AggregateVersionNumber > snapshotVersion)
                                .OrderBy(x => x.AggregateVersionNumber)
                                .ToList()
                                .Select(x => Deserialize<IDomainEvent>(x.Data))
                                .ToList();
        }

        public ISnapshot GetLastSnapshotForAggregate(Guid aggregateId)
        {
            var snapshot = _databaseContext.Snapshots.OrderByDescending(x => x.AggregateVersionNumber)
                                           .FirstOrDefault(x => x.AggregateId == aggregateId);

            return snapshot == null ? null : Deserialize<ISnapshot>(snapshot.Data);
        }

        public void Rollback()
        {
            _databaseContext = new EventStoreContext(_nameOrConnectionString);
        }

        public void SaveEvents(IReadOnlyCollection<IDomainEvent> domainEvents)
        {
            if (domainEvents == null || !domainEvents.Any())
            {
                return;
            }

            foreach (var domainEvent in domainEvents.OrderBy(x => x.AggregateVersionNumber)
                                                    .ToList())
            {
                _databaseContext.DomainEvents.Add(new DomainEventEntity
                                                  {
                                                      AggregateId = domainEvent.AggregateId,
                                                      AggregateVersionNumber = domainEvent.AggregateVersionNumber,
                                                      DomainEventType = domainEvent.GetType()
                                                                                   .FullName,
                                                      Data = Serialize(domainEvent),
                                                  });
            }
        }

        public void SaveSnapshotForAggregate(Guid aggregateId, int aggregateVersionNumber, object memento)
        {
            _databaseContext.Snapshots.Add(new SnapshotEntity
                                           {
                                               AggregateId = aggregateId,
                                               AggregateVersionNumber = aggregateVersionNumber,
                                               Data = Serialize(memento)
                                           });
        }

        private byte[] Serialize(object obj)
        {
            using (var memoryStream = new MemoryStream())
            {
                _formatter.Serialize(memoryStream, obj);

                return memoryStream.ToArray();
            }
        }

        private EventStoreContext _databaseContext;

        private readonly IFormatter _formatter;

        private readonly string _nameOrConnectionString;
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using CodeUtopia.Domain;
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

        private int GetAggregateVersionNumber(Guid aggregateId)
        {
            var versionNumber = _databaseContext.DomainEvents.Where(x => x.AggregateId == aggregateId)
                                                .OrderByDescending(x => x.VersionNumber)
                                                .Select(x => x.VersionNumber)
                                                .FirstOrDefault();

            return versionNumber;
        }

        public IReadOnlyCollection<IDomainEvent> GetAll(Guid aggregateId)
        {
            return _databaseContext.DomainEvents.Where(x => x.AggregateId == aggregateId)
                                   .OrderBy(x => x.VersionNumber)
                                   .ToList()
                                   .Select(x => Deserialize<IDomainEvent>(x.Data))
                                   .ToList();
        }

        public IReadOnlyCollection<IDomainEvent> GetAll(int skip, int take)
        {
            return _databaseContext.DomainEvents.OrderBy(x => x.AggregateId)
                                   .ThenBy(x => x.VersionNumber)
                                   .Skip(skip)
                                   .Take(take)
                                   .ToList()
                                   .Select(x => Deserialize<IDomainEvent>(x.Data))
                                   .ToList();
        }

        public IReadOnlyCollection<IDomainEvent> GetAllSinceLastSnapshot(Guid aggregateId)
        {
            var snapshot = GetLastSnapshot(aggregateId);

            var snapshotVersion = snapshot == null ? -1 : snapshot.VersionNumber;

            return
                _databaseContext.DomainEvents.Where(
                                                    x =>
                                                    x.AggregateId == aggregateId && x.VersionNumber > snapshotVersion)
                                .OrderBy(x => x.VersionNumber)
                                .ToList()
                                .Select(x => Deserialize<IDomainEvent>(x.Data))
                                .ToList();
        }

        public ISnapshot GetLastSnapshot(Guid aggregateId)
        {
            var snapshot = _databaseContext.Snapshots.OrderByDescending(x => x.VersionNumber)
                                           .FirstOrDefault(x => x.AggregateId == aggregateId);

            return snapshot == null ? null : Deserialize<ISnapshot>(snapshot.Data);
        }

        public void Rollback()
        {
            _databaseContext = new EventStoreContext(_nameOrConnectionString);
        }

        public void SaveChanges(IAggregate aggregate)
        {
            var versionNumber = GetAggregateVersionNumber(aggregate.AggregateId);

            if (versionNumber != aggregate.VersionNumber)
            {
                throw new ConcurrencyViolationException();
            }

            var domainEvents = aggregate.GetChanges()
                                        .OrderBy(x => x.VersionNumber);

            if (!domainEvents.Any())
            {
                return;
            }

            foreach (var domainEvent in domainEvents)
            {
                _databaseContext.DomainEvents.Add(new DomainEventEntity
                                                  {
                                                      AggregateId = aggregate.AggregateId,
                                                      AggregateType = aggregate.GetType()
                                                                               .FullName,
                                                      DomainEventType = domainEvent.GetType()
                                                                                   .FullName,
                                                      VersionNumber = domainEvent.VersionNumber,
                                                      Data = Serialize(domainEvent),
                                                  });
            }

            aggregate.UpdateVersionNumber(domainEvents.Last()
                                                      .VersionNumber);
        }

        public void SaveSnapshot<TAggregate>(TAggregate aggregate) where TAggregate : IAggregate, IOriginator
        {
            _databaseContext.Snapshots.Add(new SnapshotEntity
                                           {
                                               AggregateId = aggregate.AggregateId,
                                               VersionNumber = aggregate.VersionNumber,
                                               Data = Serialize(aggregate.CreateMemento())
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
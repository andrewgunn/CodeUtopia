using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace CodeUtopia.EventStore.EntityFramework
{
    public class EventStoreContext : DbContext
    {
        public EventStoreContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            Configuration.LazyLoadingEnabled = false;
        }

        private int GetAggregateVersionNumber(Guid aggregateId)
        {
            return DomainEvents.Where(x => x.AggregateId == aggregateId)
                               .OrderByDescending(x => x.AggregateVersionNumber)
                               .Select(x => x.AggregateVersionNumber)
                               .FirstOrDefault();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new DomainEventEntityConfiguration());
            modelBuilder.Configurations.Add(new SnapshotEntityConfiguration());
        }

        public override int SaveChanges()
        {
            var exceptions = new List<Exception>();

            foreach (var deletedDomainEvent in ChangeTracker.Entries<DomainEventEntity>()
                                                            .Where(x => x.State == EntityState.Deleted)
                                                            .Select(x => x.Entity)
                                                            .ToList())
            {
                exceptions.Add(new DomainEventsCannotBeDeletedException(deletedDomainEvent));
            }

            foreach (var updatedDomainEvent in ChangeTracker.Entries<DomainEventEntity>()
                                                            .Where(x => x.State == EntityState.Modified)
                                                            .Select(x => x.Entity)
                                                            .ToList())
            {
                exceptions.Add(new DomainEventsCannotBeUpdatedException(updatedDomainEvent));
            }

            var createdDomainEvents = ChangeTracker.Entries<DomainEventEntity>()
                                                   .Where(x => x.State == EntityState.Added)
                                                   .Select(x => x.Entity)
                                                   .ToList();
            var aggregateIds = createdDomainEvents.Select(x => x.AggregateId)
                                                  .Distinct();

            foreach (var aggregateId in aggregateIds)
            {
                var aggregateVersionNumber = GetAggregateVersionNumber(aggregateId);

                if (aggregateVersionNumber != createdDomainEvents.OrderBy(x => x.AggregateVersionNumber)
                                                                 .Select(x => x.AggregateVersionNumber)
                                                                 .First() - 1)
                {
                    exceptions.Add(new AggregateConcurrencyViolationException(aggregateId));
                }
            }

            return base.SaveChanges();
        }

        public IDbSet<DomainEventEntity> DomainEvents { get; set; }

        public IDbSet<SnapshotEntity> Snapshots { get; set; }
    }
}
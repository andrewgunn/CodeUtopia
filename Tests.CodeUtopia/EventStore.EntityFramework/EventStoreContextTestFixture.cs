using System;
using System.Collections.Generic;
using CodeUtopia.EventStore.EntityFramework;

namespace Tests.CodeUtopia.EventStore.EntityFramework
{
    [Specification]
    public abstract class EventStoreContextTestFixture
    {
        protected virtual void Finally()
        {
        }

        protected virtual IReadOnlyCollection<DomainEventEntity> GivenDomainEvents()
        {
            return new DomainEventEntity[0];
        }

        protected virtual IReadOnlyCollection<SnapshotEntity> GivenSnapshots()
        {
            return new SnapshotEntity[0];
        }

        [Given]
        public void Setup()
        {
            try
            {
                using (var databaseContext = new EventStoreContext("EventStore"))
                {
                    foreach (var domainEvent in GivenDomainEvents())
                    {
                        databaseContext.DomainEvents.Add(domainEvent);
                    }

                    foreach (var snapshot in GivenSnapshots())
                    {
                        databaseContext.Snapshots.Add(snapshot);
                    }

                    databaseContext.SaveChanges();
                }

                When();
            }
            catch (Exception exception)
            {
                Exception = exception;
            }
            finally
            {
                Finally();
            }
        }

        protected abstract void When();

        protected Exception Exception { get; private set; }
    }
}
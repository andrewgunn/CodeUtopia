using System;
using System.Collections.Generic;
using CodeUtopia.Domain;
using CodeUtopia.Messages;

namespace Tests.CodeUtopia.Domain
{
    [Specification]
    public abstract class AggregateTestFixture<TAggregate>
        where TAggregate : IAggregate, new()
    {
        protected virtual void Finally()
        {
        }

        protected virtual IReadOnlyCollection<IDomainEvent> GivenEvents()
        {
            return new List<IDomainEvent>();
        }

        [Given]
        public void Setup()
        {
            Aggregate = new TAggregate();
            Changes = new List<IDomainEvent>();

            try
            {
                Aggregate.LoadFromHistory(GivenEvents());
                When();
                Changes = Aggregate.GetChanges();
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

        protected TAggregate Aggregate { get; private set; }

        protected IReadOnlyCollection<IDomainEvent> Changes { get; private set; }

        protected Exception Exception { get; private set; }
    }
}
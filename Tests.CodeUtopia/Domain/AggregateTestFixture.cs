using System;
using System.Collections.Generic;
using CodeUtopia.Domain;
using CodeUtopia.Events;

namespace Tests.CodeUtopia.Domain
{
    [Specification]
    public abstract class AggregateTestFixture<TAggregate>
        where TAggregate : IAggregate, new()
    {
        protected virtual void Finally()
        {
        }

        protected virtual IReadOnlyCollection<IDomainEvent> Given()
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
                Aggregate.LoadFromHistory(Given());
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

        protected TAggregate Aggregate;

        protected IReadOnlyCollection<IDomainEvent> Changes;

        protected Exception Exception;
    }
}
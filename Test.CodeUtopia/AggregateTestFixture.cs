using System;
using System.Collections.Generic;
using CodeUtopia.Domain;

namespace Test.CodeUtopia
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
            Events = new List<IDomainEvent>();

            try
            {
                Aggregate.LoadFromHistory(Given());
                When();
                Events = Aggregate.GetChanges();
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

        protected IReadOnlyCollection<IDomainEvent> Events;

        protected Exception Exception;
    }
}
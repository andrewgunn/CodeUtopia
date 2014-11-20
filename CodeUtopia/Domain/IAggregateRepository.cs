using System;

namespace CodeUtopia.Domain
{
    /// <summary>
    ///     Store aggregates.
    /// </summary>
    public interface IAggregateRepository : IUnitOfWork
    {
        TAggregate Get<TAggregate>(Guid aggregateId) where TAggregate : class, IAggregate, IOriginator, new();

        void RegisterForTracking<TAggregate>(TAggregate aggregate)
            where TAggregate : class, IAggregate, IOriginator, new();
    }
}
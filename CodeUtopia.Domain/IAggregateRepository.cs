using System;

namespace CodeUtopia.Domain
{
    /// <summary>
    ///     Store aggregates.
    /// </summary>
    public interface IAggregateRepository : IUnitOfWork
    {
        void Add<TAggregate>(TAggregate aggregate) where TAggregate : class, IAggregate, new();

        TAggregate Get<TAggregate>(Guid aggregateId) where TAggregate : class, IAggregate, new();
    }
}
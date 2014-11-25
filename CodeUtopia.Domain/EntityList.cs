using System.Collections.Generic;

namespace CodeUtopia.Domain
{
    public class EntityList<TEntity> : List<TEntity>
        where TEntity : IEntity
    {
        public EntityList(IEntityTracker aggregate)
        {
            _aggregate = aggregate;
        }

        public EntityList(IEntityTracker aggregate, int capacity)
            : base(capacity)
        {
            _aggregate = aggregate;
        }

        public EntityList(IEntityTracker aggregate, IEnumerable<TEntity> collection)
            : base(collection)
        {
            _aggregate = aggregate;
        }

        public new void Add(TEntity entity)
        {
            _aggregate.RegisterForTracking(entity);

            base.Add(entity);
        }

        private readonly IEntityTracker _aggregate;
    }
}
using System.Collections.Generic;

namespace CodeUtopia.Domain
{
    public class EntityList<TEntity> : List<TEntity>
        where TEntity : IEntity
    {
        public EntityList(IAggregate aggregate)
        {
            _aggregate = aggregate;
        }

        public EntityList(IAggregate aggregate, int capacity)
            : base(capacity)
        {
            _aggregate = aggregate;
        }

        public EntityList(IAggregate aggregate, IEnumerable<TEntity> collection)
            : base(collection)
        {
            _aggregate = aggregate;
        }

        public new void Add(TEntity entity)
        {
            _aggregate.RegisterEntity(entity);

            base.Add(entity);
        }

        private readonly IAggregate _aggregate;
    }
}
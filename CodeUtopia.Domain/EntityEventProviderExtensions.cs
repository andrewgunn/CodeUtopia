using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeUtopia.Domain
{
    public static class EntityExtensions
    {
        public static bool TryGetValue<TEntity>(this IEnumerable<TEntity> entities, Guid entityId, out TEntity entity)
            where TEntity : IEntity
        {
            entity = entities.FirstOrDefault(x => x.EntityId == entityId);

            return entity != null;
        }
    }
}
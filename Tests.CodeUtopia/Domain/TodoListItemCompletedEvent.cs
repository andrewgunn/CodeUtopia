using System;
using CodeUtopia.Events;

namespace Tests.CodeUtopia.Domain
{
    public class TodoListItemCompletedEvent : EntityEvent
    {
        public TodoListItemCompletedEvent(Guid todoListId, int todoListVersionNumber, Guid todoListItemId)
            : base(todoListId, todoListVersionNumber, todoListItemId)
        {
        }

        public Guid TodoListId
        {
            get
            {
                return ((IEntityEvent)this).AggregateId;
            }
        }

        public Guid TodoListItemId
        {
            get
            {
                return ((IEntityEvent)this).EntityId;
            }
        }
    }
}
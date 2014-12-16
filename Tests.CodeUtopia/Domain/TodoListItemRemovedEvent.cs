using System;
using CodeUtopia.Events;

namespace Tests.CodeUtopia.Domain
{
    internal class TodoListItemRemovedEvent : DomainEvent
    {
        public TodoListItemRemovedEvent(Guid todoListId, int todoListVersionNumber, Guid todoListItemId)
            : base(todoListId, todoListVersionNumber)
        {
            _todoListItemId = todoListItemId;
        }

        public Guid TodoListId
        {
            get
            {
                return ((IDomainEvent)this).AggregateId;
            }
        }

        public Guid TodoListItemId
        {
            get
            {
                return _todoListItemId;
            }
        }

        private readonly Guid _todoListItemId;
    }
}
using System;
using CodeUtopia.Events;

namespace Tests.CodeUtopia.Domain
{
    internal class TodoListItemAddedEvent : DomainEvent
    {
        public TodoListItemAddedEvent(Guid todoListId,
                                      int todoListVersionNumber,
                                      Guid todoListItemId,
                                      string description)
            : base(todoListId, todoListVersionNumber)
        {
            _todoListItemId = todoListItemId;
            _description = description;
        }

        public string Description
        {
            get
            {
                return _description;
            }
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

        private readonly string _description;

        private readonly Guid _todoListItemId;
    }
}
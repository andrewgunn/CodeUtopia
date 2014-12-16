using System;
using CodeUtopia.Events;

namespace Tests.CodeUtopia.Domain
{
    public class TodoListCreatedEvent : DomainEvent
    {
        public TodoListCreatedEvent(Guid todoListId, int todoListVersionNumber, string name)
            : base(todoListId, todoListVersionNumber)
        {
            _name = name;
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public Guid TodoListId
        {
            get
            {
                return ((IDomainEvent)this).AggregateId;
            }
        }

        private readonly string _name;
    }
}
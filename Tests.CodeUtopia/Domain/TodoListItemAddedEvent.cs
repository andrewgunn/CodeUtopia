using System;
using CodeUtopia.Events;

namespace Tests.CodeUtopia.Domain
{
    internal class TodoListItemAddedEvent : IDomainEvent
    {

        public string Description
        {
            get;
            set;
        }

        public Guid TodoListItemId
        {
            get;
            set;
        }

        public Guid AggregateId { get; set; }

        public int AggregateVersionNumber { get; set; }
    }
}
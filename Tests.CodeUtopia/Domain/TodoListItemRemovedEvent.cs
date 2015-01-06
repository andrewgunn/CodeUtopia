using System;
using CodeUtopia.Messages;

namespace Tests.CodeUtopia.Domain
{
    internal class TodoListItemRemovedEvent : IDomainEvent
    {
        public Guid AggregateId { get; set; }

        public int AggregateVersionNumber { get; set; }

        public Guid TodoListItemId { get; set; }
    }
}
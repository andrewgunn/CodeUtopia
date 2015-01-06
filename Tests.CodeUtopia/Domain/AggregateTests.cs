using System;
using System.Collections.Generic;
using CodeUtopia.Domain;
using CodeUtopia.Messages;
using NUnit.Framework;

namespace Tests.CodeUtopia.Domain
{
    public class When_clearing_the_changes : AggregateTestFixture<TodoList>
    {
        protected override IReadOnlyCollection<IDomainEvent> GivenEvents()
        {
            return new IDomainEvent[]
                   {
                       new TodoListCreatedEvent
                       {
                           AggregateId = Guid.NewGuid(),
                           AggregateVersionNumber = 1,
                           Name = "Todo"
                       }
                   };
        }

        [Test]
        public void Then_there_are_no_changes()
        {
            Assert.That(Changes.Count, Is.EqualTo(0));
        }

        protected override void When()
        {
            ((IAggregate)Aggregate).ClearChanges();
        }
    }

    public class When_loading_an_event_without_a_registered_handler : AggregateTestFixture<TodoList>
    {
        protected override IReadOnlyCollection<IDomainEvent> GivenEvents()
        {
            return new IDomainEvent[]
                   {
                       new TodoListCreatedEvent
                       {
                           AggregateId = Guid.NewGuid(),
                           AggregateVersionNumber = 1,
                           Name = "Todo"
                       },
                       new TodoListItemRemovedEvent
                       {
                           AggregateId = Aggregate.AggregateId,
                           AggregateVersionNumber = 2,
                           TodoListItemId = Guid.NewGuid()
                       }
                   };
        }

        [Then]
        public void Then_an_exception_will_be_thrown()
        {
            Assert.That(Exception, Is.InstanceOf<DomainEventHandlerNotRegisteredException>());
        }

        [Then]
        public void Then_the_aggregate_version_number_is_one()
        {
            Assert.That(Aggregate.AggregateVersionNumber, Is.EqualTo(1));
        }

        [Then]
        public void Then_there_are_no_changes()
        {
            Assert.That(Changes.Count, Is.EqualTo(0));
        }

        protected override void When()
        {
        }
    }

    public class When_loading_an_event_with_a_registered_handler : AggregateTestFixture<TodoList>
    {
        protected override IReadOnlyCollection<IDomainEvent> GivenEvents()
        {
            return new IDomainEvent[]
                   {
                       new TodoListCreatedEvent
                       {
                           AggregateId = Guid.NewGuid(),
                           AggregateVersionNumber = 1,
                           Name = "Todo"
                       },
                       new TodoListItemAddedEvent
                       {
                           AggregateId = Aggregate.AggregateId,
                           AggregateVersionNumber = 2,
                           TodoListItemId = Guid.NewGuid()
                       }
                   };
        }

        [Then]
        public void Then_an_exception_is_not_thrown()
        {
            Assert.That(Exception, Is.EqualTo(null));
        }

        [Then]
        public void Then_the_aggregate_version_number_is_two()
        {
            Assert.That(Aggregate.AggregateVersionNumber, Is.EqualTo(2));
        }

        [Then]
        public void Then_there_are_no_changes()
        {
            Assert.That(Changes.Count, Is.EqualTo(0));
        }

        protected override void When()
        {
        }
    }
}
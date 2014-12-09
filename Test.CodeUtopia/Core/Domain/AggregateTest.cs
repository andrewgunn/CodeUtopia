using System;
using System.Collections.Generic;
using System.Linq;
using CodeUtopia.Domain;
using CodeUtopia.Events;
using NUnit.Framework;

namespace Test.CodeUtopia.Core.Domain
{
    public class When_clearing_the_changes : AggregateTestFixture<Customer>
    {
        protected override IReadOnlyCollection<IDomainEvent> Given()
        {
            return new IDomainEvent[]
                   {
                       new CustomerCreated(Guid.NewGuid(), 1)
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

    public class When_loading_a_single_event_without_a_registered_handler : AggregateTestFixture<Customer>
    {
        protected override IReadOnlyCollection<IDomainEvent> Given()
        {
            return new IDomainEvent[]
                   {
                       new CustomerCreated(Guid.NewGuid(), 1), new CustomerDidSomethingElse(Aggregate.AggregateId, 1)
                   };
        }

        [Then]
        public void Then_an_exception_will_be_thrown()
        {
            Assert.That(Exception, Is.InstanceOf<DomainEventHandlerNotRegisteredException>());
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

    public class When_triggering_behaviour_on_the_aggregate_and_entities : AggregateTestFixture<Customer>
    {
        protected override IReadOnlyCollection<IDomainEvent> Given()
        {
            return new IDomainEvent[]
                   {
                       new CustomerCreated(Guid.NewGuid(), 1)
                   };
        }

        [Then]
        public void Then_an_exception_is_not_thrown()
        {
            Assert.That(Exception, Is.EqualTo(null));
        }

        [Then]
        public void Then_the_aggregate_and_the_first_event_have_a_different_event_version_number()
        {
            Assert.That(Aggregate.EventVersionNumber,
                        Is.Not.EqualTo(Changes.ElementAt(0)
                                              .VersionNumber));
        }

        [Then]
        public void Then_the_aggregate_and_the_first_event_have_a_different_version_number()
        {
            Assert.That(Aggregate.VersionNumber,
                        Is.Not.EqualTo(Changes.ElementAt(0)
                                              .VersionNumber));
        }

        [Then]
        public void Then_the_aggregate_and_the_first_event_have_the_same_id()
        {
            Assert.That(Aggregate.AggregateId,
                        Is.EqualTo(Changes.ElementAt(0)
                                          .AggregateId));
        }

        [Then]
        public void Then_the_aggregate_and_the_last_event_have_a_different_version_number()
        {
            Assert.That(Aggregate.VersionNumber,
                        Is.Not.EqualTo(Changes.Last()
                                              .VersionNumber));
        }

        [Then]
        public void Then_the_aggregate_and_the_last_event_have_the_same_event_version_number()
        {
            Assert.That(Aggregate.EventVersionNumber,
                        Is.EqualTo(Changes.Last()
                                          .VersionNumber));
        }

        [Then]
        public void Then_the_aggregate_and_the_second_event_have_a_different_event_version_number()
        {
            Assert.That(Aggregate.EventVersionNumber,
                        Is.Not.EqualTo(Changes.ElementAt(1)
                                              .VersionNumber));
        }

        [Then]
        public void Then_the_aggregate_and_the_second_event_have_a_different_version_number()
        {
            Assert.That(Aggregate.VersionNumber,
                        Is.Not.EqualTo(Changes.ElementAt(1)
                                              .VersionNumber));
        }

        [Then]
        public void Then_the_first_event_was_customer_did_something()
        {
            Assert.That(Changes.ElementAt(0), Is.InstanceOf<CustomerDidSomething>());
        }

        [Then]
        public void Then_the_second_event_was_order_added_to_customer()
        {
            Assert.That(Changes.ElementAt(1), Is.InstanceOf<OrderAddedToCustomer>());
        }

        [Then]
        public void Then_the_third_event_was_order_did_something_else()
        {
            Assert.That(Changes.ElementAt(2), Is.InstanceOf<OrderDidSomething>());
        }

        [Then]
        public void Then_there_are_three_changes()
        {
            Assert.That(Changes.Count, Is.EqualTo(3));
        }

        protected override void When()
        {
            Aggregate.DoSomething();

            var orderId = Guid.NewGuid();
            Aggregate.AddOrder(orderId);

            var order = Aggregate.GetOrder(orderId);
            order.DoSomething();
        }
    }

    public class When_loading_a_single_event_with_a_registered_handler : AggregateTestFixture<Customer>
    {
        protected override IReadOnlyCollection<IDomainEvent> Given()
        {
            return new IDomainEvent[]
                   {
                       new CustomerCreated(Guid.NewGuid(), 1), new CustomerDidSomething(Aggregate.AggregateId, 1)
                   };
        }

        [Then]
        public void Then_an_exception_is_not_thrown()
        {
            Assert.That(Exception, Is.EqualTo(null));
        }

        [Then]
        public void Then_the_aggregate_version_number_is_one()
        {
            Assert.That(Aggregate.VersionNumber, Is.EqualTo(1));
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

    public class Customer : Aggregate
    {
        public Customer()
        {
            _orders = new EntityList<Order>(this);

            RegisterEventHandlers();
        }

        private Customer(Guid customerId)
        {
            Apply(new CustomerCreated(customerId, GetNextVersionNumber()));
        }

        public void AddOrder(Guid orderId)
        {
            Apply(new OrderAddedToCustomer(AggregateId, GetNextVersionNumber(), orderId));
        }

        public static Customer Create(Guid customerId)
        {
            return new Customer(customerId);
        }

        public void DoSomething()
        {
            Apply(new CustomerDidSomething(AggregateId, GetNextVersionNumber()));
        }

        public void DoSomethingElse()
        {
            Apply(new CustomerDidSomethingElse(AggregateId, GetNextVersionNumber()));
        }

        public Order GetOrder(Guid orderId)
        {
            Order order;

            if (!_orders.TryGetValue(orderId, out order))
            {
                throw new OrderNotFoundException(orderId);
            }

            return order;
        }

        private void OnCustomerCreated(CustomerCreated customerCreated)
        {
            AggregateId = customerCreated.CustomerId;
        }

        private void OnOrderAddedToCustomer(OrderAddedToCustomer orderAddedToCustomer)
        {
            var order = Order.Create(AggregateId, this, orderAddedToCustomer.OrderId);

            _orders.Add(order);
        }

        private void RegisterEventHandlers()
        {
            RegisterEventHandler<CustomerCreated>(OnCustomerCreated);
            RegisterEventHandler<CustomerDidSomething>(x =>
                                                       {
                                                       });
            RegisterEventHandler<OrderAddedToCustomer>(OnOrderAddedToCustomer);
        }

        private readonly EntityList<Order> _orders;
    }

    public class OrderAddedToCustomer : DomainEvent
    {
        public OrderAddedToCustomer(Guid aggregateId, int versionNumber, Guid orderId)
            : base(aggregateId, versionNumber)
        {
            _orderId = orderId;
        }

        public Guid OrderId
        {
            get
            {
                return _orderId;
            }
        }

        private readonly Guid _orderId;
    }

    public class CustomerCreated : DomainEvent
    {
        public CustomerCreated(Guid aggregateId, int versionNumber)
            : base(aggregateId, versionNumber)
        {
        }

        public Guid CustomerId
        {
            get
            {
                return ((IDomainEvent)this).AggregateId;
            }
        }
    }

    public class OrderNotFoundException : Exception
    {
        public OrderNotFoundException(Guid orderId)
            : base(string.Format("The order {0} cannot be found.", orderId))
        {
        }
    }

    public class Order : Entity
    {
        private Order(Guid aggregateId, IVersionNumberProvider versionNumberProvider, Guid orderId)
            : base(aggregateId, versionNumberProvider)
        {
            EntityId = orderId;

            RegisterEventHandlers();
        }

        public static Order Create(Guid aggregateId, IVersionNumberProvider versionNumberProvider, Guid orderId)
        {
            return new Order(aggregateId, versionNumberProvider, orderId);
        }

        public void DoSomething()
        {
            Apply(new OrderDidSomething(AggregateId, GetNextVersionNumber(), EntityId));
        }

        public void DoSomethingElse()
        {
            Apply(new OrderDidSomethingElse(AggregateId, GetNextVersionNumber(), EntityId));
        }

        private void RegisterEventHandlers()
        {
            RegisterEventHandler<OrderDidSomething>(x =>
                                                    {
                                                    });
        }
    }

    public class OrderCreated : DomainEvent
    {
        public OrderCreated(Guid aggregateId, int versionNumber)
            : base(aggregateId, versionNumber)
        {
        }
    }

    public class OrderDidSomething : EntityEvent
    {
        public OrderDidSomething(Guid aggregateId, int versionNumber, Guid entityId)
            : base(aggregateId, versionNumber, entityId)
        {
        }
    }

    public class OrderDidSomethingElse : EntityEvent
    {
        public OrderDidSomethingElse(Guid aggregateId, int versionNumber, Guid entityId)
            : base(aggregateId, versionNumber, entityId)
        {
        }
    }

    public class CustomerDidSomething : DomainEvent
    {
        public CustomerDidSomething(Guid aggregateId, int versionNumber)
            : base(aggregateId, versionNumber)
        {
        }
    }

    public class CustomerDidSomethingElse : DomainEvent
    {
        public CustomerDidSomethingElse(Guid aggregateId, int versionNumber)
            : base(aggregateId, versionNumber)
        {
        }
    }
}
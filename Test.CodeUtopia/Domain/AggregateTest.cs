using System;
using System.Collections.Generic;
using System.Linq;
using CodeUtopia.Domain;
using NUnit.Framework;

namespace Test.CodeUtopia.Domain
{
    public class When_loading_a_single_event_without_a_registered_handler : AggregateTestFixture<TestAggregate>
    {
        protected override IReadOnlyCollection<IDomainEvent> Given()
        {
            return new[]
                   {
                       new SomethingNotRegistered(Aggregate.AggregateId, Aggregate)
                   };
        }

        [Then]
        public void Then_an_exception_will_be_thrown()
        {
            Assert.That(Exception, Is.InstanceOf<EventHandlerNotRegisteredException>());
        }

        [Then]
        public void Then_zero_events_are_applied()
        {
            Assert.That(Events.Count, Is.EqualTo(0));
        }

        protected override void When()
        {
        }
    }

    public class When_triggering_behaviour_on_the_aggregate_and_entities : AggregateTestFixture<TestAggregate>
    {
        [Then]
        public void Then_the_aggregate_and_the_first_event_have_a_different_version_number()
        {
            Assert.That(Events.ElementAt(0)
                              .VersionNumber,
                        Is.Not.EqualTo(Aggregate.VersionNumber));
        }

        [Then]
        public void Then_the_aggregate_and_the_first_event_have_the_same_id()
        {
            Assert.That(Events.ElementAt(0)
                              .AggregateId,
                        Is.EqualTo(Aggregate.AggregateId));
        }

        [Then]
        public void Then_the_aggregate_and_the_second_event_have_the_same_version_number()
        {
            Assert.That(Events.ElementAt(1)
                              .VersionNumber,
                        Is.EqualTo(Aggregate.VersionNumber));
        }

        [Then]
        public void Then_the_first_event_was_something_registered()
        {
            Assert.That(Events.ElementAt(0), Is.InstanceOf<SomethingRegistered>());
        }

        [Then]
        public void Then_the_second_event_was_something_else_registered()
        {
            Assert.That(Events.ElementAt(1), Is.InstanceOf<SomethingElseRegistered>());
        }

        [Then]
        public void Then_two_events_are_applied()
        {
            Assert.That(Events.Count, Is.EqualTo(2));
        }

        protected override void When()
        {
            Aggregate.DoSomethingRegistered();

            var testEntity = Aggregate.GetTestEntity();
            testEntity.DoSomethingElseRegistered();
        }
    }

    public class When_loading_a_single_event_with_a_registered_handler : AggregateTestFixture<TestAggregate>
    {
        protected override IReadOnlyCollection<IDomainEvent> Given()
        {
            return new[]
                   {
                       new SomethingRegistered(Aggregate.AggregateId, Aggregate)
                   };
        }

        [Then]
        public void Then_an_exception_is_not_thrown()
        {
            Assert.That(Exception, Is.EqualTo(null));
        }

        [Then]
        public void Then_zero_events_are_applied()
        {
            Assert.That(Events.Count, Is.EqualTo(0));
        }

        protected override void When()
        {
        }
    }

    public class TestAggregate : Aggregate
    {
        public TestAggregate()
        {
            AggregateId = Guid.NewGuid();

            _testEntities = new EntityList<TestEntity>(this)
                            {
                                new TestEntity(AggregateId, this)
                            };

            RegisterEventHandlers();
        }

        public void AddTestEntity(TestEntity testEntity)
        {
            _testEntities.Add(testEntity);
        }

        public void DoSomethingNotRegistered()
        {
            Apply(new SomethingNotRegistered(AggregateId, this));
        }

        public void DoSomethingRegistered()
        {
            Apply(new SomethingRegistered(AggregateId, this));
        }

        public TestEntity GetTestEntity()
        {
            return _testEntities.ElementAt(0);
        }

        private void RegisterEventHandlers()
        {
            RegisterEventHandler<SomethingRegistered>(x =>
                                                      {
                                                      });
        }

        private readonly EntityList<TestEntity> _testEntities;
    }

    public class TestEntityNotFoundException : Exception
    {
        public TestEntityNotFoundException(Guid testEntityId)
            : base(string.Format("The test entity {0} cannot be found.", testEntityId))
        {
        }
    }

    public class TestEntity : Entity
    {
        public TestEntity(Guid aggregateId, IVersionNumberProvider versionNumberProvider)
            : base(aggregateId, versionNumberProvider)
        {
            RegisterEventHandlers();
        }

        public void DoSomethingElseNotRegistered()
        {
            Apply(new SomethingElseNotRegistered(AggregateId, VersionNumberProvider, EntityId));
        }

        public void DoSomethingElseRegistered()
        {
            Apply(new SomethingElseRegistered(AggregateId, VersionNumberProvider, EntityId));
        }

        private void RegisterEventHandlers()
        {
            RegisterEventHandler<SomethingElseRegistered>(x =>
                                                          {
                                                          });
            RegisterEventHandler<SomethingElseNotRegistered>(x =>
                                                             {
                                                             });
        }
    }

    public class SomethingElseRegistered : EntityEvent
    {
        public SomethingElseRegistered(Guid aggregateId, IVersionNumberProvider versionNumberProvider, Guid entityId)
            : base(aggregateId, versionNumberProvider, entityId)
        {
        }
    }

    public class SomethingElseNotRegistered : EntityEvent
    {
        public SomethingElseNotRegistered(Guid aggregateId, IVersionNumberProvider versionNumberProvider, Guid entityId)
            : base(aggregateId, versionNumberProvider, entityId)
        {
        }
    }

    public class SomethingRegistered : DomainEvent
    {
        public SomethingRegistered(Guid aggregateId, IVersionNumberProvider versionNumberProvider)
            : base(aggregateId, versionNumberProvider)
        {
        }
    }

    public class SomethingNotRegistered : DomainEvent
    {
        public SomethingNotRegistered(Guid aggregateId, IVersionNumberProvider versionNumberProvider)
            : base(aggregateId, versionNumberProvider)
        {
        }
    }
}
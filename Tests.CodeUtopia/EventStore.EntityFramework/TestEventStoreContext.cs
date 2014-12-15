using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using CodeUtopia.EventStore.EntityFramework;
using NUnit.Framework;

namespace Tests.CodeUtopia.EventStore.EntityFramework
{
    public class When_adding_two_events_with_the_same_aggregate_version_number : EventStoreContextTestFixture
    {
        public When_adding_two_events_with_the_same_aggregate_version_number()
        {
            _formatter = new BinaryFormatter();

            var serializable = new Serializable();

            _domainEvent = new DomainEventEntity
                              {
                                  AggregateId = new Guid("753e7d92-a277-44f5-ab9b-15d4f134b9d1"),
                                  AggregateVersionNumber = 1,
                                  DomainEventType = serializable.GetType()
                                                                .FullName,
                                  Data = Serialize(serializable)
                              };
        }

        private readonly DomainEventEntity _domainEvent;

        protected override IReadOnlyCollection<DomainEventEntity> GivenDomainEvents()
        {
            return new[]
                   {
                       _domainEvent
                   };
        }

        private byte[] Serialize(object obj)
        {
            using (var memoryStream = new MemoryStream())
            {
                _formatter.Serialize(memoryStream, obj);

                return memoryStream.ToArray();
            }
        }

        [Then]
        public void Then_a_primary_key_violation_exception_will_be_thrown()
        {
            Assert.That(Exception, Is.Not.Null);

            var primaryKeyConstraintViolationException = Exception as PrimaryKeyConstraintViolationException;

            Assert.That(primaryKeyConstraintViolationException, Is.Not.Null);
            Assert.That(primaryKeyConstraintViolationException.Keys, Has.None.Null);
            Assert.That(primaryKeyConstraintViolationException.Keys, Has.Member(_domainEvent.AggregateId.ToString()));
            Assert.That(primaryKeyConstraintViolationException.Keys, Has.Member(_domainEvent.AggregateVersionNumber.ToString(CultureInfo.InvariantCulture)));
        }

        protected override void When()
        {
            using (var databaseContext = new EventStoreContext("EventStore"))
            {
                foreach (var domainEvent in GivenDomainEvents())
                {
                    databaseContext.DomainEvents.Add(domainEvent);
                }

                databaseContext.SaveChanges();
            }
        }

        private readonly IFormatter _formatter;

        [Serializable]
        private class Serializable
        {
        }
    }
}
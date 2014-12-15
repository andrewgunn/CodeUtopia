using System;
using System.Collections.Generic;
using CodeUtopia.Domain;
using CodeUtopia.Events;
using NUnit.Framework;

namespace Tests.CodeUtopia.Domain
{
    public class When_clearing_the_changes : AggregateTestFixture<Book>
    {
        protected override IReadOnlyCollection<IDomainEvent> GivenEvents()
        {
            return new IDomainEvent[]
                   {
                       new BookRegisteredEvent(Guid.NewGuid(), 1, "Lorem Ipsum")
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

    public class When_loading_an_event_without_a_registered_handler : AggregateTestFixture<Book>
    {
        protected override IReadOnlyCollection<IDomainEvent> GivenEvents()
        {
            return new IDomainEvent[]
                   {
                       new BookRegisteredEvent(Guid.NewGuid(), 1, "Lorem Ipsum"),
                       new BookStolenEvent(Aggregate.AggregateId, 2, DateTime.UtcNow)
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

    public class When_loading_an_event_with_a_registered_handler : AggregateTestFixture<Book>
    {
        protected override IReadOnlyCollection<IDomainEvent> GivenEvents()
        {
            return new IDomainEvent[]
                   {
                       new BookRegisteredEvent(Guid.NewGuid(), 1, "Lorem Ipsum"),
                       new BookLentEvent(Aggregate.AggregateId, 2, DateTime.UtcNow)
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

    public class Book : Aggregate
    {
        public Book()
        {
            RegisterEventHandlers();
        }

        private Book(Guid bookId, string title)
            : this()
        {
            Apply(new BookRegisteredEvent(bookId, BookVersionNumber, title));
        }

        public void Lend(DateTime lentAt)
        {
            Apply(new BookLentEvent(BookId, BookVersionNumber, lentAt));
        }

        private void OnBookLent(BookLentEvent bookLentAt)
        {
            _lentAt = bookLentAt.LentAt;
        }

        private void OnBookRegistered(BookRegisteredEvent bookRegisteredEvent)
        {
            AggregateId = bookRegisteredEvent.AggregateId;
            _title = bookRegisteredEvent.Title;
        }

        private void OnBookReturned(BookReturnedEvent bookReturnedEvent)
        {
            _lentAt = null;
        }

        public static Book Register(Guid bookId, string title)
        {
            return new Book(bookId, title);
        }

        private void RegisterEventHandlers()
        {
            RegisterEventHandler<BookRegisteredEvent>(OnBookRegistered);
            RegisterEventHandler<BookLentEvent>(OnBookLent);
            RegisterEventHandler<BookReturnedEvent>(OnBookReturned);
        }

        public void Return(DateTime returnedAt)
        {
            Apply(new BookReturnedEvent(BookId, BookVersionNumber, returnedAt));
        }

        private Guid BookId
        {
            get
            {
                return AggregateId;
            }
        }

        private int BookVersionNumber
        {
            get
            {
                return AggregateVersionNumber;
            }
        }

        private DateTime? _lentAt;

        private string _title;
    }

    public class BookRegisteredEvent : DomainEvent
    {
        public BookRegisteredEvent(Guid bookId, int bookVersionNumber, string title)
            : base(bookId, bookVersionNumber)
        {
            _title = title;
        }

        public Guid BookId
        {
            get
            {
                return ((IDomainEvent)this).AggregateId;
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }
        }

        private readonly string _title;
    }

    public class BookLentEvent : DomainEvent
    {
        public BookLentEvent(Guid bookId, int bookVersionNumber, DateTime lentAt)
            : base(bookId, bookVersionNumber)
        {
            _lentAt = lentAt;
        }

        public Guid BookId
        {
            get
            {
                return ((IDomainEvent)this).AggregateId;
            }
        }

        public DateTime LentAt
        {
            get
            {
                return _lentAt;
            }
        }

        private readonly DateTime _lentAt;
    }

    public class BookReturnedEvent : DomainEvent
    {
        public BookReturnedEvent(Guid bookId, int bookVersionNumber, DateTime returnedAt)
            : base(bookId, bookVersionNumber)
        {
            _returnedAt = returnedAt;
        }

        public Guid BookId
        {
            get
            {
                return ((IDomainEvent)this).AggregateId;
            }
        }

        public DateTime ReturnedAt
        {
            get
            {
                return _returnedAt;
            }
        }

        private readonly DateTime _returnedAt;
    }

    public class BookStolenEvent : DomainEvent
    {
        public BookStolenEvent(Guid bookId, int bookVersionNumber, DateTime stolenAt)
            : base(bookId, bookVersionNumber)
        {
            _stolenAt = stolenAt;
        }

        public Guid BookId
        {
            get
            {
                return ((IDomainEvent)this).AggregateId;
            }
        }

        public DateTime StolenAt
        {
            get
            {
                return _stolenAt;
            }
        }

        private readonly DateTime _stolenAt;
    }
}
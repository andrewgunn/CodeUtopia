using Library.Events.v1;
using NServiceBus;

namespace Library.Frontend.ReadStore.Book.EventHandlers
{
    public class BookRegisteredEventHandler : IHandleMessages<BookRegisteredEvent>
    {
        public BookRegisteredEventHandler(IReadStoreDatabaseSettings readStoreDatabaseSettings)
        {
            _readStoreDatabaseSettings = readStoreDatabaseSettings;
        }

        public void Handle(BookRegisteredEvent bookRegisteredEvent)
        {
            using (var databaseContext = new ReadStoreContext(_readStoreDatabaseSettings))
            {
                var book = new BookEntity
                           {
                               BookId = bookRegisteredEvent.AggregateId,
                               Title = bookRegisteredEvent.Title
                           };

                databaseContext.Books.Add(book);

                databaseContext.SaveChanges();
            }
        }

        private readonly IReadStoreDatabaseSettings _readStoreDatabaseSettings;
    }
}
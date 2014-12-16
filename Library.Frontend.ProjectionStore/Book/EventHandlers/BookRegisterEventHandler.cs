using CodeUtopia;
using Library.Events;

namespace Library.Frontend.ProjectionStore.Book.EventHandlers
{
    public class BookRegisteredEventHandler : IEventHandler<BookRegisteredEvent>
    {
        public BookRegisteredEventHandler(string nameOrConnectionString)
        {
            _nameOrConnectionString = nameOrConnectionString;
        }

        public void Handle(BookRegisteredEvent bookRegisteredEvent)
        {
            using (var databaseContext = new ProjectionStoreContext(_nameOrConnectionString))
            {
                var client = new BookEntity
                             {
                                 BookId = bookRegisteredEvent.BookId,
                                 Title = bookRegisteredEvent.Title
                             };

                databaseContext.Books.Add(client);

                databaseContext.SaveChanges();
            }
        }

        private readonly string _nameOrConnectionString;
    }
}
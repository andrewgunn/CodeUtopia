using System;
using System.Collections.Generic;
using Library.Frontend.Host.Models;

namespace Library.Frontend.Host.Hubs
{
    public interface IBookHub
    {
        [Obsolete]
        void BookBorrowed(Guid bookId);

        void BookBorrowed(Guid bookId, DateTime returnBy);

        void BookRegistered(Guid bookId, string title);

        void BookReturned(Guid bookId);

        void LoadBooks(IReadOnlyCollection<BookModel> books);
    }
}
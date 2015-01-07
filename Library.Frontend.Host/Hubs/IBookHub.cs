using System;
using System.Collections.Generic;
using Library.Frontend.Host.Models;

namespace Library.Frontend.Host.Hubs
{
    public interface IBookHub
    {
        void BookBorrowed(Guid bookId);

        void BookRegistered(Guid bookId, string title);

        void BookReturned(Guid bookId);

        void LoadBooks(IReadOnlyCollection<BookModel> books);
    }
}
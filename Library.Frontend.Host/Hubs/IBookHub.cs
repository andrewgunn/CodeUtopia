using System.Collections.Generic;
using Library.Frontend.Host.Models;

namespace Library.Frontend.Host.Hubs
{
    public interface IBookHub
    {
        void LoadBooks(IReadOnlyCollection<BookModel> books);
    }
}
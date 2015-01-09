using System;

namespace Library.Frontend.ReadStore.Book
{
    public class BookEntity
    {
        public Guid BookId { get; set; }

        public bool IsBorrowed { get; set; }

        public DateTime? ReturnBy { get; set; }

        public string Title { get; set; }
    }
}
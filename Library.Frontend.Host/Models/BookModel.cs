using System;

namespace Library.Frontend.Host.Models
{
    public class BookModel
    {
        public BookModel(Guid bookId, string title)
        {
            _bookId = bookId;
            _title = title;
        }

        public Guid BookId
        {
            get
            {
                return _bookId;
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }
        }

        private readonly Guid _bookId;

        private readonly string _title;
    }
}
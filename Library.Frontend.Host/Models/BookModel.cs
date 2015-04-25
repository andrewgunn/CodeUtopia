using System;

namespace Library.Frontend.Host.Models
{
    public class BookModel
    {
        public BookModel(Guid bookId, string title, bool isBorrowed, string returnBy)
        {
            _bookId = bookId;
            _title = title;
            _isBorrowed = isBorrowed;
            _returnBy = returnBy;
        }

        public Guid BookId
        {
            get
            {
                return _bookId;
            }
        }

        public bool IsBorrowed
        {
            get
            {
                return _isBorrowed;
            }
        }

        public string ReturnBy
        {
            get
            {
                return _returnBy;
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

        private readonly bool _isBorrowed;

        private readonly string _returnBy;

        private readonly string _title;
    }
}
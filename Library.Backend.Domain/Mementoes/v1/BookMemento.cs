using System;

namespace Library.Backed.Domain.Mementoes.v1
{
    [Serializable]
    public class BookMemento
    {
        public BookMemento(Guid bookId, string title, bool isBorrowed)
        {
            _bookId = bookId;
            _title = title;
            _isBorrowed = isBorrowed;
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

        public string Title
        {
            get
            {
                return _title;
            }
        }

        private readonly Guid _bookId;

        private readonly bool _isBorrowed;

        private readonly string _title;
    }
}
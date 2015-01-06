using System;

namespace Library.Backend.Domain.Mementoes.v1
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


        public string Title
        {
            get
            {
                return _title;
            }
        }

        public bool IsBorrowed
        {
            get
            {
                return _isBorrowed;
            }
        }

        private readonly Guid _bookId;


        private readonly string _title;

        private readonly bool _isBorrowed;
    }
}
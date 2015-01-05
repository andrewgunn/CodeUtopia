using System;

namespace Library.Backend.Domain.Mementoes.v1
{
    [Serializable]
    public class BookMemento
    {
        public BookMemento(Guid bookId, string title, DateTime? borrowedAt)
        {
            _bookId = bookId;
            _title = title;
            _borrowedAt = borrowedAt;
        }

        public Guid BookId
        {
            get
            {
                return _bookId;
            }
        }

        public DateTime? BorrowedAt
        {
            get
            {
                return _borrowedAt;
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

        private readonly DateTime? _borrowedAt;

        private readonly string _title;
    }
}
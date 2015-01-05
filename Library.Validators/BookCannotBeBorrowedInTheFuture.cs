using System;
using CodeUtopia.Validators;

namespace Library.Validators
{
    public class BookCannotBeBorrowedInTheFuture : IValidationError
    {
        public BookCannotBeBorrowedInTheFuture(DateTime borrowedAt, DateTime now)
        {
            _borrowedAt = borrowedAt;
            _now = now;
        }

        public DateTime BorrowedAt
        {
            get
            {
                return _borrowedAt;
            }
        }

        public string Message
        {
            get
            {
                return "The book cannot be borrowed in the future.";
            }
        }

        public DateTime Now
        {
            get
            {
                return _now;
            }
        }

        private readonly DateTime _borrowedAt;

        private readonly DateTime _now;
    }
}
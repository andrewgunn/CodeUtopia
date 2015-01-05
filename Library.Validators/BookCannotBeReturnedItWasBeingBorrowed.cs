using System;
using CodeUtopia.Validators;

namespace Library.Validators
{
    public class BookCannotBeReturnedItWasBeingBorrowed : IValidationError
    {
        public BookCannotBeReturnedItWasBeingBorrowed(DateTime borrowedAt, DateTime returnedAt)
        {
            _borrowedAt = borrowedAt;
            _returnedAt = returnedAt;
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
                return "Book cannot be returned before it was borrowed.";
            }
        }

        public DateTime ReturnedAt
        {
            get
            {
                return _returnedAt;
            }
        }

        private readonly DateTime _borrowedAt;

        private readonly DateTime _returnedAt;
    }
}
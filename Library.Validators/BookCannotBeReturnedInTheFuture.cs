using System;
using CodeUtopia.Validators;

namespace Library.Validators
{
    public class BookCannotBeReturnedInTheFuture : IValidationError
    {
        public BookCannotBeReturnedInTheFuture(DateTime returnedAt, DateTime now)
        {
            _returnedAt = returnedAt;
            _now = now;
        }

        public string Message
        {
            get
            {
                return "The book cannot be returned in the future.";
            }
        }

        public DateTime Now
        {
            get
            {
                return _now;
            }
        }

        public DateTime ReturnedAt
        {
            get
            {
                return _returnedAt;
            }
        }

        private readonly DateTime _now;

        private readonly DateTime _returnedAt;
    }
}
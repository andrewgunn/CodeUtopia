using System;

namespace Library.Validators.Book
{
    public class BookErrorException : Exception
    {
        public BookErrorException(BookErrorCodes errorCodes)
            : base("One or more errors occurred.")
        {
            _errorCodes = errorCodes;
        }

        public BookErrorCodes ErrorCodes
        {
            get
            {
                return _errorCodes;
            }
        }

        private readonly BookErrorCodes _errorCodes;
    }
}
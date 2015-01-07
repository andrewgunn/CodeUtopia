using System;

namespace Library.Validators.Book
{
    public class BookValidationFailedException : Exception
    {
        public BookValidationFailedException(BookValidationErrorCodes errorCodes)
            : base("One or more Book validation errors occurred.")
        {
            _errorCodes = errorCodes;
        }

        public BookValidationErrorCodes ErrorCodes
        {
            get
            {
                return _errorCodes;
            }
        }

        private readonly BookValidationErrorCodes _errorCodes;
    }
}
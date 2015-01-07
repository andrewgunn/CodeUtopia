using System;

namespace Library.Validators.Book
{
    [Flags]
    public enum BookValidationErrorCodes
    {
        None = 0,

        TitleIsNull = 1 << 0,

        TitleIsTooShort = 1 << 1,

        TitleIsTooLong = 1 << 2,

        TitleContainsInvalidCharacters = 1 << 3,
    }
}
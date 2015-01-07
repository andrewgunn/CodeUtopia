using System;

namespace Library.Commands.v1.Replies
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
using System.Text.RegularExpressions;

namespace Library.Validators.Book
{
    public class TitleValidator
    {
        public BookValidationErrorCodes Validate(string title)
        {
            var errorCodes = BookValidationErrorCodes.None;

            if (string.IsNullOrEmpty(title))
            {
                errorCodes |= BookValidationErrorCodes.TitleIsNull;
            }
            else
            {
                if (title.Length < 5)
                {
                    errorCodes |= BookValidationErrorCodes.TitleIsTooShort;
                }
                if (title.Length > 50)
                {
                    errorCodes |= BookValidationErrorCodes.TitleIsTooLong;
                }
                if (!Regex.IsMatch(title, "[a-zA-Z ]+"))
                {
                    errorCodes |= BookValidationErrorCodes.TitleContainsInvalidCharacters;
                }
            }

            return errorCodes;
        }
    }
}
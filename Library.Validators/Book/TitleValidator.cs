using System.Text.RegularExpressions;

namespace Library.Validators.Book
{
    public class TitleValidator
    {
        public BookErrorCodes Validate(string title)
        {
            var errorCodes = BookErrorCodes.None;

            if (string.IsNullOrEmpty(title))
            {
                errorCodes |= BookErrorCodes.TitleIsNull;
            }
            else
            {
                if (title.Length < 5)
                {
                    errorCodes |= BookErrorCodes.TitleIsTooShort;
                }
                if (title.Length > 50)
                {
                    errorCodes |= BookErrorCodes.TitleIsTooLong;
                }
                if (!Regex.IsMatch(title, "^[a-zA-Z ]+$"))
                {
                    errorCodes |= BookErrorCodes.TitleContainsInvalidCharacters;
                }
                if (Regex.IsMatch(title, "^[ ]+"))
                {
                    errorCodes |= BookErrorCodes.TitleHasWhiteSpaceAtTheBeginning;
                }
                if (Regex.IsMatch(title, "[ ]+$"))
                {
                    errorCodes |= BookErrorCodes.TitleHasWhiteSpaceAtTheEnd;
                }
            }

            return errorCodes;
        }
    }
}
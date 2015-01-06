using System.Collections.Generic;
using CodeUtopia.Validators;

namespace Library.Validators.Book
{
    public class TitleValidator : IValidator<string>
    {
        public IReadOnlyCollection<IValidationError> Validate(string title)
        {
            var validationErrors = new List<IValidationError>();

            if (title == null)
            {
                validationErrors.Add(new TitleNotDefined());
            }
            else
            {
                if (title.Length == 0)
                {
                    validationErrors.Add(new TitleTooShort(title));
                }
                if (title.Length > 50)
                {
                    validationErrors.Add(new TitleTooLong(title));
                }
            }

            return validationErrors;
        }
    }
}
using System.Collections.Generic;
using CodeUtopia.Validators;
using Library.Commands.v1;
using Library.Validators.Book;

namespace Library.Validators.Commands.v1
{
    public class RegisterBookCommandValidator : IValidator<RegisterBookCommand>
    {
        public IReadOnlyCollection<IValidationError> Validate(RegisterBookCommand candidate)
        {
            var validationErrors = new List<IValidationError>();

            if (candidate == null)
            {
                validationErrors.Add(new TitleNotDefined());
            }
            else
            {
                validationErrors.AddRange(new TitleValidator().Validate(candidate.Title));
            }

            return validationErrors;
        }
    }
}
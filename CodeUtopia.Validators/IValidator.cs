using System.Collections.Generic;
using CodeUtopia.Validators.ValidationErrors;

namespace CodeUtopia.Validators
{
    public interface IValidator
    {
        bool IsValid { get; }

        IReadOnlyCollection<IValidationError> ValidationErrors { get; }
    }
}
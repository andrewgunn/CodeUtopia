using System.Collections.Generic;

namespace CodeUtopia.Validators
{
    public interface IValidator<in T>
    {
        IReadOnlyCollection<IValidationError> Validate(T candidate);
    }
}
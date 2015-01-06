using CodeUtopia.Validators;

namespace CodeUtopia.Messages.Validators
{
    public interface ICommandValidator
    {
        IValidationError Validate(object command);
    }
}
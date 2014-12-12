namespace CodeUtopia.Validators
{
    public interface IValidator<in T>
    {
        IValidationError Validate(T candidate);
    }
}
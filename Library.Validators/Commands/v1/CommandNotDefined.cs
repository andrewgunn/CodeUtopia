using CodeUtopia.Validators;

namespace Library.Validators.Commands.v1
{
    public class CommandNotDefined : IValidationError
    {
        public string Message
        {
            get
            {
                return "The command is not defined.";
            }
        }
    }
}
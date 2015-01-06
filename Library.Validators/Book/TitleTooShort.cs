using CodeUtopia.Validators;

namespace Library.Validators.Book
{
    public class TitleTooShort: IValidationError
    {
        public TitleTooShort(string title)
        {
            _title = title;
        }

        public string Message
        {
            get
            {
                return "The title is too short.";
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }
        }

        private readonly string _title;
    }
}